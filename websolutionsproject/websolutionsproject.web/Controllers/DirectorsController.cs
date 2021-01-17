using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.Directors;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class DirectorsController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public DirectorsController(MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "directors");

                List<GetDirectorModel> getActorModels = await _moviemindAPIService.GetModels<GetDirectorModel>("directors");

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
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "director");

                GetDirectorModel getDirectorModel = await _moviemindAPIService.GetModel<GetDirectorModel>(id, "Directors");

                return View(getDirectorModel);
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
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "director");

                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Create(PostDirectorModel postDirectorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Create", "director");


                if (ModelState.IsValid)
                {
                    GetDirectorModel getDirectorModel = await _moviemindAPIService.PostModel<PostDirectorModel, GetDirectorModel>(postDirectorModel, "Directors");

                    if (TempData["controller"] != null && TempData["action"] != null)
                    {
                        return RedirectToRoute(new { action = TempData["action"], controller = TempData["controller"] });
                    }

                    return Redirect("Directors/Details/" + getDirectorModel.Id.ToString());
                }

                return View(postDirectorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(postDirectorModel));
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "director");

                GetDirectorModel getDirectorModel = await _moviemindAPIService.GetModel<GetDirectorModel>(id, "Directors");

                PutDirectorModel putDirectorModel = new PutDirectorModel
                {
                    FirstName = getDirectorModel.FirstName,
                    LastName = getDirectorModel.LastName,
                    Birth = getDirectorModel.Birth,
                    Description = getDirectorModel.Description,
                    Nationality = getDirectorModel.Nationality
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
        public async Task<IActionResult> Edit(string id, PutDirectorModel putDirectorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Edit", "director");

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PutModel<PutDirectorModel>(id, putDirectorModel, "Directors");

                    return RedirectToRoute(new { action = "Index", controller = "Directors"});
                }

                return View(putDirectorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putDirectorModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "director");

                GetDirectorModel getDirectorModel = await _moviemindAPIService.GetModel<GetDirectorModel>(id, "Directors");

                return View(getDirectorModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetDirectorModel getDirectorModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Editor", this.GetType().Name, "Delete", "director");

                await _moviemindAPIService.DeleteModel(id, "Directors");

                return RedirectToRoute(new { action = "Index", controller = "Directors"});
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getDirectorModel));
            }
        }
    }
}
