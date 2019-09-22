using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MovieApi.Models.Movies;
using MovieApp.Core.Domain;

namespace MovieApi.Factories
{
    public class MovieModelFactory : IMovieModelFactory
    {
        private readonly IHostingEnvironment host;
        public MovieModelFactory(IHostingEnvironment host)
        {
            this.host = host;
        }
        public MovieModel PrepareMovieModel(Movie Model, string host)
        {
            if (Model == null)
                return null;
            MovieModel MovieModel = new MovieModel()
            {
                Id = Model.Id,
                Description = Model.Description,
                Title = Model.Title,
                ImageName = host + "/uploads/" + Model.Image.FileName
            };
            return MovieModel;
        }

        public IList<MovieModel> PrepareMovieModelList(IList<Movie> MovieList)
        {
            IList<MovieModel> movieModels = new List<MovieModel>();
            if (MovieList == null)
                return null;

            foreach (var item in MovieList)
            {
                movieModels.Add(new MovieModel()
                {
                    Description = item.Description,
                    Id = item.Id,
                    Image = new FileModel()
                    {
                        FileName = item.Image.FileName,
                        Id = item.Image.Id
                    },
                    Title = item.Title,
                    ImageName = "https://localhost:44334/uploads/" + item.Image.FileName
                });
                
            }
            return movieModels;

        }
    }
}
