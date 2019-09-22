using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models
{
    public class LoginModel : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
