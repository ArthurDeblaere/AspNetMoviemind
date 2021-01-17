using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Middlewares
{
    public static class ClaimsMiddlewareExtensions
    {
        public static IApplicationBuilder UseClaims(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClaimsMiddleware>();
        }
    }
}
