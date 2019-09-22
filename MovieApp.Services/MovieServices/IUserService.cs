using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Services.MovieServices
{
    public  interface IUserService
    {
        User CheckAuthentication(string Email, string Password);
        User GetById(int UserId);
    }
}
