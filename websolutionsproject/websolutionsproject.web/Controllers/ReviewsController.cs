using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public ReviewsController(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "reviews");

                List<GetReviewModel> getReviewModels = await _moviemindAPIService.GetModels<GetReviewModel>("reviews");

                return View(getReviewModels);
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
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "review");

                GetReviewModel getReviewModel = await _moviemindAPIService.GetModel<GetReviewModel>(id, "reviews");

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                ViewBag.IsMyReview = false;

                foreach (GetReviewModel review in user.Reviews)
                {
                    if (review.Id == Guid.Parse(id))
                    {
                        ViewBag.IsMyReview = true;
                    }
                }

                return View(getReviewModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }


        public async Task<IActionResult> Create(string movieId)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Create", "review");

                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");
                ViewBag.Movies = getMovieModels;

                if (movieId != null)
                {
                    GetMovieModel getMovieModel = await _moviemindAPIService.GetModel<GetMovieModel>(movieId, "movies");
                    ViewBag.MovieId = Guid.Parse(movieId);
                }
                
                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Create(PostReviewModel postReviewModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Create", "review");

                if (ModelState.IsValid)
                {
                    var userId = HttpContext.Session.GetString("_Id");
                    var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                    postReviewModel.UserId = user.Id;

                    GetReviewModel getReviewModel = await _moviemindAPIService.PostModel<PostReviewModel, GetReviewModel>(postReviewModel, "reviews");

                    return Redirect("/Reviews/Details/" + getReviewModel.Id.ToString());
                }

                return View(postReviewModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(postReviewModel));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "review");

                GetReviewModel getReviewModel = await _moviemindAPIService.GetModel<GetReviewModel>(id, "reviews");
                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");

                PutReviewModel putReviewModel = new PutReviewModel
                {
                    Description = getReviewModel.Description,
                    Date = getReviewModel.Date,
                    MovieId = getReviewModel.MovieId,
                    Rating = getReviewModel.Rating,
                    UserId = getReviewModel.UserId
                };

                ViewBag.Movies = getMovieModels;

                return View(putReviewModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Edit(string id, PutReviewModel putReviewModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "review");

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PutModel<PutReviewModel>(id, putReviewModel, "reviews");

                    return Redirect("/Reviews/Details/" + id.ToString());
                }

                return View(putReviewModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putReviewModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "review");

                GetReviewModel getReviewModel = await _moviemindAPIService.GetModel<GetReviewModel>(id, "reviews");

                return View(getReviewModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetReviewModel getReviewModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "review");

                await _moviemindAPIService.DeleteModel(id, "reviews");

                return RedirectToRoute(new { action = "Index", controller = "Reviews" });
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getReviewModel));
            }
        }
    }
}
