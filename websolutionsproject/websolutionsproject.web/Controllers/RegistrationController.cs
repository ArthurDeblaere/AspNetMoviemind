using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using websolutionsproject.models.Users;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class RegistrationController : Controller
    {
        private readonly MoviemindAPIService _moviemindAPIService;
        private readonly IStateManagementService _stateManagementService;
        private readonly IStringLocalizer<RegistrationController> _localizer;

        public RegistrationController(
            MoviemindAPIService moviemindAPIService,
            IStateManagementService stateManagementService,
            IStringLocalizer<RegistrationController> localizer)
        {
            _moviemindAPIService = moviemindAPIService;
            _stateManagementService = stateManagementService;
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents XSRF/CSRF attacks
        public async Task<IActionResult> Index(PostUserModel postUserModel, string rememberMe)
        {
            if (postUserModel.Password != postUserModel.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", _localizer["Passwords are not the same"]);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (postUserModel.Roles == null)
                    {
                        postUserModel.Roles = new List<String>
                        {
                            "Guest"
                        };
                    }
                    else if (postUserModel.Roles.Count == 0)
                    {
                        postUserModel.Roles.Add("Guest");

                    }
                    // Send an API request to create the new user
                    GetUserModel getUserModel = await _moviemindAPIService.PostModel<PostUserModel, GetUserModel>(postUserModel, "users");
              
                    // When the user was successfully created send an API request to authenticate the new user
                    PostAuthenticateRequestModel postAuthenticateRequestModel = new PostAuthenticateRequestModel
                    {
                        UserName = postUserModel.UserName,
                        Password = postUserModel.Password,
                    };

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

            return View(postUserModel);
        }
    }
}
