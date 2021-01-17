using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.Favorites;
using websolutionsproject.models.Movies;
using websolutionsproject.models.Reviews;
using websolutionsproject.models.UserFollowers;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsersController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;

        public UsersController(
            MoviemindAPIService moviemindAPIService)
        {
            _moviemindAPIService = moviemindAPIService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Index", "users");

                List<GetUserModel> getUsersModels = await _moviemindAPIService.GetModels<GetUserModel>("users");

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                //index does not include users own profile
                getUsersModels.RemoveAll(x => x.Id == user.Id);

                return View(getUsersModels);
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
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "Details", "user");

                GetUserModel getUserModel = await _moviemindAPIService.GetModel<GetUserModel>(id, "users");

                foreach(GetReviewModel getReview in getUserModel.Reviews)
                {
                    getReview.Movie = await _moviemindAPIService.GetModel<GetMovieModel>(getReview.MovieId.ToString(), "movies");
                    getReview.User = await _moviemindAPIService.GetModel<GetUserModel>(getReview.UserId.ToString(), "users");
                }

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                ViewBag.IsFollower = false;

                foreach (GetUserModel follower in getUserModel.Followers)
                {
                    if (follower.Email == user.Email)
                    {
                        ViewBag.IsFollower = true;
                    }
                }

                if (getUserModel.Id == user.Id)
                {
                    return View("Profile", getUserModel);
                }
                else
                {
                    return View("Details", getUserModel);

                }
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Administrator", this.GetType().Name, "Edit", "user");

                GetUserModel getUserModel = await _moviemindAPIService.GetModel<GetUserModel>(id, "users");

                PutUserModel putUserModel = new PutUserModel
                {
                    FirstName = getUserModel.FirstName,
                    LastName = getUserModel.LastName,
                    Email = getUserModel.Email,
                    Roles = getUserModel.Roles
                };

                return View(putUserModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Edit(string id, PutUserModel putUserModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Administrator", this.GetType().Name, "Edit", "user");

                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                if (user.Id == Guid.Parse(id) || HttpContext.User.IsInRole("Editor"))
                {

                    if (ModelState.IsValid)
                    {
                        await _moviemindAPIService.PutModel<PutUserModel>(id, putUserModel, "users");

                        return RedirectToRoute(new { action = "Index", controller = "Users" });
                    }

                    return View(putUserModel);

                }
                else
                {
                    return RedirectToRoute(new { action = "Index", controller = "Users" });
                }
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(putUserModel));
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Administrator", this.GetType().Name, "Delete", "user");

                GetUserModel getUserModel = await _moviemindAPIService.GetModel<GetUserModel>(id, "users");  

                return View(getUserModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Delete(string id, GetUserModel getUserModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Administrator", this.GetType().Name, "Delete", "user");

                GetUserModel user = await _moviemindAPIService.GetModel<GetUserModel>(id, "users");

                foreach (GetReviewModel getReviewModel in user.Reviews)
                {
                    await _moviemindAPIService.DeleteModel(getReviewModel.Id.ToString(), "reviews");
                }

                //delete user favorites
                List<GetFavoriteModel> getFavoriteModels = await _moviemindAPIService.GetModels<GetFavoriteModel>("favorites");
                List<GetFavoriteModel> favoritesToDelete = getFavoriteModels.Where(x => x.UserId == user.Id).ToList();

                foreach (GetFavoriteModel favoriteModel in favoritesToDelete)
                {
                    await _moviemindAPIService.DeleteModel(favoriteModel.Id.ToString(), "favorites");
                }

                //delete user follower and followings
                List<GetUserFollowerModel> getUserFollowerModels = await _moviemindAPIService.GetModels<GetUserFollowerModel>("userfollowers");
                List<GetUserFollowerModel> userFollowersToDelete = getUserFollowerModels.Where(x => x.FollowerId == user.Id || x.FollowingId == user.Id).ToList();

                foreach (GetUserFollowerModel getUserFollowerModel in userFollowersToDelete)
                {
                    await _moviemindAPIService.DeleteModel(getUserFollowerModel.Id.ToString(), "userfollowers");
                }

                await _moviemindAPIService.DeleteModel(id, "users");

                return RedirectToRoute(new { action = "Index", controller = "Users" });
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(getUserModel));
            }
        }

        public IActionResult ChangePassword(string id)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "ChangePassword", "user");

                return View();
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> ChangePassword(string id, PatchUserModel patchUserModel)
        {
            try
            {
                AuthorizeHelper.Authorize(this.HttpContext, "Guest", this.GetType().Name, "ChangePassword", "user");

                if (patchUserModel.NewPassword != patchUserModel.ConfirmNewPassword)
                {
                    ModelState.AddModelError("ConfirmNewPassword", "Wachtwoorden komen niet overeen");
                }

                if (ModelState.IsValid)
                {
                    await _moviemindAPIService.PatchModel<PatchUserModel>(id, patchUserModel, "users");

                    return RedirectToRoute(new { action = "Index", controller = "Home" });
                }

                return View(patchUserModel);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e, this.View(patchUserModel));
            }
        }


        public async Task<IActionResult> AddFollower(string followingId)
        {
            try
            {
                var userId = HttpContext.Session.GetString("_Id");
                var user = await _moviemindAPIService.GetModel<GetUserModel>(userId, "users");

                PostUserFollowerModel postUserFollowerModel = new PostUserFollowerModel
                {
                    FollowingId = Guid.Parse(followingId),
                    FollowerId = user.Id
                };

                GetUserFollowerModel getUserFollowerModel = await _moviemindAPIService.PostModel<PostUserFollowerModel, GetUserFollowerModel>(postUserFollowerModel, "Userfollowers");
                return Redirect("/Users/Details/" + followingId);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }

        public async Task<IActionResult> RemoveFollower(string followingId)
        {
            try
            {
                List<GetUserFollowerModel> getUserFollowerModels = await _moviemindAPIService.GetModels<GetUserFollowerModel>("Userfollowers");

                foreach (var fol in getUserFollowerModels)
                {
                    if (fol.FollowingId == Guid.Parse(followingId))
                    {
                        await _moviemindAPIService.DeleteModel(fol.Id.ToString(), "userfollowers");
                    }
                }

                return Redirect("/Users/Details/" + followingId);
            }
            catch (MovieMindException e)
            {
                return ErrorHelper.HandleError(e);
            }
        }
    }
}
