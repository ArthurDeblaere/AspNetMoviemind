using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Middlewares
{
    public static class StateManagementMiddlewareExtensions
    {
        public static IApplicationBuilder UseStateManagement(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<StateManagementMiddleware>();
        }
    }
}
