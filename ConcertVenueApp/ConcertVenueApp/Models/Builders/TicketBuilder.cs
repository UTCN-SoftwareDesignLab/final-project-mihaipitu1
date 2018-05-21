using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models.Builders
{
    public class TicketBuilder : IBuilder<Ticket>
    {
        private Ticket ticket;

        public TicketBuilder()
        {
            ticket = new Ticket();
        }

        public TicketBuilder SetId(long id)
        {
            ticket.SetId(id);
            return this;
        }

        public TicketBuilder SetHolderId(long id)
        {
            ticket.SetUserId(id);
            return this;
        }

        public TicketBuilder SetEventId(long id)
        {
            ticket.SetEventId(id);
            return this;
        }

        public Ticket Build()
        {
            return ticket;
        }
    }
}
