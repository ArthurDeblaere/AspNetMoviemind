using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Models;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoviemindAPIService _moviemindAPIService;

        public HomeController(ILogger<HomeController> logger, MoviemindAPIService moviemindAPIService)
        {
            _logger = logger;
            _moviemindAPIService = moviemindAPIService;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            if (returnUrl == "~/Search")
            {
                return Redirect("/");
            }
            else
            {
                return LocalRedirect(returnUrl);
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "home");

                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");
                List<GetUserModel> getUserModels = await _moviemindAPIService.GetModels<GetUserModel>("users");
                List<GetReviewModel> getReviewModels = await _moviemindAPIService.GetModels<GetReviewModel>("reviews");

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                List<GetReviewModel> followerReviews = new List<GetReviewModel>();

                foreach (GetUserModel userModel in getUserModels)
                {
                    foreach (GetUserModel getFollower in userModel.Followers)
                    {
                        if (getFollower.Id == user.Id)
                        {
                            List<GetReviewModel> reviews = (from review in getReviewModels
                                                            where review.UserId == userModel.Id
                                                            select review).ToList();

                            followerReviews.AddRange(reviews);
                        }
                    }
                }

                return View(followerReviews);

            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}