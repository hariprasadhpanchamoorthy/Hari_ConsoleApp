using System;
using System.Collections.Generic;
using System.Linq;

namespace Flights.Domain
{
    /// <summary>
    /// Filters out flights which departs before specified time.
    /// </summary>
    public class DepartsBeforeFlightsFilter : IFlightsFilter
    {
        private readonly DateTime _departsBefore;

        public DepartsBeforeFlightsFilter(DateTime departsBefore)
        {
            _departsBefore = departsBefore;
        }

        public IEnumerable<Flight> Filter(IEnumerable<Flight> flights)
        {
            if (flights == null)
            {
                throw new ArgumentNullException("flights");
            }

            return flights.Where(flight => flight.Segments.Any()).Where(flight => flight.Segments[0].DepartureDate < _departsBefore);
        }
    }
}
