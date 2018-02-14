using System;
using System.Collections.Generic;
using System.Linq;

namespace Flights.Domain
{
    /// <summary>
    /// Flights filter which filters out flights with at least one segment with arrival date sonner than departure date.
    /// </summary>
    public class InvertedSegmentsFlightsFilter : IFlightsFilter
    {
        public IEnumerable<Flight> Filter(IEnumerable<Flight> flights)
        {
            if (flights == null)
            {
                throw new ArgumentNullException("flights");
            }

            return flights.Where(flight => flight.Segments.Any(s => s.ArrivalDate < s.DepartureDate));
        }
    }
}
