using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.Actors;
using websolutionsproject.models.Directors;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    public class SearchController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;
        public SearchController(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }
        //https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-5.0
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "search");

                List<GetMovieModel> getMovieModels = await _moviemindAPIService.GetModels<GetMovieModel>("movies");
                List<GetActorModel> getActorModels = await _moviemindAPIService.GetModels<GetActorModel>("actors");
                List<GetDirectorModel> getDirectorModels = await _moviemindAPIService.GetModels<GetDirectorModel>("directors");
                List<GetUserModel> getUserModels = await _moviemindAPIService.GetModels<GetUserModel>("users");


                if (!String.IsNullOrEmpty(searchString))
                {
                    ViewBag.Movies = getMovieModels.Where(s => s.Name.ToLower().Contains(searchString));
                    ViewBag.Actors = getActorModels.Where(s => s.FirstName.ToLower().Contains(searchString) || s.LastName.ToLower().Contains(searchString));
                    ViewBag.Directors = getDirectorModels.Where(s => s.FirstName.ToLower().Contains(searchString) || s.LastName.ToLower().Contains(searchString));
                    ViewBag.Users = getUserModels.Where(s => s.FirstName.ToLower().Contains(searchString) || s.LastName.ToLower().Contains(searchString) || s.Email.ToLower().Contains(searchString));
                    
                    ViewBag.SearchTerm = searchString;
                }


                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }
    }
}
