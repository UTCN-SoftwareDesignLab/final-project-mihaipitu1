using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Services.Events;
using ConcertVenueApp.Services.Tickets;
using ConcertVenueApp.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertVenueApp.Controllers
{
    public class StoreController : Controller
    {
        private IEventService eventService;
        private IAdminService adminService;
        private ITicketService ticketService;

        public StoreController(IEventService eventService,IAdminService adminService,ITicketService ticketService)
        {
            this.eventService = eventService;
            this.adminService = adminService;
            this.ticketService = ticketService;
        }
        public ActionResult Events()
        {
            var events = eventService.GetEvents();
            return View(events);
        }

        public ActionResult Details(long id)
        {
            var ev = eventService.GetEventById(id);
            return View(ev);
        }
    }
}