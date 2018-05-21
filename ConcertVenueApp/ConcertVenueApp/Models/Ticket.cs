using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models
{
    public class Ticket
    {
        private long id;
        private long user_id;
        private long event_id;
        private User ticketHolder;
        private Event ticketEvent;

        public long GetId()
        {
            return id;
        }
        public void SetId(long id)
        {
            this.id = id;
        }
        public long Id
        {
            get { return GetId(); }
            set { SetId(value); }
        }

        public long GetUserId()
        {
            return user_id;
        }
        public void SetUserId(long user_id)
        {
            this.user_id = user_id;
        }


        public long GetEventId()
        {
            return event_id;
        }
        public void SetEventId(long event_id)
        {
            this.event_id = event_id;
        }

        public User GetTicketHolder()
        {
            return ticketHolder;
        }
        public void SetTicketHolder(User ticketHodler)
        {
            this.ticketHolder = ticketHolder;
        }
        public User TicketHolder
        {
            get { return GetTicketHolder(); }
            set { SetTicketHolder(value); }
        }

        public Event GetTicketEvent()
        {
            return ticketEvent;
        }
        public void SetTicketEvent(Event ticketEvent)
        {
            this.ticketEvent = ticketEvent;
        }
        public Event TicketEvent
        {
            get { return GetTicketEvent(); }
            set { SetTicketEvent(value); }
        }
    }
}
