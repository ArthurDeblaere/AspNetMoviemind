using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using websolutionsproject.shared.Exceptions;
using websolutionsproject.web.Handlers;
using websolutionsproject.web.Helpers;
using websolutionsproject.web.Middlewares;
using websolutionsproject.web.Services;

namespace websolutionsproject.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo> { };
                var nl = new CultureInfo("nl");
                var en = new CultureInfo("en")
                {
                    DateTimeFormat = nl.DateTimeFormat
                };

                supportedCultures.Add(nl);
                supportedCultures.Add(en);


                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            // Session configuration
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = "MovieMind.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<ValidateHeaderHandler>();

            services.AddHttpClient<MoviemindAPIService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:44346/api/");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddHttpMessageHandler<ValidateHeaderHandler>();

            // Set default localization resources path
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            // Default configuration with additional JsonSerializerOptions and SessionStateTempDataProvider configured
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new MovieMindExceptionConverter());
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddSessionStateTempDataProvider()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(models.Actors.BaseActorModel).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });
            // Get access to the HttpContext in views and business logic
            services.AddHttpContextAccessor();

            services.AddMovieMind();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            // Redirect to Home when route not found
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/");
                    return;
                }
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();

            // Localization
            var requestLocalizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestLocalizationOptions.Value);

            app.UseAuthorization();

            app.UseSession();

            // Start Custom middlewares
            app.UseStateManagement();
            app.UseTokenValidation();
            app.UseClaims();

            ErrorHelper.Configure(app.ApplicationServices.GetService<IHttpContextAccessor>(), app.ApplicationServices.GetService<ITempDataDictionaryFactory>(), app.ApplicationServices.GetService<IStringLocalizerFactory>());
            AuthorizeHelper.Configure(app.ApplicationServices.GetService<IStringLocalizerFactory>());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
