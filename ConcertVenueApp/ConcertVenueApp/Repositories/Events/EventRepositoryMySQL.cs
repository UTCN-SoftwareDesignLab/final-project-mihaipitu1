using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Database;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Builders;
using MySql.Data.MySqlClient;

namespace ConcertVenueApp.Repositories.Events
{
    public class EventRepositoryMySQL : IEventRepository
    {
        private DBConnectionWrapper connectionWrapper;

        public EventRepositoryMySQL(DBConnectionWrapper connectionWrapper)
        {
            this.connectionWrapper = connectionWrapper;
        }

        public bool Create(Event t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Insert into event (id, title, genre, date, description, noTickets, ticketPrice) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'); ", t.GetId(), t.GetTitle(), t.GetGenre(), t.GetDate().ToString("yyyy-MM-dd HH:mm:ss"), t.GetDescription(),t.GetNoTickets(),t.GetTicketPrice());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        public bool Delete(Event t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Delete from event where id = '{0}';", t.GetId());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        public List<Event> FindAll()
        {
            List<Event> events = new List<Event>();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from event");
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        events.Add(BuildFromReader(reader));
                    }
                }
                connection.Close();
            }
            return events;
        }

        public List<Event> FindByDescription(string description)
        {
            List<Event> events = new List<Event>();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from event WHERE title LIKE '%{0}%' or genre LIKE '{0}' or description LIKE '{0}'",description);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        events.Add(BuildFromReader(reader));
                    }
                }
                connection.Close();
            }
            return events;
        }

        public Event FindById(long id)
        {
            Event ev = new Event();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from event where id = '{0}'",id);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ev = BuildFromReader(reader);
                    }
                }
                connection.Close();
            }
            return ev;
        }

        public bool Update(Event t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Update event SET title='{0}', genre = '{1}', date = '{2}', description = '{3}', noTickets = '{4}' , ticketPrice = '{5}' where id = '{6}';", t.GetTitle(), t.GetGenre(), t.GetDate().ToString("yyyy-MM-dd HH:mm:ss"), t.GetDescription(), t.GetNoTickets(), t.GetTicketPrice(),t.GetId());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        private Event BuildFromReader(MySqlDataReader reader)
        {
            return new EventBuilder()
                .SetId(reader.GetInt64(0))
                .SetTitle(reader.GetString(1))
                .SetGenre(reader.GetString(2))
                .SetDate(reader.GetDateTime(3))
                .SetDescription(reader.GetString(4))
                .SetNoTickets(reader.GetInt64(5))
                .SetPrice(reader.GetDouble(6))
                .Build();
        }
    }
}
