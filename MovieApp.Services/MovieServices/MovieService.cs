using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Domain;
using MovieApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieApp.Services.MovieServices
{
    public class MovieService : IMovieService
    {
        #region Fields
        private readonly IRepository<Movie> _movieRepository;
        private readonly IRepository<Image> _fileRepository;
        private readonly IRepository<FavoriteUserMovies> _favoriteUserMovie;

        #endregion

        #region Constructor
        public MovieService(IRepository<Movie> _movieRepository,
            IRepository<Image> _fileRepository,
            IRepository<FavoriteUserMovies> _favoriteUserMovie)
        {
            this._movieRepository = _movieRepository;
            this._fileRepository = _fileRepository;
            this._favoriteUserMovie = _favoriteUserMovie;
        }
        #endregion

        #region Insert Movie
        public void InsertMovie(Movie Movie)
        {
            if (Movie == null)
                return;

            _movieRepository.Insert(Movie);
        }
        #endregion

        #region Update Movie
        public void UpdateMovie(Movie MovieModel)
        {
            if (MovieModel == null)
                return;
            
            _movieRepository.Update(MovieModel);
        }
        #endregion

        #region Save Movie
        public void SaveMovie()
        {
            _movieRepository.SaveDb();
        }
        #endregion

        #region Delete Movie
        public void DeleteMovie(Movie Movie)
        {
            if (Movie == null)
                return;
            _movieRepository.Delete(Movie);
        }
        #endregion

        #region Get Movie By Id
        public Movie GetMovieById(int? MovieId)
        {
            if (MovieId == null)
                return null;
            var movie = _movieRepository.Table.Include(a => a.Image).Where(a=>a.Id == MovieId.Value).FirstOrDefault();
            if (movie == null)
                return null;
            return movie;
        }
        #endregion

        #region Get All Movie
        public IList<Movie> GetAllMovie(int UserId)
        {
           var movie= _movieRepository.Table.Include(a => a.Image).Where(a=>a.FavoriteUserMovies.Any(b=>b.UserId == UserId)).ToList();
            return movie;
        }
        #endregion
    }
}
