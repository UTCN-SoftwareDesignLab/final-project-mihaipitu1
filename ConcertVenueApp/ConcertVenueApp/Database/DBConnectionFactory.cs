using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Database
{
    public class DBConnectionFactory
    {
        public DBConnectionWrapper GetConnectionWrapper(bool test)
        {
            if (test)
            {
                return new DBConnectionWrapper("concertvenue_test");
            }
            return new DBConnectionWrapper("concertvenue");
        }
    }
}
