using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Options;
using MusicStoreApp.Core;

namespace MusicStoreApp.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;

    using MusicStoreApp.ViewModels;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly string _connectionString;

        public HomeController(IOptions<SqlConnectionConfig> options)
        {
            _connectionString = options.Value.StoreDb;
        }

        public IActionResult Index()
        {
            //throw new InvalidOperationException();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                this.ViewData.Add("SqlVersion", sqlConnection.ServerVersion);
            }

            var mobileClaim = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.MobilePhone);
            if (mobileClaim != null)
            {
                ViewData.Add(mobileClaim.Type, mobileClaim.Value);
            }


            return new ViewResult { ViewName = "Index", StatusCode = 200, ViewData = this.ViewData };
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(UserLoginViewModel userLogin)
        {
            if (!ModelState.IsValid)
            {
                return View(userLogin);
            }

            if (userLogin.UserName != "test" || userLogin.Password != "test")
            {
                ModelState.AddModelError("InvalidUserNamePassword", "Either the user name or password is invalid");
                return View(userLogin);
            }

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                    new ClaimsIdentity(
                        new List<Claim>() { new Claim(ClaimTypes.MobilePhone, "1234567890") },
                        CookieAuthenticationDefaults.AuthenticationScheme));
            HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index", "Home");
        }
    }
}