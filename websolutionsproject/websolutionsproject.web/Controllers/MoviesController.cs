using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.ActorMovies;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Favorites;
using websolutionsproject.models.Genres;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class MoviesController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public MoviesController(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "movies");

                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");
             
                return View(getMovieModels);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "movie");

                GetMovieModel getMovieModel = await _moviemindAPIService.GetModel<GetMovieModel>(id, "Movies");

                foreach(GetReviewModel getReviewModel in getMovieModel.Reviews)
                {
                    getReviewModel.User = await _moviemindAPIService.GetModel<GetUserModel>(getReviewModel.UserId.ToString(), "users");
                }

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                ViewBag.IsFavorite = false;

                foreach (GetMovieModel fav in user.Favorites)
                {
                    if (fav.Id == Guid.Parse(id))
                    {
                        ViewBag.IsFavorite = true;
                    }
                }

                return View(getMovieModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "movie");

                List<GetGenreModel> getGenreModels = await _moviemindAPIService.GetModels<GetGenreModel>("genres");
                List<GetDirectorModel> getDirectorModels = await _moviemindAPIService.GetModels<GetDirectorModel>("directors");

                ViewBag.Genres = getGenreModels;
                ViewBag.Directors = getDirectorModels;

                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Create(PostMovieModel postMovieModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "movie");


                if (ModelState.IsValid)
                {
                    GetMovieModel getMovieModel = await _moviemindAPIService.PostModel<PostMovieModel, GetMovieModel>(postMovieModel, "Movies");

                    //put in new relationships
                    foreach (Guid actorId in postMovieModel.ActorIds)
                    {
                        await _moviemindAPIService.PostModel<PostActorMovieModel, GetActorMovieModel>(new PostActorMovieModel
                        {
                            ActorId = actorId,
                            MovieId = getMovieModel.Id
                        }, "ActorMovies");
                    }

                    return Redirect("/Movies/Details/" + getMovieModel.Id.ToString());
                }

                return View(postMovieModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(postMovieModel));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "movie");

                GetMovieModel getMovieModel = await _moviemindAPIService.GetModel<GetMovieModel>(id, "Movies");
                List<GetGenreModel> getGenreModels = await _moviemindAPIService.GetModels<GetGenreModel>("genres");
                List<GetDirectorModel> getDirectorModels = await _moviemindAPIService.GetModels<GetDirectorModel>("directors");

                PutMovieModel putMovieModel = new PutMovieModel
                {
                    Name = getMovieModel.Name,
                    Year = getMovieModel.Year,
                    Description = getMovieModel.Description,
                    OverallRating = getMovieModel.OverallRating,
                    Duration = getMovieModel.Duration,
                    DirectorId = getMovieModel.DirectorId,
                    Director = getMovieModel.Director,
                    GenreId = getMovieModel.GenreId,
                    Genre = getMovieModel.Genre
                };

                ViewBag.Genres = getGenreModels;
                ViewBag.Directors = getDirectorModels;

                return View(putMovieModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Edit(string id, PutMovieModel putMovieModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "movie");

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PutModel<PutMovieModel>(id, putMovieModel, "Movies");

                    //delete (override) previous relationships
                    if (putMovieModel.ActorIds != null)
                    {
                        List<GetActorMovieModel> getActorMovieModels = await _moviemindAPIService.GetModels<GetActorMovieModel>("ActorMovies");
                        List<GetActorMovieModel> getActorMovieModelsToDelete = getActorMovieModels.Where(x => x.MovieId == Guid.Parse(id)).ToList();

                        foreach (GetActorMovieModel getActorMovieModel in getActorMovieModelsToDelete)
                        {
                            await _moviemindAPIService.DeleteModel(getActorMovieModel.Id.ToString(), "ActorMovies");
                        }

                        //put in new relationships
                        foreach (Guid actorId in putMovieModel.ActorIds)
                        {
                            await _moviemindAPIService.PostModel<PostActorMovieModel, GetActorMovieModel>(new PostActorMovieModel
                            {
                                ActorId = actorId,
                                MovieId = Guid.Parse(id)
                            }, "ActorMovies");
                        }
                    }

                    return Redirect("/Movies/Details/" + id.ToString());
                }

                return View(putMovieModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putMovieModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "movie");

                GetMovieModel getMovieModel = await _moviemindAPIService.GetModel<GetMovieModel>(id, "Movies");

                return View(getMovieModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetMovieModel getMovieModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "movie");

                await _moviemindAPIService.DeleteModel(id, "Movies");

                return RedirectToRoute(new { action = "Index", controller = "Movies" });
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getMovieModel));
            }
        }

        public async Task<IActionResult> AddFavorite(string movieId)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                PostFavoriteModel postFavoriteModel = new PostFavoriteModel
                {
                    UserId = user.Id,
                    MovieId = Guid.Parse(movieId)
                };

                GetFavoriteModel getFavoriteModel = await _moviemindAPIService.PostModel<PostFavoriteModel, GetFavoriteModel>(postFavoriteModel, "Favorites");
                return Redirect("/Movies/Details/" + movieId);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public async Task<IActionResult> RemoveFavorite(string movieId)
        {
            try
            {
                List<GetFavoriteModel> getFavoriteModels = await _moviemindAPIService.GetModels<GetFavoriteModel>("Favorites");
                
                foreach(var fav in getFavoriteModels)
                {
                    if(fav.MovieId == Guid.Parse(movieId))
                    {
                        await _moviemindAPIService.DeleteModel(fav.Id.ToString(), "favorites");
                    }
                }
                
                return Redirect("/Movies/Details/" + movieId);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }
    }
}
