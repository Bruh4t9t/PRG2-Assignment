using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
//==========================================================
// Student Number : S10266941K
// Student Name : Puru Gulati
// Partner Name : Damian
//==========================================================
namespace PRG2_Assingment
{
    class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> airlines { get; set; }
        public Dictionary<string, Flight> flights { get; set; }
        public Dictionary<string, BoardingGate> boardingGates { get; set; }
        public Dictionary<string, double> gateFees { get; set; }

        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            this.airlines = airlines ?? new Dictionary<string, Airline>();
            this.flights = flights ?? new Dictionary<string, Flight>();
            this.boardingGates = boardingGates ?? new Dictionary<string, BoardingGate>();
            this.gateFees = gateFees ?? new Dictionary<string, double>();
        }

        public void PrintAirlineFees(List<List<string>> flightList)
        {
            List<Flight> unassignedFlights = new List<Flight>();

            foreach (var flight in flights.Values)
            {
                bool hasGate = boardingGates.Values.Any(gate => gate.flight == flight);
                if (!hasGate)
                {
                    unassignedFlights.Add(flight);
                }
            }

            if (unassignedFlights.Count > 0)
            {
                Console.WriteLine("The following flights have NOT been assigned a boarding gate:");
                foreach (var flight in unassignedFlights)
                {
                    Console.WriteLine(flight.flightNumber);
                }
                Console.WriteLine("Please assign gates before calculating fees.");
                return;
            }

            Dictionary<string, double> airlineFees = new Dictionary<string, double>();
            Dictionary<string, double> airlineDiscounts = new Dictionary<string, double>();

            Console.WriteLine("==============================================");
            Console.WriteLine("Total Fees and Discounts Breakdown");
            Console.WriteLine("==============================================");

            foreach (var airline in airlines.Values)
            {
                double totalFees = 0;
                double totalDiscounts = 0;
                int flightCount = 0;

                foreach (var flight in airline.flights.Values)
                {
                    if (flight.destination == "Singapore (SIN)")
                    {
                        totalFees += 500; 
                    }
                    if (flight.origin == "Singapore (SIN)")
                    {
                        totalFees += 800;
                    }

                    totalFees += 300;
                    string specialRequestCode = "";
                    foreach (var flightDetails in flightList)
                    {
                        if (flightDetails[0] == flight.flightNumber)
                        {
                            specialRequestCode = flightDetails[4];
                            break;
                        }
                    }

                    if (specialRequestCode == "CFFT")
                    {
                        totalFees += 150;
                    }
                    else if (specialRequestCode == "DDJB")
                    {
                        totalFees += 300;
                    }
                    else if (specialRequestCode == "LWTT")
                    {
                        totalFees += 500;
                    }

                    if (flight.expectedTime.Hour < 11 || flight.expectedTime.Hour > 21)
                    {
                        totalDiscounts += 110;
                    }

                    if (flight.origin == "Dubai (DXB)" || flight.origin == "Bangkok (BKK)" || flight.origin == "Tokyo (NRT)")
                    {
                        totalDiscounts += 25;
                    }

                    if (specialRequestCode == "")
                    {
                        totalDiscounts += 50;
                    }

                    flightCount++;
                }

                if (flightCount >= 3)
                {
                    totalDiscounts += 350 * (flightCount / 3);
                }

                if (flightCount > 5)
                {
                    totalDiscounts += totalFees * 0.03;
                }

                airlineFees[airline.name] = totalFees;
                airlineDiscounts[airline.name] = totalDiscounts;

                Console.WriteLine($"{airline.name}:");
                Console.WriteLine($"  Subtotal Fees: {totalFees:C}");
                Console.WriteLine($"  Discounts: {totalDiscounts:C}");
                Console.WriteLine($"  Final Total: {totalFees - totalDiscounts:C}");
                Console.WriteLine();
            }

            double totalFeesAllAirlines = airlineFees.Values.Sum();
            double totalDiscountsAllAirlines = airlineDiscounts.Values.Sum();
            double finalTotalFees = totalFeesAllAirlines - totalDiscountsAllAirlines;
            double discountPercentage = totalFeesAllAirlines > 0 ? (totalDiscountsAllAirlines / totalFeesAllAirlines) * 100 : 0;

            Console.WriteLine("==============================================");
            Console.WriteLine("Final Total for Terminal 5");
            Console.WriteLine("==============================================");
            Console.WriteLine($"  Subtotal Fees (All Airlines): {totalFeesAllAirlines:C}");
            Console.WriteLine($"  Subtotal Discounts: {totalDiscountsAllAirlines:C}");
            Console.WriteLine($"  Final Total Fees Collected: {finalTotalFees:C}");
            Console.WriteLine($"  Discount Percentage: {discountPercentage:F2}%");
            Console.WriteLine("==============================================");
        }

        public override string ToString()
        {
            return $"Terminal: {TerminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Boarding Gates: {boardingGates.Count}";
        }
    }
}
