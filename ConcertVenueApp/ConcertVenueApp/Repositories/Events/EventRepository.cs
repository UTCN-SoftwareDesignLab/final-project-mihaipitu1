using ConcertVenueApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Repositories.Events
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        List<Event> FindByDescription(string description);
    }
}
