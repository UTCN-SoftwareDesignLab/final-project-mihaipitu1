using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConcertVenueApp.Database;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Builders;
using MySql.Data.MySqlClient;

namespace ConcertVenueApp.Repositories.Tickets
{
    public class TicketRepositoryMySQL : ITicketRepository
    {
        private DBConnectionWrapper connectionWrapper;

        public TicketRepositoryMySQL(DBConnectionWrapper connectionWrapper)
        {
            this.connectionWrapper = connectionWrapper;
        }

        public bool Create(Ticket t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Insert into ticket (id, user_id, event_id) VALUES('{0}', '{1}', '{2}'); ", t.GetId(), t.GetUserId(), t.GetEventId());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        public bool Delete(Ticket t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Delete from ticket where id ='{0}';", t.GetId());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        public List<Ticket> FindAll()
        {
            List<Ticket> tickets = new List<Ticket>();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from ticket");
                    MySqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        tickets.Add(BuildFromReader(reader));
                    }
                }
                connection.Close();
            }
            return tickets;
        }

        public Ticket FindById(long id)
        {
            Ticket ticket = new Ticket();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from ticket where id = '{0}'",id);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ticket = BuildFromReader(reader);
                    }
                }
                connection.Close();
            }
            return ticket;
        }

        public List<Ticket> FindTicketsByHolder(long user_id)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Select * from ticket where user_id = '{0}'",user_id);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        tickets.Add(BuildFromReader(reader));
                    }
                }
                connection.Close();
            }
            return tickets;
        }

        public bool Update(Ticket t)
        {
            if (t == null)
                return false;
            using (MySqlConnection connection = connectionWrapper.GetConnection())
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = String.Format("Update ticket user_id = '{0}', event_id = '{1}' where id= '{2}'; ", t.GetUserId(), t.GetEventId(), t.GetId());
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return true;
        }

        private Ticket BuildFromReader(MySqlDataReader reader)
        {
            return new TicketBuilder()
                .SetId(reader.GetInt64(0))
                .SetHolderId(reader.GetInt64(1))
                .SetEventId(reader.GetInt64(2))
                .Build();
        }
    }
}
