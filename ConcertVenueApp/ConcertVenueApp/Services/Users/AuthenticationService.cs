using ConcertVenueApp.Models;
using ConcertVenueApp.Models.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Services.Users
{
    public interface IAuthenticationService
    {
        Notification<User> Login(string username, string password);
    }
}
