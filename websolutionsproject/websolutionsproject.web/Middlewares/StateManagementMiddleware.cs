using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using websolutionsproject.web.Services;

namespace websolutionsproject.web.Middlewares
{
    public class StateManagementMiddleware
    {
        private readonly RequestDelegate _next;
        public StateManagementMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IStateManagementService stateManagementService)
        {
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
