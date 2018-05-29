using System;
using System.Collections.Generic;
using System.Text;
using ConcertVenueApp.Database;
using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Builders;
using ConcertVenueApp.Repositories.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ConcertVenueApp.Tests.Repos
{
    [TestClass]
    public class EventRepositoryTest
    {
        private IEventRepository eventRepo;

        public EventRepositoryTest()
        {
            eventRepo = new EventRepositoryMySQL(new DBConnectionFactory().GetConnectionWrapper(true));
        }

        public void RemoveAll()
        {
            List<Event> users = eventRepo.FindAll();

            foreach (var user in users)
            {
                eventRepo.Delete(user);
            }
        }

        [TestMethod]
        public void TestFindAllUsers()
        {
            RemoveAll();
            List<Event> users = eventRepo.FindAll();
            Assert.AreEqual(users.Count, 0);
        }

        [TestMethod]
        public void TestCreateUser()
        {
            RemoveAll();
            Event user = new EventBuilder()
                .SetId(1)
                .SetTitle("title")
                .SetGenre("genre")
                .SetDate(DateTime.Now)
                .SetDescription("Des")
                .SetNoTickets(1)
                .SetPrice(1.0)
                .Build();
            Assert.IsTrue(eventRepo.Create(user));
            RemoveAll();
        }

        [TestMethod]
        public void TestEditUser()
        {
            RemoveAll();
            Event user = new EventBuilder()
                .SetId(1)
                .SetTitle("title")
                .SetGenre("genre")
                .SetDate(DateTime.Now)
                .SetDescription("Des")
                .SetNoTickets(1)
                .SetPrice(1.0)
                .Build();
            eventRepo.Create(user);
            Assert.IsTrue(eventRepo.Update(user));
            RemoveAll();
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            RemoveAll();
            Event user = new EventBuilder()
                .SetId(1)
                .SetTitle("title")
                .SetGenre("genre")
                .SetDate(DateTime.Now)
                .SetDescription("Des")
                .SetNoTickets(1)
                .SetPrice(1.0)
                .Build();
            eventRepo.Create(user);
            Assert.IsTrue(eventRepo.Delete(user));
            RemoveAll();
        }

        [TestMethod]
        public void TestFindEventById()
        {
            Event user = eventRepo.FindById(0);
            Assert.IsNull(user.GetTitle());
        }
    }
}
