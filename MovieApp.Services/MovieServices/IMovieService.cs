using MovieApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.Services.MovieServices
{
    public interface IMovieService
    {
        void InsertMovie(Movie Movie);
        void UpdateMovie(Movie Movie);
        void DeleteMovie(Movie Movie);
        Movie GetMovieById(int? MovieId);
        IList<Movie> GetAllMovie(int UserId);
        void SaveMovie();
    }
}
