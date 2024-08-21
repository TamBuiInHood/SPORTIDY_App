using FSU.SPORTIDY.Service.BusinessModel;
using FSU.SPORTIDY.Service.BusinessModel.AuthensModel;
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
        public Task<AuthenModel> LoginByEmailAndPassword(string email, string password);
        public Task<bool> RegisterAsync(SignUpModel model);

        public Task<AuthenModel> RefreshToken(string jwtToken);
        public Task<bool> RequestResetPassword(string email);
        public Task<bool> ConfirmResetPassword(ConfirmOtpModel confirmOtpModel);
        public Task<bool> ExecuteResetPassword(ResetPasswordModel resetPasswordModel);
        public Task<AuthenModel> LoginWithGoogle(string credental);
        public Task<bool> UpdateUser(UpdateUserModel updateUserRequestModel);
    }
}
