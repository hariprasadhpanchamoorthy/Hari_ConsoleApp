using System.Collections.Generic;

namespace Flights.Domain
{
    /// <summary>
    /// Provides methods for filtering a stream of flights based on certain condition defined by 
    /// the implemetantion of this interface.
    /// </summary>
    public interface IFlightsFilter
    {
        /// <summary>
        /// Lazily filters the input stream.
        /// </summary>
        /// <param name="flights">Input stream of flights. Cannot be null.</param>
        /// <returns>Filtered stream.</returns>
        IEnumerable<Flight> Filter(IEnumerable<Flight> flights);
    }
}
