using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.web.Helpers
{
    public class ErrorHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        private static ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private static IStringLocalizerFactory _factory;
        private static LocalizerHelper helper;

        public static void Configure(
            IHttpContextAccessor httpContextAccessor,
            ITempDataDictionaryFactory tempDataDictionaryFactory,
            IStringLocalizerFactory factory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _factory = factory;
            helper = new LocalizerHelper(_factory);
        }

        public static IActionResult HandleError(MovieMindException e)
        {
            ITempDataDictionary tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
            tempData["ApiError"] = helper.LocalizeString(e.Message);

            if (e.MovieMindError.Status == "401")
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authentication" }));
            };
            
            var routeValue = new RouteValueDictionary(new { action = "Index", controller = "Home" });

            return new RedirectToRouteResult(routeValue);
        }

        public static IActionResult HandleError(MovieMindException e, ViewResult view)
        {
            ITempDataDictionary tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
            tempData["ApiError"] = helper.LocalizeString(e.Message);

            if (e.MovieMindError.Status == "401")
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authentication" }));
            };

            var routeValue = new RouteValueDictionary(new { action = "Index", controller = "Home" });

            if (e.MovieMindError.Type.Equals("DatabaseException") ||
                e.MovieMindError.Type.Equals("EntityException") ||
                e.MovieMindError.Type.Equals("CollectionException") ||
                e.MovieMindError.Type.Equals("IdentityException"))
            {
                return view;
            }

            return new RedirectToRouteResult(routeValue);
        }

        public static IActionResult HandleError(MovieMindException e, string targetController, string targetActionMethod)
        {
            ITempDataDictionary tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
            tempData["ApiError"] = helper.LocalizeString(e.Message);

            if (e.MovieMindError.Status == "401")
            {
                return new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Authentication" }));
            };

            var routeValue = new RouteValueDictionary(new { action = "Index", controller = "Home" });

            return new RedirectToRouteResult(routeValue);
        }
    }
}
