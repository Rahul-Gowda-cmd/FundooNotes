using FundooManager.Interface;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controller
{
   
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;

        private readonly ILogger<UserController> logger;

        public UserController(IUserManager manager, ILogger<UserController> logger)
        {
            this.manager = manager;
            this.logger = logger;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            try
            {
                this.logger.LogInformation(user.FirstName + " " + user.LastName + " is trying to register");
                string resultMessage = await this.manager.Register(user);
                if (resultMessage.Equals("Registration Successfull"))
                {
                    this.logger.LogInformation(user.FirstName + " " + user.LastName + " " + resultMessage);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    this.logger.LogWarning(user.FirstName + " " + user.LastName + " " + resultMessage);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using register " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userlogin)
        {
            try
            {
                this.logger.LogInformation(userlogin.Email + " is trying to Login");
                string resultMessage = this.manager.Login(userlogin);
                string tokenString = this.manager.GenerateToken(userlogin.Email);
                if (resultMessage.Equals("Login Successfull"))
                {
                    this.logger.LogInformation(userlogin.Email + " logged in successfully and the token generated is "+ tokenString);
                    ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = connectionMultiplexer.GetDatabase();
                    string firstname = database.StringGet("FirstName");
                    string lastname = database.StringGet("LastName");
                    int userId = Convert.ToInt32(database.StringGet("UserId"));

                    UserModel user = new UserModel
                    {
                        FirstName = firstname,
                        LastName = lastname,
                        UserId = userId,
                        Email = userlogin.Email
                    };
                    //    var user = this.userContext.Users.SingleOrDefault(x => x.Email == userlogin.Email);
                    //    user.Password = null;
                    
                    return this.Ok(new { Status = true, Message = resultMessage, Data = user, tokenString });
                }
                else if (resultMessage.Equals("Wrong Password"))
                {
                    this.logger.LogWarning(userlogin.Email + " " + resultMessage);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
                else
                {
                    this.logger.LogError(userlogin.Email + " " + resultMessage);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while logging in " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/ForgetPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                this.logger.LogInformation(email + "is using forgot password");
                string resultMessage = this.manager.ForgotPassword(email);
                if (resultMessage.Equals("Check your Email"))
                {
                    this.logger.LogInformation(resultMessage);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage});
                }
                else
                {
                    this.logger.LogError(resultMessage);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using forgot password " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserModel resetpassword)
        {
            try
            {
                this.logger.LogInformation(resetpassword.Email + "is using reset password");
                string message =await this.manager.ResetPassword(resetpassword);
                if (message.Equals("Password Reset Successfull"))
                {
                    this.logger.LogInformation("Password reseted Successfully for " + resetpassword.Email);
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    this.logger.LogError(message);
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogInformation("Exception occured while using reset password " + ex.Message);
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
