using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.UserBsModels;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using Microsoft.AspNetCore.Http;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IUserService
    {
        public Task<PageEntity<UserModel>> GetAllUser(PaginationParameter paginationParameter);
        public Task<UserModel> GetUserById(int userId);
        public Task<UserModel> GetUserByEmail(string email);
        public Task<AuthenModel> LoginByEmailAndPassword(string email, string password);
        public Task<bool> RegisterAsync(SignUpModel model);

        public Task<AuthenModel> RefreshToken(string jwtToken);
        public Task<bool> RequestResetPassword(string email);
        public Task<bool> ConfirmResetPassword(ConfirmOtpModel confirmOtpModel);
        public Task<bool> ExecuteResetPassword(ResetPasswordModel resetPasswordModel);
        public Task<AuthenModel> LoginWithGoogle(string credental);
        public Task<bool> UpdateUser(UpdateUserModel updateUserRequestModel);
        public Task<bool> SoftDeleteUser(int userId);
        public Task<bool> BannedUser(int userId);
        public Task<bool> DeleteUser(int userId);
        public Task<bool> CreateUser(CreateAccountModel createAccountModel);
        public Task<string> UpdateAvatarOfUser(IFormFile avatarOfUser, int id);
        public Task<List<User>> GetAllUsersByRole(string roleName);
        public Task<bool> UpdateDeviceCodeByUserId(string deviceCode, int id);
        public Task<UserMonthlyStatisticResponse> GetUserStatisticsByMonth(int year);
    }
}
