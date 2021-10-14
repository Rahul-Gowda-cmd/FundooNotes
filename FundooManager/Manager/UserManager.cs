using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task<string> Register(UserModel user)
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

        public string ForgotPassword(string email)
        {
            try
            {
                return this.repository.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Task<string> ResetPassword(UserModel resetpassword)
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

        public string GenerateToken(string email)
        {
            try
            {
                return this.repository.GenerateToken(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
