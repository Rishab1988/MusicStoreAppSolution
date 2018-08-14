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

    using MusicStoreApp.ViewModels;

    public class NewController : Controller
    {
        public IActionResult Index()
        {
            throw new InvalidOperationException();
            return this.Content("ghghgjhg");
        }
    }
    public class HomeController : Controller
    {
        private readonly string _connectionString;
        public HomeController(IOptions<SqlConnectionConfig> options)
        {
            _connectionString = options.Value.StoreDb;
        }
        public IActionResult Index()
        {
            throw new InvalidOperationException();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                this.ViewData.Add("SqlVersion", sqlConnection.ServerVersion);
            }

            return new ViewResult
            {
                ViewName = "Index",
                StatusCode = 200,
                ViewData = this.ViewData
            };
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLoginViewModel userLogin)
        {
            return this.Content(true.ToString());
        }
    }

}