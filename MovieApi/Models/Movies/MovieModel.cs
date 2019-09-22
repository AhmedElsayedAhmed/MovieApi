using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Models.Movies
{
    public class MovieModel : BaseEntity
    {
        public string Title { get; set; }
        public FileModel Image { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string ImageName { get; set; }
    }
}
