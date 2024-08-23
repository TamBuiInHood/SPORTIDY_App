using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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
    }
}
