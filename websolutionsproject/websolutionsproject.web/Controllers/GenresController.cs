using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using websolutionsproject.models.Genres;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class GenresController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;
        private readonly IStringLocalizer<GenresController> _localizer;

        public GenresController(MoviemindAPIService moviemindAPIService, IStringLocalizer<GenresController> localizer)
        {
            _moviemindAPIService = moviemindAPIService;
            _localizer = localizer;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "genres");

                List<GetGenreModel> getActorModels = await _moviemindAPIService.GetModels<GetGenreModel>("genres");

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
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "genre");

                GetGenreModel getGenreModel = await _moviemindAPIService.GetModel<GetGenreModel>(id, "Genres");

                return View(getGenreModel);
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
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "genre");

                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Create(PostGenreModel postGenreModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "genre");


                if (ModelState.IsValid)
                {
                    GetGenreModel getGenreModel = await _moviemindAPIService.PostModel<PostGenreModel, GetGenreModel>(postGenreModel, "Genres");

                    if (TempData["controller"] != null && TempData["action"] != null)
                    {
                        return RedirectToRoute(new { action = TempData["action"], controller = TempData["controller"] });
                    }

                    return RedirectToRoute(new { action = "Index", controller = "Genres" });
                }

                return View(postGenreModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(postGenreModel));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "genre");

                GetGenreModel getGenreModel = await _moviemindAPIService.GetModel<GetGenreModel>(id, "Genres");

                PutGenreModel putGenreModel = new PutGenreModel
                {
                    Name = getGenreModel.Name
                };

                return View(putGenreModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Edit(string id, PutGenreModel putGenreModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "genre");

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PutModel<PutGenreModel>(id, putGenreModel, "Genres");

                    return RedirectToRoute(new { action = "Index", controller = "Genres" });
                }

                return View(putGenreModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putGenreModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "genre");

                GetGenreModel getGenreModel = await _moviemindAPIService.GetModel<GetGenreModel>(id, "Genres");

                return View(getGenreModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetGenreModel getGenreModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "genre");

                await _moviemindAPIService.DeleteModel(id, "Genres");

                return RedirectToRoute(new { action = "Index", controller = "Genres" });
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getGenreModel));
            }
        }
    }
}
