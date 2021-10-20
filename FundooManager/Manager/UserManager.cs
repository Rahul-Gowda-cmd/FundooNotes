// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModels;
    using FundooRepository.Interface;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// ClassManager
    /// </summary>
    /// <seealso cref="FundooManager.Interface.IUserManager" />
    public class UserManager : IUserManager
    {
        /// <summary>
        /// The repository
        /// </summary>
        private readonly IUserRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Returns string if Register is successful
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Logins the specified userlogin.
        /// </summary>
        /// <param name="userlogin">The userlogin.</param>
        /// <returns>
        /// returns string if login is successful
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns string if mail sent successful else false
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetpassword">The resetpassword.</param>
        /// <returns>
        /// Returns true if the password is successfully reset
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Returns the token when user logins
        /// </returns>
        /// <exception cref="System.Exception"></exception>
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
