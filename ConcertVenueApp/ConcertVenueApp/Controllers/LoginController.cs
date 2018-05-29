using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using ConcertVenueApp.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConcertVenueApp.Controllers
{
    public class LoginController : Controller
    {
        private IAuthenticationService authService;
        private IAdminService adminService;

        public LoginController(IAuthenticationService authService,IAdminService adminService)
        {
            this.authService = authService;
            this.adminService = adminService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            var userNotification = authService.Login(Username, Password);
            if (userNotification.HasErrors() || userNotification.GetResult() == null)
            {
                ViewData["Errors"] = userNotification.GetErrors();
                return View("Error");
            }
            var user = userNotification.GetResult();
            switch (user.GetType())
            {
                case "admin": return RedirectToAction("Users", "User");
                case "user":
                    {
                        TempData["user"] = JsonConvert.SerializeObject(user);
                        return RedirectToAction("Events", "Venue");
                    }
                default:
                    return StatusCode(404);
            }
        }

        public ActionResult Error(List<string> errors)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            user.SetType("user");
            Notification<bool> notifier = adminService.Register(user);
            if (!notifier.GetResult())
            {
                ViewData["Errors"] = notifier.GetErrors();
                return View("ErrorRegister");
            }
            TempData["user"] = JsonConvert.SerializeObject(user);
            return RedirectToAction("Events", "Venue");
        }

        public ActionResult ErrorRegister(List<string> errors)
        {
            return View();
        }
    }
}