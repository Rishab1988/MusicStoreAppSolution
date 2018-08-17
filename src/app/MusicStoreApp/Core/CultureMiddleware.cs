using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MusicStoreApp.Core
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Response.HasStarted)
            {
                CookieBuilder cookieBuilder = new CookieBuilder { Name = ".AspNetCore.Culture" };
                 var cookieOptions = cookieBuilder.Build(httpContext);
                httpContext.Response.Cookies.Append(cookieBuilder.Name, "c=fr-FR|uic=fr-Fr", cookieOptions);
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }
}
