using FundooManager.Interface;
using FundooModels;
using FundooRepository.Context;
using FundooRepository.Interface;
using FundooRepository.Repository;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IUserRepository repository;
        
        public UserController(IUserManager manager, IUserRepository repository)
        {
            this.manager = manager;
            this.repository = repository;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            try
            {
                string resultMessage = await this.manager.Register(user);
                if (resultMessage.Equals("Registration Successfull"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }


        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userlogin)
        {
            try
            {
                string resultMessage = this.manager.Login(userlogin);
                if (resultMessage.Equals("Login Successfull"))
                {
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
                    string tokenString = this.repository.GenerateToken(userlogin.Email);
                    return this.Ok(new { Status = true, Message = resultMessage, Data = user, tokenString });
                }
                else if (resultMessage.Equals("Wrong Password"))
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/ForgetPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                string resultMessage = this.manager.ForgotPassword(email);
                if (resultMessage.Equals("Check your Email"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = resultMessage});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("api/resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserModel resetpassword)
        {
            try
            {
                string message =await this.manager.ResetPassword(resetpassword);
                if (message.Equals("Password Reset Successfull"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = message });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = message });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

    }
}
