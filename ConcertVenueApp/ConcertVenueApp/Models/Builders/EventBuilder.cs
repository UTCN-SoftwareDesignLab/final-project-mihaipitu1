using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models.Builders
{
    public class EventBuilder : IBuilder<Event>
    {
        private Event eventName;

        public EventBuilder()
        {
            eventName = new Event();
        }

        public EventBuilder SetId(long id)
        {
            eventName.SetId(id);
            return this;
        }

        public EventBuilder SetTitle(string title)
        {
            eventName.SetTitle(title);
            return this;
        }

        public EventBuilder SetGenre(string genre)
        {
            eventName.SetGenre(genre);
            return this;
        }


        public EventBuilder SetDate(DateTime date)
        {
            eventName.SetDate(date);
            return this;
        }

        public EventBuilder SetDescription(string description)
        {
            eventName.SetDescription(description);
            return this;
        }

        public EventBuilder SetNoTickets(long noTickets)
        {
            eventName.SetNoTickets(noTickets);
            return this;
        }

        public EventBuilder SetPrice(double price)
        {
            eventName.SetTicketPrice(price);
            return this;
        }

        public Event Build()
        {
            return eventName;
        }
    }
}
