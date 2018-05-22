using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Services.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertVenueApp.Controllers
{
    public class EventController : Controller
    {
        private IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        // GET: Event
        public ActionResult Events()
        {
            var events = eventService.GetEvents();
            return View(events);
        }

        // GET: Event/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event ev)
        {
            eventService.CreateEvent(ev);
            return RedirectToAction("Events");
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
            var ev = eventService.GetEventById(id);
            return View(ev);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event ev)
        {
            eventService.UpdateEvent(ev);
            return RedirectToAction("Events");
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int id)
        {
            var ev = eventService.GetEventById(id);
            return View(ev);
        }

        // POST: Event/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Event ev)
        {
            eventService.DeleteEvent(ev);
            return RedirectToAction("Events");
        }
    }
}