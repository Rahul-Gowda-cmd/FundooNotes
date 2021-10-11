using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {

        string Register(UserModel user);
        string Login(LoginModel userlogin);
        string ForgotPassword(ForgotPasswordModel forgotpassword);
        string ResetPassword(ResetPasswordModel resetpassword);

    }
}
