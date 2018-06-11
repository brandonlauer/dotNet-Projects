using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz1Flights
{
    class FlightsDBContext : DbContext
    {
        public FlightsDBContext() : base("name=EFFlightsDB") { }
        public virtual DbSet<Flight> Flights { get; set; }
    }
}
