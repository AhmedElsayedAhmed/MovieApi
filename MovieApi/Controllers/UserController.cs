using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieApi.Models;
using MovieApp.Services.MovieServices;
using Newtonsoft.Json;

namespace MovieApi.Controllers
{
    public class UserController : Controller
    {
        #region Fields
        private IConfiguration _config;
        private readonly IUserService _userService;
        #endregion 

        #region Constructor
        public UserController(IConfiguration _config,
            IUserService _userService)
        {
            this._config = _config;
            this._userService = _userService;
        }
        #endregion

        #region Login
        [HttpPost]
        public JsonResult Login([FromBody]LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return Json(500);
            }
            if (loginModel.Email == null || loginModel.Password == null)
            {
                return Json(410);
            }
            var user = AuthenticateUser(loginModel);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                tokenString = tokenString + " " + user.Id;
                return Json(tokenString);
            }
            return Json(410);
        }
        #endregion

        #region Generate JWT
        private string GenerateJSONWebToken(LoginModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

        #region Check Authentication for user
        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;
            if (login != null)
            {
                if (login.Email != null && login.Password != null)
                {
                    var userModel = _userService.CheckAuthentication(login.Email, login.Password);
                    if (userModel != null)
                    {
                        user = new LoginModel { Email = login.Email, Password = login.Password , Id = userModel.Id};
                    }
                }
            }
            return user;
        }
        #endregion 
    }
}
