using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace websolutionsproject.web.Services
{
    public static class MovieMindServiceCollectionExtensions
    {        public static IServiceCollection AddMovieMind(this IServiceCollection services)
        {
            services.AddScoped<IStateManagementService, StateManagementService>();

            return services;

        }
    }
}
