using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AuthenticationController : Controller
    {

        private readonly MoviemindAPIService _moviemindAPIService;
        private readonly IStateManagementService _stateManagementService;

        public AuthenticationController(
            MoviemindAPIService moviemindAPIService,
            IStateManagementService stateManagementService)
        {
            _moviemindAPIService = moviemindAPIService;
            _stateManagementService = stateManagementService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SessionExpired") != null)
            {
                TempData["ApiError"] = HttpContext.Session.GetString("SessionExpired");
                HttpContext.Session.Remove("SessionExpired");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Index(PostAuthenticateRequestModel postAuthenticateRequestModel)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Send an API request to authenticate the new user
                    PostAuthenticateResponseModel postAuthenticateResponseModel = await _moviemindAPIService.Authenticate(postAuthenticateRequestModel);

                    _stateManagementService.SetState(postAuthenticateResponseModel);

                    // Redirect to the home page
                    return RedirectToRoute(new { action = "Index", controller = "Home" });
                }
                catch (MovieMindException e)
                {
                    TempData["ApiError"] = e.Message;
                }
            }

            return View(postAuthenticateRequestModel);
        }
        public IActionResult Logout()
        {
            _stateManagementService.ClearState();

            return RedirectToRoute(new { action = "Index", controller = "Home" });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {

            if (ModelState.IsValid)
            {
                ResetPasswordModel resetPasswordModel = await _moviemindAPIService.PostModel<ForgotPasswordModel, ResetPasswordModel>(forgotPasswordModel, "users/forgotpassword");
                return View("ResetPassword", resetPasswordModel);
            }

            return View(forgotPasswordModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {

            if (ModelState.IsValid)
            {
                await _moviemindAPIService.ResetPassword<ResetPasswordModel>(resetPasswordModel, "users/resetpassword");
                TempData["Confirmation"] = "Password successfully reset";
                return View("Index");
            }

            return View(resetPasswordModel);
        }
    }
}
