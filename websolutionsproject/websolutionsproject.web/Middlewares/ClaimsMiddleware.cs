using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace websolutionsproject.web.Middlewares
{
    public class ClaimsMiddleware
    {
        private readonly RequestDelegate _next;

        public ClaimsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Session.GetString("_Roles")))
            {
                string[] roleNames = context.Session.GetString("_Roles").Split(',');

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, context.Session.GetString("_Id")));
                claims.Add(new Claim("FirstName", context.Session.GetString("_FirstName")));
                claims.Add(new Claim("LastName", context.Session.GetString("_LastName")));
                claims.Add(new Claim("UserName", context.Session.GetString("_UserName")));

                foreach (string roleName in roleNames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "JwtBearerDefaults.AuthenticationScheme");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                context.User = claimsPrincipal;
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
