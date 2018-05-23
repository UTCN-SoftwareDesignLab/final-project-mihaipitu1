using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Services.Events
{
    public interface IEventService
    {
        Notification<bool> CreateEvent(Event ev);

        bool DeleteEvent(Event ev);

        bool UpdateEvent(Event ev);

        List<Event> GetEvents();

        Event GetEventById(long id);

        List<Event> GetEventsByDescription(string description);
    }
}
