﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using ConcertVenueApp.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertVenueApp.Controllers
{
    public class UserController : Controller
    {
        private IAdminService adminService;

        public UserController(IAdminService adminService)
        {
            this.adminService = adminService;
        }
        public ActionResult Index()
        {
            var users = adminService.GetUsers();
            return View(users);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            Notification<bool> notifier = adminService.Register(user);
            if (!notifier.GetResult())
            {
                ViewData["Errors"] = notifier.GetErrors();
                return View("Error");
            }
            return RedirectToAction("Index", "User");
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = adminService.GetUserById(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                adminService.UpdateUser(user);
                return RedirectToAction("Index", "User");
            }

            return StatusCode(400);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = adminService.GetUserById(id);
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {
            if (ModelState.IsValid)
            {
                bool x = adminService.DeleteUser(user);
                return RedirectToAction("Index", "User");
            }
            return StatusCode(404);
        }
    }
}