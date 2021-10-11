using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public string Register(UserModel user)
        {
            try
            {
                return this.repository.Register(user) ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string Login(LoginModel userlogin)
        {
            try
            {
                return this.repository.Login(userlogin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(ForgotPasswordModel forgotpassword)
        {
            try
            {
                return this.repository.ForgotPassword(forgotpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string ResetPassword(ResetPasswordModel resetpassword)
        {
            try
            {
                return this.repository.ResetPassword(resetpassword);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
