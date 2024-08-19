using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using FSU.SPORTIDY.Repository.Repositories;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using FSU.SPORTIDY.Service.Utils.Enums;
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

namespace FSU.SPORTIDY.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
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

                        string refreshToken = GenerateRefreshToken(email);
                        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int tokenValidityTime);

                        await _unitOfWork.UserTokenRepository.AddUserToken(new UserToken
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
                            RefreshTokenn = refreshToken
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
                        FullName = model.FullName,
                        Status = 1,
                        RoleId = (int) model.Role,
                    };

                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                    if(existUser != null)
                    {
                        throw new Exception("Accoust is existed");
                    }
                    newUser.Password = PasswordHelper.HashPassword(model.Password);
                    var role = await _unitOfWork.RoleRepository.GetRoleByName(model.Role.ToString());
                    if(role == null)
                    {
                        Role newRole = new Role
                        {
                            RoleName = model.Role.ToString(),
                        };
                        await _unitOfWork.RoleRepository.AddRoleAsync(newRole);
                        role = newRole;
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
                            var accessToken =  await GenerateAccessToken(email, existUser);
                            var refreshToken = GenerateRefreshToken(email);
                            return new AuthenModel
                            {
                                HttpCode = 200,
                                Message = "Refresh Token successfully",
                                AccessToken = accessToken,
                                RefreshTokenn = refreshToken,
                            };
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
            var role = await _unitOfWork.RoleRepository.GetRoleById(user.RoleId);
            var authClaims = new List<Claim>(); 
            if(role != null)
            {
                authClaims.Add(new Claim(ClaimTypes.Email, email));
                authClaims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                authClaims.Add(new Claim("Status", user.Status.ToString()));
                authClaims.Add(new Claim("FullName", user.FullName));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            }
            var accessToken = GenerateJWTToken.CreateAccessToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
            };
            var refreshToken = GenerateJWTToken.CreateRefreshToken(claims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
    }
}
