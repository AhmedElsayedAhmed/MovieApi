using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieApp.Core.Domain;
using MovieApp.Data;

namespace MovieApp.Services.MovieServices
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> _userRepository)
        {
            this._userRepository = _userRepository;
        }
        public User CheckAuthentication(string Email, string Password)
        {
            if (Email == null || Password == null)
                return null;
            var user = _userRepository.Table.Where(a => a.Email == Email & a.Password == Password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public User GetById(int UserId)
        {
            return _userRepository.GetById(UserId);
        }
    }
}
