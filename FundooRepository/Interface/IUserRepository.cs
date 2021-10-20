// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Interface
{
    using FundooModels;
    using System.Threading.Tasks;

    /// <summary>
    /// interface IUserRepository
    /// </summary>
    public interface IUserRepository
    {

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Returns string if Register is successful</returns>
        Task<string> Register(UserModel user);

        /// <summary>
        /// Logins the specified userlogin.
        /// </summary>
        /// <param name="userlogin">The userlogin.</param>
        /// <returns>returns string if login is successful</returns>
        string Login(LoginModel userlogin);

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns string if mail sent successful else false</returns>
        string ForgotPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetpassword">The resetpassword.</param>
        /// <returns>Returns true if the password is successfully reset</returns>
        Task<string> ResetPassword(UserModel resetpassword);

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Returns the token when user logins</returns>
        string GenerateToken(string email);
    }
}
