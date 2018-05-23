using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using ConcertVenueApp.Repositories.Events;

namespace ConcertVenueApp.Services.Events
{
    public class EventServiceMySQL : IEventService
    {
        private IEventRepository eventRepo;

        public EventServiceMySQL(IEventRepository eventRepo)
        {
            this.eventRepo = eventRepo;
        }

        public Notification<bool> CreateEvent(Event ev)
        {
            ev.SetId(GetMaxId() + 1);
            EventValidator validator = new EventValidator(ev);
            bool valid = validator.Validate();
            Notification<bool> notifier = new Notification<bool>();
            if(!valid)
            {
                foreach(var error in validator.GetErrors())
                {
                    notifier.AddError(error);
                }
                notifier.SetResult(false);
            }
            else
            {
                notifier.SetResult(eventRepo.Create(ev));
            }
            return notifier;
        }

        public bool DeleteEvent(Event ev)
        {
            return eventRepo.Delete(ev);
        }

        public Event GetEventById(long id)
        {
            return eventRepo.FindById(id);
        }

        public List<Event> GetEvents()
        {
            return eventRepo.FindAll();
        }

        public List<Event> GetEventsByDescription(string description)
        {
            return eventRepo.FindByDescription(description);
        }

        public bool UpdateEvent(Event ev)
        {
            return eventRepo.Update(ev);
        }

        private long GetMaxId()
        {
            long id = 0;
            List<Event> events = GetEvents();
            foreach(var ev in events)
            {
                if (ev.Id > id)
                    id = ev.Id;
            }
            return id;
        }
    }
}
