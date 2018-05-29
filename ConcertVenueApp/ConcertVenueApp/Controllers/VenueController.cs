using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Builders;
using ConcertVenueApp.Services.Events;
using ConcertVenueApp.Services.Tickets;
using ConcertVenueApp.Services.Users;
using ConcertVenueApp.Utilities.EMail;
using ConcertVenueApp.Utilities.FileGenerator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConcertVenueApp.Controllers
{
    public class VenueController : Controller
    {
        private IEventService eventService;
        private IAdminService adminService;
        private ITicketService ticketService;

        public VenueController(IEventService eventService,IAdminService adminService,ITicketService ticketService)
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

        public ActionResult BuyTicket(long id)
        {
            Ticket ticket = new TicketBuilder()
                .SetEventId(id)
                .Build();
            if(TempData["user"] != null)
            {
                var userJson = JsonConvert.DeserializeObject(TempData["user"].ToString());
                JToken token = JObject.Parse(userJson.ToString());
                ticket.SetUserId((long)token.SelectToken("Id"));
                User user = new UserBuilder()
                    .SetId((long)token.SelectToken("Id"))
                    .SetUsername((string)token.SelectToken("Username"))
                    .Build();
                TempData["user"] = JsonConvert.SerializeObject(user);
            }
            if(ticket.GetUserId() == 0)
            {
                return StatusCode(404);
            }
            ticketService.CreateTicket(ticket);
            return RedirectToAction("UserTickets");
        }
        
        public ActionResult UserTickets()
        {
            if (TempData["user"] != null)
            {
                var userJson = JsonConvert.DeserializeObject(TempData["user"].ToString());
                JToken token = JObject.Parse(userJson.ToString());
                User user = new UserBuilder()
                                   .SetId((long)token.SelectToken("Id"))
                                   .SetUsername((string)token.SelectToken("Username"))
                                   .Build();
                TempData["user"] = JsonConvert.SerializeObject(user);
                var tickets = ticketService.GetTicketsByUser(user.GetId());
                return View(tickets);
            }
            return StatusCode(404);
        }

        public ActionResult SendTickets()
        {
            if (TempData["user"] != null)
            {
                var userJson = JsonConvert.DeserializeObject(TempData["user"].ToString());
                JToken token = JObject.Parse(userJson.ToString());
                User user = new UserBuilder()
                                   .SetId((long)token.SelectToken("Id"))
                                   .SetUsername((string)token.SelectToken("Username"))
                                   .Build();
                TempData["user"] = JsonConvert.SerializeObject(user);
                var tickets = ticketService.GetTicketsByUser(user.GetId());
                FileStream file = PdfFileGenerator.GeneratePdfTickets(tickets);
                EmailCreation.SendEMail(file.Name, user.GetUsername());
                return RedirectToAction("Events");
            }
            return StatusCode(404);
        }
    }
}