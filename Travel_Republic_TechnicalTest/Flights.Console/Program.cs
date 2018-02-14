using Flights.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flights.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var Filters = new []
            {
                new
                {
                    //Filter1
                    FilterName = "Flights that depart before the current date/time",   
                    Filter = (IFlightsFilter)new DepartsBeforeFlightsFilter(DateTime.Now),
                },
                new
                {
                    //Filter2
                    FilterName = "Flights that have a segment with an arrival date before the departure date",
                    Filter = (IFlightsFilter)new InvertedSegmentsFlightsFilter(),
                },
                new
                {
                    //Filter3
                    FilterName = "Flights that spend more than two hours on the ground",
                    Filter = (IFlightsFilter)new SpendsMoreTimeOnGroundFlightsFilter(TimeSpan.FromHours(2)),
                },
            };

            var allFlights = new FlightBuilder().GetFlights();

            foreach (var Filter in Filters)
            {
                var filteredFlights = Filter.Filter.Filter(allFlights).ToList();

                System.Console.WriteLine(Filter.FilterName + ":");
                foreach (var flight in filteredFlights)
                {
                    System.Console.WriteLine(string.Join(", ", flight.Segments.Select(s => s.DepartureDate + " --> " + s.ArrivalDate)));
                }

                System.Console.WriteLine();
            }

            System.Console.ReadLine();
        }
    }
}
