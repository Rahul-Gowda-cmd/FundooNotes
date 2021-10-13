using Experimental.System.Messaging;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        private readonly IConfiguration configuration;
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }


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

        public static string EncryptData(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

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
        public string GenerateToken(string Email)
        {
            byte[] key = Convert.FromBase64String(this.configuration["SecretKey"]);
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
