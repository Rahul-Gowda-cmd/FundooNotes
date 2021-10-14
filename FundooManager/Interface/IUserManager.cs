using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{

    public interface IUserManager
    {
        Task<string> Register(UserModel user);

        string Login(LoginModel userlogin);

        string ForgotPassword(string email);

        Task<string> ResetPassword(UserModel resetpassword);
        string GenerateToken(string email);
    }
}
