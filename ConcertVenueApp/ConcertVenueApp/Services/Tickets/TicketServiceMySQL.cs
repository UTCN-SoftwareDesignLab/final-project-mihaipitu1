using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Repositories.Events;
using ConcertVenueApp.Repositories.Tickets;
using ConcertVenueApp.Repositories.Users;

namespace ConcertVenueApp.Services.Tickets
{
    public class TicketServiceMySQL : ITicketService
    {
        private ITicketRepository ticketRepo;
        private IUserRepository userRepo;
        private IEventRepository eventRepo;

        public TicketServiceMySQL(ITicketRepository ticketRepo,IUserRepository userRepo,IEventRepository eventRepo)
        {
            this.ticketRepo = ticketRepo;
            this.userRepo = userRepo;
            this.eventRepo = eventRepo;
        }

        public bool CreateTicket(Ticket ticket)
        {
            ticket.SetId(GetMaxId() + 1);
            Event ev = eventRepo.FindById(ticket.GetEventId());
            ev.SetNoTickets(ev.GetNoTickets() - 1);
            eventRepo.Update(ev);
            return ticketRepo.Create(ticket);
        }

        public bool DeleteTicket(Ticket ticket)
        {
            return ticketRepo.Delete(ticket);
        }

        public Ticket GetTicketById(long id)
        {
            return ticketRepo.FindById(id);
        }

        public List<Ticket> GetTickets()
        {
            var ti = ticketRepo.FindAll();
            List<Ticket> tickets = new List<Ticket>();
            foreach(var t in ti)
            {
                t.SetTicketEvent(eventRepo.FindById(t.GetEventId()));
                t.SetTicketHolder(userRepo.FindById(t.GetUserId()));
                tickets.Add(t);
            }
            return tickets;
        }

        public List<Ticket> GetTicketsByUser(long user_id)
        {
            var ti = ticketRepo.FindTicketsByHolder(user_id);
            List<Ticket> tickets = new List<Ticket>();
            foreach (var t in ti)
            {
                t.SetTicketEvent(eventRepo.FindById(t.GetEventId()));
                t.SetTicketHolder(userRepo.FindById(t.GetUserId()));
                tickets.Add(t);
            }
            return tickets;
        }

        public bool UpdateTicket(Ticket ticket)
        {
            return ticketRepo.Update(ticket);
        }

        private long GetMaxId()
        {
            long id = 0;
            List<Ticket> events = GetTickets();
            foreach (var ev in events)
            {
                if (ev.Id > id)
                    id = ev.Id;
            }
            return id;
        }
    }
}
