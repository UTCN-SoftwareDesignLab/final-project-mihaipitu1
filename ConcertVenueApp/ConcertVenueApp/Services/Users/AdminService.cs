using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Services.Users
{
    public interface IAdminService
    {
        Notification<bool> Register(User user);

        bool UpdateUser(User user);

        bool DeleteUser(User user);

        List<User> GetUsers();

        User GetUserById(long id);
    }
}
