using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MusicStoreApp.Core;
using MusicStoreApp.DI;
using Newtonsoft.Json;

namespace MusicStoreApp
{
    using System.Globalization;
    using System.Net;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddConfiguration(configuration);
            builder.AddJsonFile(source =>
            {
                source.Path = "Menu.json";
                source.ReloadOnChange = true;
                source.Optional = true;
            });
            _configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            /*
            options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                    factory.Create(typeof(SharedResource));
            }
            */
            services.AddLocalization(
                setupAction =>
                    {
                        setupAction.ResourcesPath = "Resources";
                    });

            services.AddMvc().AddViewLocalization(
            setupAction =>
                {
                    setupAction.ResourcesPath = "Resources";
                })
                .AddDataAnnotationsLocalization(

        //options =>
        //    {
        //        options.DataAnnotationLocalizerProvider = (type, factory) =>
        //            factory.Create(typeof(SharedResource));
        //    }




        );

            services.Configure<AppMenuComponent>(_configuration.GetSection("Menu"));
            services.Configure<SqlConnectionConfig>(_configuration.GetSection("ConnectionStrings"));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                options =>
                    {
                        options.Cookie.Name = "MusicStoreApp";
                        options.LoginPath = "/Home/Login";
                        options.AccessDeniedPath = "/Home/Denied";
                        options.ExpireTimeSpan = new TimeSpan(0, 0, 60);
                    }
                );
            return AutoFacContainer.GetServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppMenuComponent> appMenu, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation(new EventId(5001), env.EnvironmentName);
                app.UseDeveloperExceptionPage();
            }
            //app.UseCultureMiddleware();
            app.UseRequestLocalization(
                options =>
                {
                    options.SupportedCultures = new List<CultureInfo>
                                                    {
                                                        new CultureInfo("en-US"), new CultureInfo ("fr-FR")
                                                    };
                    options.SupportedUICultures = options.SupportedCultures;
                    options.DefaultRequestCulture = new RequestCulture("en-US");
                });


            var x = CultureInfo.CurrentCulture;
            //app.UseExceptionHandler("/Home/Error");
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.Use(
                async (context, next) =>
                    {
                        if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                        {
                            logger.LogWarning("404 Error");
                        }

                        try
                        {
                            await next.Invoke();
                            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                            {
                                logger.LogWarning("404 Error");
                                context.Response.StatusCode = (int)HttpStatusCode.Redirect;
                                context.Response.Redirect("/Home/Index");
                            }
                        }
                        catch (Exception e)
                        {
                            logger.LogError(e.ToString());
                        }
                    });
            app.UseMvc(routes =>
             {
                 routes.MapRoute("default", "{controller}/{action}", new { controller = "Home", action = "Index" });
             });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
