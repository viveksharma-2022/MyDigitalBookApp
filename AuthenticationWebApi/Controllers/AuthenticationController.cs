using AuthenticationWebApi.Models;
using AuthenticationWebApi.Services;
using CommonDbLayer.DatabaseEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public MyDigitalBookDbContext DbContext { get; set; }
        public readonly IConfiguration _configuration;
        public AuthenticationController(ITokenService tokenService, MyDigitalBookDbContext dbContext, IConfiguration configuration)
        {
            _tokenService = tokenService;
            DbContext = dbContext;
            _configuration = configuration;
        }

        private class RequestedUserInfo
        {
            public long UserId { get; set; }
            public string UserName { get; set; }
            public string UserType { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public RequestedUserInfo(long userId, string userName, string usertype, string email, string password)
            {
                UserId = userId;
                UserName = userName;
                UserType = usertype;
                UserEmail = email;
                Password = password;
            }
        }


        /// <summary>
        /// This method is used to authenticate a user credentials
        /// </summary>
        /// <param name="userdetail"></param>
        /// <returns>Returns Token if credentials are correct</returns>
        [HttpPost]
        public ActionResult<string> Authentication(ValidateUserCredentials userdetail)
        {
            try
            {
                var userdata = ValidateUserCredentials(userdetail);
                if (userdata !=null)
                {
                    var token = _tokenService.BuildToken(_configuration["Jwt:Key"],
                                _configuration["Jwt:Issuer"],
                                new[]
                                {
                                _configuration["Jwt:ApiGatewayAudience"],
                                _configuration["Jwt:ReaderAudience"],
                                _configuration["Jwt:PaymentAudience"],
                                _configuration["Jwt:AuthorAudience"]
                                },
                                userdata.UserName,
                                userdata.UserType);
                    return Ok(new
                    {
                        Token = token,
                        IsAuthenticated = true
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {

                return Unauthorized();
            }
           
        }

        /// <summary>
        /// This method returns bool value based on user credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if a valid user else returns false</returns>
        private RequestedUserInfo ValidateUserCredentials(ValidateUserCredentials userLogin)
        {
            try
            {
                //var isUserValid = DbContext.UserDetails.Where(u => u.UserName == userName && u.Password == password).ToList();
                List<UserDetail> list = _tokenService.validateuser(userLogin);
                foreach (var item in list)
                {
                    return new RequestedUserInfo(item.UserId, item.UserName, item.UserType, item.UserEmail, item.Password);

                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
