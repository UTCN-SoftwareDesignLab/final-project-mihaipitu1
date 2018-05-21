using ConcertVenueApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Services.Tickets
{
    public interface ITicketService
    {
        bool CreateTicket(Ticket ticket);

        bool DeleteTicket(Ticket ticket);

        bool UpdateTicket(Ticket ticket);

        List<Ticket> GetTickets();

        Ticket GetTicketById(long id);

        List<Ticket> GetTicketsByUser(long user_id);
    }
}
