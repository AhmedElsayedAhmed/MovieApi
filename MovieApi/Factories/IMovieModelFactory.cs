using MovieApi.Models.Movies;
using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApi.Factories
{
    public interface IMovieModelFactory
    {
        MovieModel PrepareMovieModel(Movie Model, string host);
       IList<MovieModel> PrepareMovieModelList(IList<Movie> Model);

    }
}
