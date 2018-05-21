using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Services.Tickets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConcertVenueApp.Controllers
{
    public class TicketController : Controller
    {
        private ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        public ActionResult Tickets()
        {
            var tickets = ticketService.GetTickets();
            return View(tickets);
        }
    }
}