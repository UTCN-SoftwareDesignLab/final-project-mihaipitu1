using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models
{
    public class Event
    {
        private long id;
        private string title;
        private string genre;
        private DateTime date;
        private string description;
        private long noTickets;
        private double ticketPrice;

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

        public string GetTitle()
        {
            return title;
        }
        public void SetTitle(string title)
        {
            this.title = title;
        }
        public string Title
        {
            get { return GetTitle(); }
            set { SetTitle(value); }
        }

        public string GetGenre()
        {
            return genre;
        }
        public void SetGenre(string genre)
        {
            this.genre = genre;
        }
        public string Genre
        {
            get { return GetGenre(); }
            set { SetGenre(value); }
        }

        public DateTime GetDate()
        {
            return date;
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public DateTime Date
        {
            get { return GetDate(); }
            set { SetDate(value); }
        }

        public string GetDescription()
        {
            return description;
        }
        public void SetDescription(string description)
        {
            this.description = description;
        }
        public string Description
        {
            get { return GetDescription(); }
            set { SetDescription(value); }
        }

        public long GetNoTickets()
        {
            return noTickets;
        }
        public void SetNoTickets(long noTickets)
        {
            this.noTickets = noTickets;
        }
        public long NoTickets
        {
            get { return GetNoTickets(); }
            set { SetNoTickets(value); }
        }

        public double GetTicketPrice()
        {
            return ticketPrice;
        }
        public void SetTicketPrice(double ticketPrice)
        {
            this.ticketPrice = ticketPrice;
        }
        public double TicketPrice
        {
            get { return GetTicketPrice(); }
            set { SetTicketPrice(value); }
        }
    }
}
