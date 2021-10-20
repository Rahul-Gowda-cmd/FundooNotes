// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Rahul prabu"/>
// --------------------------------------------------------------------------------------------------------------------

namespace FundooRepository.Repository
{
    using Experimental.System.Messaging;
    using FundooModels;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using StackExchange.Redis;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// class UserRepository 
    /// </summary>
    /// <seealso cref="FundooRepository.Interface.IUserRepository" />
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userContext">The user context.</param>
        /// <param name="configuration">The configuration.</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Registers the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        /// Returns string if Register is successful
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> Register(UserModel user)
        {
            try
            {
                user.Password = EncryptData(user.Password);
                this.userContext.Users.Add(user);
                await this.userContext.SaveChangesAsync();
                return "Registration Successfull";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Encrypts the data.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string EncryptData(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
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
                var user = this.userContext.Users
                 .SingleOrDefault(x => x.Email == userlogin.Email);

                if (user != null)
                {
                    ConnectionMultiplexer connectionmultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionmultiplexer.GetDatabase();
                    database.StringSet(key: "FirstName", user.FirstName);
                    database.StringSet(key: "LastName", user.LastName);
                    database.StringSet(key: "userId", user.UserId.ToString());
                    userlogin.Password = EncryptData(userlogin.Password);
                    if (user.Password == userlogin.Password)
                    {
                        return "Login Successfull";
                    }
                    else
                    {
                        return "Wrong Password";
                    }
                }
                else
                {
                    return "Register your Email";
                }
            }
            catch (ArgumentNullException ex)
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
                var users = this.userContext.Users.Any(x => x.Email == email);
                if (users)
                {
                    MessageQueue msgqueue;
                    if (MessageQueue.Exists(@".\Private$\MyQueue"))
                    {
                        msgqueue = new MessageQueue(@".\Private$\MyQueue");
                    }
                    else
                    {
                        msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
                    }
                    Message message = new Message();

                    message.Formatter = new BinaryMessageFormatter();
                    message.Body = "This is for testing SMTP mail from GMAIL";

                    msgqueue.Label = "Mail";
                    msgqueue.Send(message);
                    SendEmail(email);
                    return "Check your Email";
                }
                else
                {
                    return "User Doesn't Exist";
                }

            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="Email">The email.</param>
        public async static void SendEmail(string Email)
        {
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }

            //// for reading message from MSMQ
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.Body = receivemsg.Body.ToString();

            mail.From = new MailAddress("Rahul.prabu.07@gmail.com");
            mail.To.Add("Rahul.prabu.07@gmail.com");
            mail.Subject = "Test Mail";

            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Credentials = new System.Net.NetworkCredential("Rahul.prabu.07@gmail.com", "Password");
            SmtpServer.EnableSsl = true;
            await SmtpServer.SendMailAsync(mail);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="resetpassword">The resetpassword.</param>
        /// <returns>
        /// Returns true if the password is successfully reset
        /// </returns>
        /// <exception cref="System.Exception"></exception>
        public async Task<string> ResetPassword(UserModel resetpassword)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Email == resetpassword.Email);

                if (users)
                {
                    var user = this.userContext.Users.Where(x =>
                    x.Email == resetpassword.Email).FirstOrDefault();
                    user.Password = EncryptData(resetpassword.Password);
                    await this.userContext.SaveChangesAsync();
                    return "Password Reset Successfull";
                }
                else
                {
                    return "User Doesn't Exist";
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Generates the token.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        public string GenerateToken(string Email)
        {
            var key = Encoding.UTF8.GetBytes(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, Email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);

            //var token = new JwtSecurityToken(
            //claims: new Claim[]
            //{
            //    new Claim(ClaimTypes.Name,useremail)
            //},
            //notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            //expires: new DateTimeOffset(DateTime.Now.AddMinutes(30)).DateTime,
            //signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
            //);
            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
