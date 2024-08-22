using AutoMapper.Internal;
using Azure.Core;
using FirebaseAdmin.Auth;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using FSU.SPORTIDY.Repository.Repositories;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using FSU.SPORTIDY.Service.Utils.Enums;
using FSU.SPORTIDY.Service.Utils.Mail;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Microsoft.AspNetCore.Http.HttpResults;
using FSU.SPORTIDY.Service.BusinessModel;

namespace FSU.SPORTIDY.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
        }

        public async Task<AuthenModel> LoginByEmailAndPassword(string email, string password)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                    if (existUser == null)
                    {
                        return new AuthenModel
                        {
                            HttpCode = 401,
                            Message = "Account does not exist"
                        };
                    }
                    var verifyPassword = PasswordHelper.VerifyPassword(password, existUser.Password);
                    if (verifyPassword)
                    {
                        if(existUser.Status == 0 || existUser.IsDeleted == 1)
                        {
                            return new AuthenModel
                            {
                                HttpCode = 401,
                                Message = "Your account is banned"
                            };
                        }
                        var accessToken =  await GenerateAccessToken(email, existUser);
                        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                        string refreshToken = await GenerateRefreshToken(email);
                        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);

                        await _unitOfWork._UserTokenRepo.AddUserToken(new UserToken
                        {
                            UserId = existUser.UserId,
                            AccessToken = accessToken,
                            RefreshToken = refreshToken,
                            CreateDate = DateTime.Now,
                            ExpiredTimeAccessToken = DateTime.Now.AddMinutes(tokenValidityInMinutes).ToString(),
                            ExpiredTimeRefreshToken = DateTime.Now.AddDays(tokenValidityTime).ToString(),   
                        });
                        await transaction.CommitAsync();
                        return new AuthenModel
                        {
                            HttpCode = 200,
                            Message = "Login successfully",
                            AccessToken = accessToken,
                            RefreshToken = refreshToken
                        };
                    }
                    else
                    {
                        return new AuthenModel
                        {
                            HttpCode = 401,
                            Message = "Password is not correct"
                        };
                    }
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
        public async Task<bool> RegisterAsync(SignUpModel model)
        {
            using(var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    User newUser = new User()
                    {
                        Email = model.Email,
                        UserCode = "SPD" + model.Email + NumberHelper.GenerateSixDigitNumber(),
                        FullName = model.FullName,
                        Status = 1,
                        RoleId = (int) model.Role,
                        IsDeleted = 0,
                        CreateDate = DateOnly.FromDateTime(DateTime.Now),
                        Avartar = model.Avatar
                    };

                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                    if(existUser != null)
                    {
                        throw new Exception("Accoust is existed");
                    }
                    if(model.Password != null)
                    {
                        newUser.Password = PasswordHelper.HashPassword(model.Password);
                    }
                    var role = await _unitOfWork._RoleRepo.GetRoleByName(model.Role.ToString());
                    if(role == null)
                    {
                        Role newRole = new Role
                        {
                            RoleName = model.Role.ToString(),
                        };
                        await _unitOfWork._RoleRepo.AddRoleAsync(newRole);
                        role = newRole;
                        newUser.RoleId = role.RoleId;
                    }
                    
                    await _unitOfWork.UserRepository.AddUserAsync(newUser);
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<AuthenModel> RefreshToken(string jwtToken)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authSigningKey,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            try
            {
                
                SecurityToken validatedToken;
                var principal = handler.ValidateToken(jwtToken, validationParameters, out validatedToken);
                var email = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                if(email != null)
                {
                    if (principal != null)
                    {
                        var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                        if (existUser != null)
                        {
                            var checkExistRefreshToken = await _unitOfWork._UserTokenRepo.GetUserTokenByRefreshToken(jwtToken);
                            if (checkExistRefreshToken == null)
                            {
                                throw new Exception("Refresh Token does not exist in system");
                            }
                            else
                            {
                                if (DateTime.Parse(checkExistRefreshToken.ExpiredTimeRefreshToken) >= DateTime.Now)
                                {
                                    var newAccessToken = await GenerateAccessToken(email, existUser);
                                    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int newTokenValidityInMinutes);
                                    await _unitOfWork._UserTokenRepo.UpdateToken(new UserToken
                                    {
                                        UserId = checkExistRefreshToken.UserId,
                                        AccessToken = newAccessToken,
                                        RefreshToken = jwtToken,
                                        ExpiredTimeAccessToken = DateTime.Now.AddMinutes(newTokenValidityInMinutes).ToString(),
                                        ExpiredTimeRefreshToken = checkExistRefreshToken.ExpiredTimeRefreshToken,
                                    });
                                    return new AuthenModel
                                    {
                                        HttpCode = 200,
                                        Message = "Refresh Token successfully",
                                        AccessToken = newAccessToken,
                                        RefreshToken = jwtToken,
                                    };
                                } 
                                else
                                {
                                    await _unitOfWork._UserTokenRepo.DeleteToken(jwtToken);
                                    throw new Exception("Refresh Token is expired time. Please log out");
                                }
                            }
                        }
                    }
                }
                return new AuthenModel
                {
                    HttpCode = 401,
                    Message = "Account does not exist",
                };
               
            }
            catch (Exception)
            {
                throw new Exception("Token is invalid");
            }
        }

        

        private async Task<string> GenerateAccessToken(string email, User user)
        {
            var role = await _unitOfWork._RoleRepo.GetRoleById(user.RoleId);
            var authClaims = new List<Claim>(); 
            if(role != null)
            {
                authClaims.Add(new Claim(ClaimTypes.Email, email));
                authClaims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                authClaims.Add(new Claim("UserId", user.UserId.ToString()));
                authClaims.Add(new Claim("Status", user.Status.ToString()));
                authClaims.Add(new Claim("FullName", user.FullName));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            }
            var accessToken = GenerateJWTToken.CreateAccessToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private async Task<string> GenerateRefreshToken(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            var role = await _unitOfWork._RoleRepo.GetRoleById(user.RoleId);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.RoleName),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Status", user.Status.ToString()),
                new Claim("FullName", user.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            
            var refreshToken = GenerateJWTToken.CreateRefreshToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
        public async Task<bool> RequestResetPassword(string email)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);  
            if(existUser != null)
            {
                if(existUser.Status == 1 && existUser.IsDeleted == 0)
                {
                    bool checkSendMail = await CreateOtpAsync(email);
                    return checkSendMail;
                }
            }
            return false;
        }
        private async Task<bool> CreateOtpAsync(string email)
        {
            try
            {
                string otpCode = NumberHelper.GenerateSixDigitNumber().ToString();
                bool checkInsertOtp = await _unitOfWork.UserRepository.UpdateOtpUser(email, otpCode);
                if (checkInsertOtp)
                {
                    bool checkSendMail = await SendOtpResetPasswordAsync(email,otpCode);
                    return checkSendMail;
                }
                else
                {
                    throw new Exception("Account does not exist. Can not send Otp");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> SendOtpResetPasswordAsync(string Email, string OtpCode)
        {
            // create new email
            MailRequest newEmail = new MailRequest()
            {
                ToEmail = Email,
                Subject = "Sportidy Reset password",
                Body = SendOTPTemplate.EmailSendOTPResetPassword(Email, OtpCode)
            };

            // send mail
            await _mailService.SendEmailAsync(newEmail);
            return true;
        }

        public async Task<bool> ConfirmResetPassword(ConfirmOtpModel confirmOtpModel)
        {
            try
            {
                var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(confirmOtpModel.Email);
                if(existUser != null)
                {
                    if(existUser.Otp.Equals(confirmOtpModel.OtpCode))
                    {
                        return true;
                    } else
                    {
                        throw new Exception("Otp does not correct. Please try again or another");
                    }
                } 
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ExecuteResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                var checkUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(resetPasswordModel.Email);
                if(checkUser != null)
                {
                    checkUser.Password = PasswordHelper.HashPassword(resetPasswordModel.Password);
                   var result = await _unitOfWork.UserRepository.UpdateUserAsync(checkUser);
                    if(result > 0)
                    {
                        return true;
                    } else
                    {
                        throw new Exception("Reset password failed");
                    }

                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<AuthenModel> LoginWithGoogle(string credental)
        {

            string clientId = _configuration["GoogleCredential:ClientId"];
            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("clientId is null");
            }

            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { clientId },
                
               
            };

            var getUser = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(credental);
            // Lấy các Claims từ token
            var payload = new User()
            {
                Email = getUser.Claims["email"] as string,
                FullName = getUser.Claims["name"] as string,
                Avartar = getUser.Claims["picture"] as string,
            };


            if (payload == null)
            {
                throw new Exception("Email is invalid");
            }
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(payload.Email);
            if (userRecord == null)
            {
                userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(new UserRecordArgs()
                {
                    Email = payload.Email,
                    DisplayName = payload.FullName,
                });
            }

            // create customer Token from Firebase
            await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userRecord.Uid);


            var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(payload.Email);
            if (existUser != null)
            {
                if (existUser.Status == 0)
                {
                    throw new Exception("Your account is banned");
                }
                else
                {
                    var accessToken = await GenerateAccessToken(existUser.Email, existUser);
                    var refreshToken = await GenerateRefreshToken(existUser.Email);
                    _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
                    _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);
                    await _unitOfWork._UserTokenRepo.AddUserToken(new UserToken
                    {
                        UserId = existUser.UserId,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        CreateDate = DateTime.Now,
                        ExpiredTimeAccessToken = DateTime.Now.AddMinutes(tokenValidityInMinutes).ToString(),
                        ExpiredTimeRefreshToken = DateTime.Now.AddDays(tokenValidityTime).ToString(),
                    });
                    return new AuthenModel()
                    {
                        HttpCode = 200,
                        Message = "Login with Google sucessfully",
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                }
            }
            else
            {
                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        User newUser = new User()
                        {
                            Email = payload.Email,
                            FullName = payload.FullName,
                            Status = 1,
                            Avartar = payload.Avartar,
                            UserCode = "SPD" + payload.Email + NumberHelper.GenerateSixDigitNumber(),
                            IsDeleted = 0,
                            CreateDate = DateOnly.FromDateTime(DateTime.Now),
                            RoleId = (int)RoleEnums.CUSTOMER
                        };

                        var checkUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                        if (checkUser != null)
                        {
                            throw new Exception("Accoust is existed");
                        }
                        newUser.Password = PasswordHelper.HashPassword(Guid.NewGuid().ToString());
                        var role = await _unitOfWork._RoleRepo.GetRoleByName(RoleEnums.CUSTOMER.ToString());
                        if (role == null)
                        {
                            Role newRole = new Role
                            {
                                RoleName = RoleEnums.CUSTOMER.ToString(),
                            };
                            await _unitOfWork._RoleRepo.AddRoleAsync(newRole);
                            role = newRole;
                            newUser.RoleId = role.RoleId;
                        }
                        await _unitOfWork.UserRepository.AddUserAsync(newUser);
                        await transaction.CommitAsync();


                        var accessToken = await GenerateAccessToken(newUser.Email, newUser);
                        var refreshToken = await GenerateRefreshToken(newUser.Email);
                        _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
                        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);
                        await _unitOfWork._UserTokenRepo.AddUserToken(new UserToken
                        {
                            UserId = newUser.UserId,
                            AccessToken = accessToken,
                            RefreshToken = refreshToken,
                            CreateDate = DateTime.Now,
                            ExpiredTimeAccessToken = DateTime.Now.AddMinutes(tokenValidityInMinutes).ToString(),
                            ExpiredTimeRefreshToken = DateTime.Now.AddDays(tokenValidityTime).ToString(),
                        });
                        return new AuthenModel()
                        {
                            HttpCode = 200,
                            Message = "Login with Google sucessfully",
                            AccessToken = accessToken,
                            RefreshToken = refreshToken
                        };
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();

                        throw;
                    }
                }
            }


        }

        public async Task<bool> UpdateUser(UpdateUserModel updateUserRequestModel)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByIdAsync(updateUserRequestModel.UserId);
            if(existUser != null)
            {
                // update account
                existUser.FullName = updateUserRequestModel.FullName;
                existUser.Description = updateUserRequestModel.Description;
                existUser.Phone = updateUserRequestModel.PhoneNumber;
                existUser.Birtday = updateUserRequestModel.Birthday;
                existUser.Address = updateUserRequestModel.Address;
                existUser.Gender = updateUserRequestModel.Gender;
                existUser.Avartar = updateUserRequestModel.Avatar;
               
                bool checkOldPassword = PasswordHelper.VerifyPassword(updateUserRequestModel.Password, existUser.Password);
                if(checkOldPassword)
                {
                    string newPassword = PasswordHelper.HashPassword(updateUserRequestModel.Password);
                    existUser.Password = newPassword;
                }
               var result = await _unitOfWork.UserRepository.UpdateUserAsync(existUser);
               if(result > 0)
               {
                    return true;
               }
               else
               {
                    throw new Exception("Update failed");
               }
            }
            else
            {
                return false;
            }
        }
    }
}
