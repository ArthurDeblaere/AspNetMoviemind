using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.models.Users;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, MoviemindAPIService movieMindAPIService, IStateManagementService stateManagementService)
        {
            // Token validation is not required for authentication and registration
            if (!context.Request.Path.Value.Contains("Registration") && !context.Request.Path.Value.Contains("Authentication"))
            {
                // Check if JWT token exists in session, if not redirect to login page
                if (string.IsNullOrEmpty(context.Session.GetString("_JwtToken")))
                {
                    context.Response.Redirect("/Authentication");
                    return;
                }
                else
                {
                    var expiresOn = Convert.ToDateTime(context.Session.GetString("_JwtExpiresOn"));
                    // JWT exist in session, check if expired
                    if (expiresOn < DateTime.UtcNow)
                    {
                        // Unnecesary because check is done at back-end but this client-side check saves an API call
                        // Check if refresh token is expired
                        // If expired the user needs to authenticate with credentials
                        var rtexpireson = Convert.ToDateTime(context.Session.GetString("_RtExpiresOn"));
                        if (rtexpireson < DateTime.UtcNow)
                        {
                            context.Session.SetString("SessionExpired", "Your session is expired");
                            context.Response.Redirect("/Authentication");
                            return;

                            //throw new TokenException("Uw sessie is verlopen", "TokenValidationMiddleware", "InvokeAsync", "401");
                        }
                        else
                        {
                            // If not expired a request to /api/Users/refresh-token is required to get a new set of tokens
                            PostAuthenticateResponseModel postAuthenticateResponseModel = await movieMindAPIService.RefreshToken();

                            // Update the session data with the new set of tokens
                            stateManagementService.SetState(postAuthenticateResponseModel);
                        }
                    }
                }
            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
