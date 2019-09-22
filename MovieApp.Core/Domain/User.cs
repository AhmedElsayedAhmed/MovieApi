using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApp.Core.Domain
{
    public class User : BaseEntities
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public char Gender { get; set; }
        public byte Age { get; set; }
        public string Phone { get; set; }
        public virtual IList<FavoriteUserMovies> FavoriteUserMovies { get; set; }
    }
}
