using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.ActorMovies;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Genres;
using websolutionsproject.models.Movies;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ActorsController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public ActorsController(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "actors");

                List<GetActorModel> getActorModels = await _moviemindAPIService.GetModels<GetActorModel>("actors");

                return View(getActorModels);
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
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "actor");

                GetActorModel getActorModel = await _moviemindAPIService.GetModel<GetActorModel>(id, "Actors");

                foreach (GetMovieModel getMovieModel in getActorModel.Movies){
                    getMovieModel.Director = await _moviemindAPIService.GetModel<GetDirectorModel>(getMovieModel.DirectorId.ToString(), "Directors");
                    getMovieModel.Genre = await _moviemindAPIService.GetModel<GetGenreModel>(getMovieModel.GenreId.ToString(), "Genres");
                }

                return View(getActorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public IActionResult Create()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "actor");

                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Create(PostActorModel postActorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "actor");

                if (ModelState.IsValid)
                {
                    GetActorModel getActorModel = await _moviemindAPIService.PostModel<PostActorModel, GetActorModel>(postActorModel, "Actors");

                    //put in new relationships
                    foreach (Guid movieId in postActorModel.MovieIds)
                    {
                        await _moviemindAPIService.PostModel<PostActorMovieModel, GetActorMovieModel>(new PostActorMovieModel
                        {
                            ActorId = getActorModel.Id,
                            MovieId = movieId
                        }, "ActorMovies");
                    }

                    return Redirect("/Actors/Details/" + getActorModel.Id.ToString());
                }

                return View(postActorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(postActorModel));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "actor");

                GetActorModel getActorModel = await _moviemindAPIService.GetModel<GetActorModel>(id, "Actors");
                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");

                ViewBag.Movies = getMovieModels;

                PutActorModel putDirectorModel = new PutActorModel
                {
                    FirstName = getActorModel.FirstName,
                    LastName = getActorModel.LastName,
                    Birth = getActorModel.Birth,
                    Description = getActorModel.Description,
                    Nationality = getActorModel.Nationality,
                    Movies = getActorModel.Movies
                };

                return View(putDirectorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Edit(string id, PutActorModel putActorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "actor");

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PutModel<PutActorModel>(id, putActorModel, "Actors");

                    //delete (override) previous relationships
                    if (putActorModel.MovieIds != null)
                    {
                        List<GetActorMovieModel> getActorMovieModels = await _moviemindAPIService.GetModels<GetActorMovieModel>("ActorMovies");
                        List<GetActorMovieModel> getActorMovieModelsToDelete = getActorMovieModels.Where(x => x.ActorId == Guid.Parse(id)).ToList();

                        foreach (GetActorMovieModel getActorMovieModel in getActorMovieModelsToDelete)
                        {
                            await _moviemindAPIService.DeleteModel(getActorMovieModel.Id.ToString(), "ActorMovies");
                        }
                    }

                    //put in new relationships
                    foreach (Guid movieId in putActorModel.MovieIds)
                    {
                        await _moviemindAPIService.PostModel<PostActorMovieModel, GetActorMovieModel>(new PostActorMovieModel
                        {
                            ActorId = Guid.Parse(id),
                            MovieId = movieId
                        }, "ActorMovies");
                    }
                }

                return Redirect("/Actors/Details/" + id);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putActorModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "actor");

                GetActorModel getActorModel = await _moviemindAPIService.GetModel<GetActorModel>(id, "Actors");

                return View(getActorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetActorModel getActorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "actor");

                await _moviemindAPIService.DeleteModel(id, "Actors");

                return RedirectToRoute(new { action = "Index", controller = "Actors" });
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getActorModel));
            }
        }
    }
}
