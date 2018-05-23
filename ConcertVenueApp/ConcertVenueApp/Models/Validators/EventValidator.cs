using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models.Validators
{
    public class EventValidator
    {
        private List<string> errors;
        private Event ev;

        public EventValidator(Event ev)
        {
            this.ev = ev;
            errors = new List<string>();
        }

        public List<string> GetErrors()
        {
            return errors;
        }

        public bool Validate()
        {
            ValidateDate(ev.GetDate());
            ValidateNoTickets(ev.GetNoTickets());
            ValidatePrice(ev.GetTicketPrice());
            return errors.Capacity == 0;
        }

        private void ValidateDate(DateTime date)
        {
            if( date < DateTime.Now)
            {
                errors.Add("Error! Cannot add event in the past.");
            }
        }

        private void ValidateNoTickets(long noTickets)
        {
            if(noTickets <=0)
            {
                errors.Add("Error! Cannot create event with no tickets.");
            }
        }

        private void ValidatePrice(double price)
        {
            if(price <= 0.0)
            {
                errors.Add("Error! Cannot create event with free tickets. :(");
            }
        }
    }
}
