using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2Passengers
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passport { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public bool HasDeparted { get; set; }
    }
}
