using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Models.Builders
{
    public interface IBuilder<T>
    {
       T Build();
    }
}
