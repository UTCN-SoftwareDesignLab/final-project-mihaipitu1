using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Repositories.Tickets;

namespace ConcertVenueApp.Services.Tickets
{
    public class TicketServiceMySQL : ITicketService
    {
        private ITicketRepository ticketRepo;

        public TicketServiceMySQL(ITicketRepository ticketRepo)
        {
            this.ticketRepo = ticketRepo;
        }

        public bool CreateTicket(Ticket ticket)
        {
            ticket.SetId(GetMaxId() + 1);
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
            return ticketRepo.FindAll();
        }

        public List<Ticket> GetTicketsByUser(long user_id)
        {
            return ticketRepo.FindTicketsByHolder(user_id);
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
