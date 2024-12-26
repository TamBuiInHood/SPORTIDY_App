using FirebaseAdmin.Auth;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using FSU.SPORTIDY.Service.Utils.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Firebase.Storage;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Common.Role;
using Microsoft.AspNetCore.Routing.Tree;
using FSU.SPORTIDY.Service.BusinessModel.UserBsModels;

namespace FSU.SPORTIDY.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMailService mailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mailService = mailService;
            _mapper = mapper;
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
                            Message = "Account does not exist. Please try again"
                        };
                    }
                    var verifyPassword = PasswordHelper.VerifyPassword(password, existUser.Password);
                    if (verifyPassword)
                    {
                        if (existUser.Status == 0 || existUser.IsDeleted == 1)
                        {
                            return new AuthenModel
                            {
                                HttpCode = 401,
                                Message = "Your account is banned"
                            };
                        }
                        var accessToken = await GenerateAccessToken(email, existUser);
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
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    User newUser = new User()
                    {
                        Email = model.Email,
                        UserCode = "SPD" + model.Email + NumberHelper.GenerateSixDigitNumber(),
                        FullName = model.FullName,
                        Status = 1,
                        RoleId = (int)model.Role,
                        IsDeleted = 0,
                        CreateDate = DateOnly.FromDateTime(DateTime.Now),
                        Avartar = model.Avatar
                    };

                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                    if (existUser != null)
                    {
                        throw new Exception("Accoust is existed");
                    }
                    if (model.Password != null)
                    {
                        newUser.Password = PasswordHelper.HashPassword(model.Password);
                    }
                    var role = await _unitOfWork._RoleRepo.GetRoleByName(model.Role.ToString());
                    if (role == null)
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
                if (email != null)
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
            if (role != null)
            {
                authClaims.Add(new Claim("email", email));
                authClaims.Add(new Claim("role", role.RoleName));
                authClaims.Add(new Claim("UserId", user.UserId.ToString()));
                authClaims.Add(new Claim("Status", user.Status.ToString()));
                authClaims.Add(new Claim("DeviceCode", user.DeviceCode ?? "Not yet"));
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
                 new Claim("email", email),
                 new Claim("role", role.RoleName),
                 new Claim("UserId", user.UserId.ToString()),
                 new Claim("Status", user.Status.ToString()),
                 new Claim("DeviceCode", user.DeviceCode ?? "Not yet"),
                 new Claim("FullName", user.FullName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };


            var refreshToken = GenerateJWTToken.CreateRefreshToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
        public async Task<bool> RequestResetPassword(string email)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            if (existUser != null)
            {
                if (existUser.Status == 1 && existUser.IsDeleted == 0)
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
                    bool checkSendMail = await SendOtpResetPasswordAsync(email, otpCode);
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
                if (existUser != null)
                {
                    if (existUser.Otp.Equals(confirmOtpModel.OtpCode))
                    {
                        return true;
                    }
                    else
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
                if (checkUser != null)
                {
                    checkUser.Password = PasswordHelper.HashPassword(resetPasswordModel.Password);
                    var result = await _unitOfWork.UserRepository.UpdateUserAsync(checkUser);
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Reset password failed");
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

        public async Task<AuthenModel> LoginWithGoogle(string credental)
        {

            string clientId = _configuration["GoogleCredential:ClientId"];
            if (string.IsNullOrEmpty(clientId))
            {
                throw new Exception("clientId is null");
            }

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
                            RoleId = (int)UserRoleEnum.PLAYER
                        };

                        var checkUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                        if (checkUser != null)
                        {
                            throw new Exception("Accoust is existed");
                        }
                        newUser.Password = PasswordHelper.HashPassword(Guid.NewGuid().ToString());
                        var role = await _unitOfWork._RoleRepo.GetRoleByName(UserRoleEnum.PLAYER.ToString());
                        if (role == null)
                        {
                            Role newRole = new Role
                            {
                                RoleName = UserRoleEnum.PLAYER.ToString(),
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
            if (existUser != null)
            {
                // update account
                if(updateUserRequestModel.FullName != null)
                {
                    existUser.FullName = updateUserRequestModel.FullName;
                }
                if(updateUserRequestModel.Description != null)
                {
                    existUser.Description = updateUserRequestModel.Description;
                }
                if(updateUserRequestModel.PhoneNumber != null)
                {
                    existUser.Phone = updateUserRequestModel.PhoneNumber;
                }
                if(updateUserRequestModel.Birthday != null)
                {
                    existUser.Birtday = updateUserRequestModel.Birthday;
                }
                if(updateUserRequestModel.Address != null)
                {
                    existUser.Address = updateUserRequestModel.Address;
                }
                if(updateUserRequestModel.Gender != null)
                {
                    existUser.Gender = updateUserRequestModel.Gender;
                }
                if(updateUserRequestModel.Avatar != null)
                {
                    existUser.Avartar = updateUserRequestModel.Avatar;
                }
                existUser.IsDeleted = updateUserRequestModel.IsDeleted != null ? updateUserRequestModel.IsDeleted : 0;
               
                if(!string.IsNullOrEmpty(updateUserRequestModel.Password))
                {
                    bool checkOldPassword = PasswordHelper.VerifyPassword(updateUserRequestModel.Password, existUser.Password);
                    if (checkOldPassword)
                    {
                        string newPassword = PasswordHelper.HashPassword(updateUserRequestModel.Password);
                        existUser.Password = newPassword;
                    }
                }
                var result = await _unitOfWork.UserRepository.UpdateUserAsync(existUser);
                if (result > 0)
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

        public async Task<PageEntity<UserModel>> GetAllUser(PaginationParameter paginationParameter)
        {
            Expression<Func<User, bool>> filter = null!;
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null!;
            if (!paginationParameter.Search.IsNullOrEmpty())
            {
                int validInt = 0;
                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                if (checkInt)
                {
                    filter = x => x.UserId == validInt;
                }
                else
                {
                    filter = x => x.UserCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.UserName.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.FullName.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Address.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Email.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Description.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Phone.ToLower().Contains(paginationParameter.Search.ToLower())
                    ;
                }
            }
            switch (paginationParameter.SortBy)
            {
                case "userid":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.UserId)
                               : x => x.OrderBy(x => x.UserId)) : x => x.OrderBy(x => x.UserId);
                    break;
                case "usercode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.UserCode)
                               : x => x.OrderBy(x => x.UserCode)) : x => x.OrderBy(x => x.UserCode);
                    break;
                case "username":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.UserName)
                               : x => x.OrderBy(x => x.UserName)) : x => x.OrderBy(x => x.UserName);
                    break;
                case "fullname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.FullName)
                               : x => x.OrderBy(x => x.FullName)) : x => x.OrderBy(x => x.FullName);
                    break;
                case "address":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Address)
                               : x => x.OrderBy(x => x.Address)) : x => x.OrderBy(x => x.Address);
                    break;
                case "email":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Email)
                               : x => x.OrderBy(x => x.Email)) : x => x.OrderBy(x => x.Email);
                    break;
                case "role":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Role.RoleName)
                               : x => x.OrderBy(x => x.Role.RoleName)) : x => x.OrderBy(x => x.Role.RoleName);
                    break;
                case "phone":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Phone)
                               : x => x.OrderBy(x => x.Phone)) : x => x.OrderBy(x => x.Phone);
                    break;
                default:
                    orderBy = x => x.OrderBy(x => x.UserId);
                    break;
            }
            string includeProperties = "Role";
            var entities = await _unitOfWork.UserRepository.Get(filter, orderBy, includeProperties, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<UserModel>();
            pagin.List = _mapper.Map<IEnumerable<UserModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.UserRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
            return pagin;

        }

        public async Task<UserModel> GetUserById(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            var result = _mapper.Map<UserModel>(user);
            return result;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
            var result = _mapper.Map<UserModel>(user);
            return result;
        }

        public async Task<bool> SoftDeleteUser(int userId)
        {
            var softDeleteUser = await _unitOfWork.UserRepository.SoftDeleteUserAsync(userId);
            return softDeleteUser > 0;
        }

        public async Task<bool> DeleteUser(int userId)
        {
            string includeProperties = "PlayFields,FriendshipUserId1Navigations,FriendshipUserId2Navigations,UserClubs,UserMeetings,Notifications";
            var EntityDelete = await _unitOfWork.UserRepository.GetByCondition(x => x.UserId == userId, includeProperties: includeProperties);
            if (EntityDelete == null)
            {
                return false;
            }
            EntityDelete!.PlayFields.Clear();
            foreach (var friendship in EntityDelete.FriendshipUserId1Navigations.ToList())
            {
                _unitOfWork.FriendShipRepository.Delete(friendship);
            }

            foreach (var friendship in EntityDelete.FriendshipUserId2Navigations.ToList())
            {
                _unitOfWork.FriendShipRepository.Delete(friendship);
            }
            EntityDelete!.UserClubs.Clear();
            EntityDelete!.UserMeetings.Clear();
            EntityDelete!.Notifications.Clear();

            try
            {
                if(EntityDelete.Avartar != null && EntityDelete.Avartar.Contains("firebasestorage"))
                {
                    // Create a Firebase Storage client
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    // Parse the image URL to get the file name
                    var fileNameAvatar = EntityDelete.Avartar.Substring(EntityDelete.Avartar.LastIndexOf('/') + 1);
                    fileNameAvatar = fileNameAvatar.Split('?')[0]; // Remove the query parameters
                    var encodedFileAvatar = Path.GetFileName(fileNameAvatar);
                    var fileNameAvatarOfficial = Uri.UnescapeDataString(encodedFileAvatar);


                    // Delete the avaatar club from Firebase Storage
                    var fileRefAvatar = firebaseStorage.Child(fileNameAvatarOfficial);
                    await fileRefAvatar.DeleteAsync();
                }

            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the deletion process
                throw new Exception($"Error deleting image: {ex.Message}");
            }
            _unitOfWork.UserRepository.Delete(EntityDelete);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<bool> CreateUser(CreateAccountModel createAccountModel)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    User newUser = new User()
                    {
                        Email = createAccountModel.Email,
                        UserCode = "SPD" + createAccountModel.Email + NumberHelper.GenerateSixDigitNumber(),
                        FullName = createAccountModel.FullName,
                        Status = 1,
                        RoleId = (int)createAccountModel.Role,
                        IsDeleted = 0,
                        CreateDate = DateOnly.FromDateTime(DateTime.Now),
                        Avartar = createAccountModel.AvatarUrl
                    };

                    var existUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(newUser.Email);
                    if (existUser != null)
                    {
                        throw new Exception("Accoust is existed");
                    }
                    if (createAccountModel.Password != null)
                    {
                        newUser.Password = PasswordHelper.HashPassword(createAccountModel.Password);
                    }
                    var role = await _unitOfWork._RoleRepo.GetRoleByName(createAccountModel.Role.ToString());
                    if (role == null)
                    {
                        Role newRole = new Role
                        {
                            RoleName = createAccountModel.Role.ToString(),
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

        public async Task<bool> BannedUser(int userId)
        {
            var existUser = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (existUser != null)
            {
                existUser.Status = 0;
                var result = await _unitOfWork.SaveAsync();
                if (result > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new Exception("Not found user for banned");
            }

        }

        public async Task<string> UpdateAvatarOfUser(IFormFile avatarOfUser, int id)
        {
            try
            {
                var existUser = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
                if (existUser != null)
                {
                    string fileName = Path.GetFileName(avatarOfUser.FileName);
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    await firebaseStorage.Child("user/avatar").Child(fileName).PutAsync(avatarOfUser.OpenReadStream());
                    var downloadUrl = await firebaseStorage.Child("user/avatar").Child(fileName).GetDownloadUrlAsync();
                    existUser.Avartar = downloadUrl;
                    _unitOfWork.UserRepository.Update(existUser);
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        return downloadUrl;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new Exception("User does not exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetAllUsersByRole(string roleName)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetAllUsersByRole(roleName);
                if (result.Count > 0)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateDeviceCodeByUserId(string deviceCode, int id)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetByID(id);
                if (user != null)
                {
                    user.DeviceCode = deviceCode;
                    var result = await _unitOfWork.SaveAsync();
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<UserMonthlyStatisticResponse> GetUserStatisticsByMonth(int year)
        {
            // Lấy danh sách user từ repository
            var users = await _unitOfWork.UserRepository.GetUsersByYear(year);

            // Tính tổng số user hiện tại trong hệ thống
            var listUsers = await _unitOfWork.UserRepository.GetAllNoPaging();
            var totalUsers = listUsers.ToList().Count;

            // Nhóm người dùng theo tháng và tính số lượng
            var statistics = users
                .GroupBy(u => u.CreateDate.Value.ToDateTime(TimeOnly.MinValue).Month)
                .Select(g => new UserMonthlyStatistic
                {
                    Month = g.Key,
                    UserCount = g.Count()
                })
                .ToList();

            // Đảm bảo trả về danh sách đủ 12 tháng
            var fullStatistics = Enumerable.Range(1, 12)
                .Select(month => new UserMonthlyStatistic
                {
                    Month = month,
                    UserCount = statistics.FirstOrDefault(s => s.Month == month)?.UserCount ?? 0
                }).ToList();

            // Tạo đối tượng trả về bao gồm tổng số user và thống kê theo tháng
            return new UserMonthlyStatisticResponse
            {
                TotalUsers = totalUsers,
                MonthlyStatistics = fullStatistics
            };
        }
    }
}
