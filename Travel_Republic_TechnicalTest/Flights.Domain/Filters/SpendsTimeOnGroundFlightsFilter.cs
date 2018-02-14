using System;
using System.Linq;
using System.Collections.Generic;

namespace Flights.Domain
{
    /// <summary>
    /// Flights filter which filters out flights which spends more than specified number of hours on ground 
    /// on any of the flight's intermediate landing.
    /// </summary>
    public class SpendsMoreTimeOnGroundFlightsFilter : IFlightsFilter
    {
        private readonly TimeSpan _timeOnGround;

        public SpendsMoreTimeOnGroundFlightsFilter(TimeSpan minimumTimeOnGround)
        {
            if (minimumTimeOnGround.TotalHours <= 0)
            {
                throw new ArgumentException("Time on ground must be a positive timespan", "timeOnGround");
            }

            _timeOnGround = minimumTimeOnGround;
        }

        public IEnumerable<Flight> Filter(IEnumerable<Flight> flights)
        {
            if (flights == null)
            {
                throw new ArgumentNullException("flights");
            }

            return flights.Where(FlightSpendsEnoughTimeOnGround);
        }

        private bool FlightSpendsEnoughTimeOnGround(Flight flight)
        {
            Segment previousSegment = null;
            foreach (var segment in flight.Segments)
            {
                if (previousSegment != null)
                {
                    if ((segment.DepartureDate - previousSegment.ArrivalDate) > _timeOnGround)
                    {
                        return true;
                    }
                }

                previousSegment = segment;
            }

            return false;
        }
    }
}
