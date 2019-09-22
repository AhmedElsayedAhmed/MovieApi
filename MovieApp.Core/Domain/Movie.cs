using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieApp.Core.Domain
{
    public class Movie : BaseEntities
    {
        public string Title { get; set; }
        public int ImageId { get; set; }
        public string Description { get; set; }
        public virtual Image Image { get; set; }
        public virtual IList<FavoriteUserMovies> FavoriteUserMovies { get; set; }
    }
}
