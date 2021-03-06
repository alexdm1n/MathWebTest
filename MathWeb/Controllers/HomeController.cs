using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MathWeb.Models;
using MathWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MathWeb.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MathWeb.Controllers
{
    public class HomeController : Controller
    {

      
        private readonly ILogger<HomeController> _logger;
        private readonly TasksToTable tasksToTable;
        public HomeController(ILogger<HomeController> logger, TasksToTable tasksToTable)
        {
            _logger = logger;
            this.tasksToTable = tasksToTable;
        }
        public IActionResult Index(string SearchString, string FilterString)
        {
            var model = tasksToTable.GetTasks();
            if(!String.IsNullOrEmpty(FilterString))
            {
                model = model.Where(s => s.Topic.Contains(FilterString));
            }
            if(!String.IsNullOrEmpty(SearchString))
            {
                model = model.Where(s => s.NameOfTask.Contains(SearchString));
            }
            return View(model);
           
        }
        public IActionResult MyAccount(string name)
        {
            var model = tasksToTable.GetByOwner(name);

            if (!String.IsNullOrEmpty(name))
            {
                model = model.Where(s => s.WhoMade == name);
            }
            return View(model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact page.";

            return View();
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task LoginWithFacebook()
        {
            await HttpContext.ChallengeAsync(FacebookDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("FacebookResponce")
            });
        }

        public async Task LoginWithGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponce")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }

        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            return Json(claims);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }

    }

}
