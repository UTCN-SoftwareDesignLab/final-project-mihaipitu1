using ConcertVenueApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Repositories.Tickets
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        List<Ticket> FindTicketsByHolder(long user_id);
    }
}
