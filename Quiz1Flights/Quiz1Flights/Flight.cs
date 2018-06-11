using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz1Flights
{
    public class Flight
    {
        [Key]
        public long Id { get; set; }

        public DateTime OuDay { get; set; }

        [MaxLength (5)]
        public string FromCode { get; set; }

        [MaxLength (5)]
        public string ToCode { get; set; }

        public FlightType Type { get; set; }

        public int Passengers { get; set; }

        public enum FlightType
        {
            Domestic,
            International,
            Private
        }

        public override string ToString()
        {
            return string.Format("{0}: {1} flight departing {2} from {3} to {4} with {5} passengers on board.", Id, Type.ToString(), OuDay.ToShortDateString(), FromCode, ToCode, Passengers);
        }
    }
}
