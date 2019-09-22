using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieApi.Authorization;
using MovieApi.Factories;
using MovieApi.Models.Movies;
using MovieApp.Core.Domain;
using MovieApp.Services.MovieServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [AuthorizationFilter]
    public class MovieController : Controller
    {
        #region Fields
        private readonly IMovieService _movieService;
        private readonly IMovieModelFactory _movieModelFactory;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IHostingEnvironment host;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public MovieController(IMovieService _movieService,
            IMovieModelFactory _movieModelFactory,
            IHostingEnvironment host,
            IUserService _userService)
        {
            this._movieService = _movieService;
            this._movieModelFactory = _movieModelFactory;
            this.host = host;
            this._userService = _userService;
        }
        #endregion

        #region Get All Movies
        // GET: api/<controller>
        [HttpGet]
        [Route("/Movie/GetMovies/{userId}")]
        public JsonResult Get(string userId)
        {
            var Movies = _movieService.GetAllMovie(int.Parse(userId));
            if (Movies != null)
            {
                var MoviesList = _movieModelFactory.PrepareMovieModelList(Movies);
                return Json(MoviesList);
            }
            return Json(500); // empty movie
        }
        #endregion

        #region Get Movie By Id
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var list = new List<MovieModel>();
            var MovieModel = new MovieModel();
            var Movie = _movieService.GetMovieById(id);
            var host = Request.Host.Value;
            if (MovieModel != null)
            {
                MovieModel = _movieModelFactory.PrepareMovieModel(Movie, host) ;
            }
            list.Add(MovieModel);
            return Json(list);
        }
        #endregion

        #region Create Movie
        // POST api/<controller>
        [HttpPost]
        public JsonResult Post([FromBody]MovieModel Model)
        {
            int StatusCode = 500;
            var Image = new Image();
            if (Model == null)
                throw new ArgumentNullException();
            if (ModelState.IsValid)
            {
                if (Model.ImageName != null)
                {
                    Image.FileName = Model.ImageName;
                }

                var movie = PrepareMovie(Model, Image);
                _movieService.InsertMovie(movie);

                try
                {
                    _movieService.SaveMovie();
                    return Json(StatusCode = 200);
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return Json(StatusCode);
        }
        #endregion

        #region Update Movie
        // PUT api/<controller>/5
        [HttpPut]
        public JsonResult Put([FromBody]MovieModel Model)
        {
            if (Model == null)
                return Json(500);

            if (ModelState.IsValid)
            {
                var Movie = _movieService.GetMovieById(Model.Id);
                if (Movie != null)
                {
                    if (Model.ImageName != null)
                    {
                        if (!Model.ImageName.Contains("uploads"))
                        {
                            Movie.Image.FileName = Model.ImageName;
                        }
                    }
                    Movie.Id = Model.Id;
                    Movie.Description = Model.Description;
                    Movie.Title = Model.Title;
                    Movie.ImageId = Movie.Image.Id;
                    Movie.FavoriteUserMovies = Movie.FavoriteUserMovies;


                    _movieService.UpdateMovie(Movie);
                    try
                    {
                        _movieService.SaveMovie();
                        return Json(200);
                    }
                    catch (Exception)
                    {
                        return Json(410);
                    }
                }
                return Json(400);
            }
            return Json(415);
        }
        #endregion

        #region Delete Movie
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var Movie = _movieService.GetMovieById(id);
            if (Movie == null)
                return Json(500);
            else
            {
                try
                {
                    _movieService.DeleteMovie(Movie);
                    _movieService.SaveMovie();
                    return Json(200);
                }
                catch (Exception)
                {
                    return Json(500);
                }
            }
        }
        #endregion

        private Movie PrepareMovie(MovieModel Model, Image ImageModel)
        {
            var user = _userService.GetById(Model.UserId);
            var list = new List<FavoriteUserMovies>();

            Movie MovieModel = new Movie()
            {
                Description = Model.Description,
                Id = Model.Id,
                Image = ImageModel,
                Title = Model.Title,
            };
            list.Add(new FavoriteUserMovies()
            {
                User = user,
                Movie = MovieModel
            });
            MovieModel.FavoriteUserMovies = list;

            return MovieModel;

        }
    }
}
