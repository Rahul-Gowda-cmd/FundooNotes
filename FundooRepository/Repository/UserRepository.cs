using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace FundooRepository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }


        public string Register(UserModel user)
        {
            try
            {
                this.userContext.Users.Add(user);
                this.userContext.SaveChanges();
                return "Registration Successfull";
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string Login(LoginModel userlogin)
        {
            try
            {
                var UserEmailcheck = this.userContext.Users
                 .Where(x => x.Email == userlogin.Email
                 && x.Password == userlogin.Password).ToList();

                //this.userContext.Users.FirstOrDefault(x =>
                //x.Email == userlogin.Email 
                //&& x.Password == userlogin.Password);

                int userId = 0;
                foreach (var userData in UserEmailcheck)
                {
                    userId = userData.UserId;
                }
                if (userId > 0)
                {
                    return "Login Successfull";
                }
                else
                {
                    return "Register new  EmailId";
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ForgotPassword(ForgotPasswordModel forgotpassword)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Email == forgotpassword.Email);

                if (users)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("Rahul.prabu.07@gmail.com");
                    mail.To.Add("Rahul.prabu.07@gmail.com");
                    mail.Subject = "Test Mail";
                    mail.Body = "This is for testing SMTP mail from GMAIL";

                    SmtpServer.Port = 587;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("Username", "Password");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                    return "Password reset link has been sent to your email id";
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

        public string ResetPassword(ResetPasswordModel resetpassword)
        {
            try
            {
                var users = this.userContext.Users.Any(x => x.Email == resetpassword.Email);

                if (users)
                {
                    var user = this.userContext.Users.Where(x => x.Email == resetpassword.Email).FirstOrDefault();
                    user.Password = resetpassword.Password;
                    this.userContext.SaveChanges();
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
    }
}
