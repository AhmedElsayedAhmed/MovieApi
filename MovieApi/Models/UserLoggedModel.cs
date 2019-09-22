using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models
{
    public class UserLoggedModel : BaseEntity
    {
        public string token { get; set; }
    }
}
