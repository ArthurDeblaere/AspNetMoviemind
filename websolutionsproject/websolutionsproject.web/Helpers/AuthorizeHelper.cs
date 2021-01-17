using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.shared.Exceptions;

namespace websolutionsproject.web.Helpers
{
    public class AuthorizeHelper
    {
        private static IStringLocalizerFactory _factory;
        private static LocalizerHelper helper;
        public static void Configure( IStringLocalizerFactory factory)
        {
            _factory = factory;
            helper = new LocalizerHelper(_factory);
        }
        public static void Authorize(HttpContext context, string role, string sourceClass, string sourceMethod, string sourceEntity)
        {
            if (!context.User.IsInRole(role))
            {
                string error = string.Empty;

                switch (sourceMethod)
                {
                    case "Index":
                        error = helper.LocalizeString("No permission to get all") + " " + helper.LocalizeString(sourceEntity);
                        break;
                    case "Details":
                        error = helper.LocalizeString("No permission to get details of") + " " + helper.LocalizeString(sourceEntity);
                        break;
                    case "Create":
                        error = helper.LocalizeString("No permission to create") + " " + helper.LocalizeString(sourceEntity);
                        break;
                    case "Edit":
                        error = helper.LocalizeString("No permission to edit") + " " + helper.LocalizeString(sourceEntity);
                        break;
                    case "Delete":
                        error = helper.LocalizeString("No permission to delete") + " " + helper.LocalizeString(sourceEntity);
                        break;
                }

                throw new ForbiddenException(error, sourceClass, sourceMethod, "403");
            }
        }
    }
}
