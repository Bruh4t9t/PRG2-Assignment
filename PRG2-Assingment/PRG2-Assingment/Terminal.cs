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
                    bool norm = false;
                    double flightTotalFee = 300;
                    double specialRequestFee = 0;

                    if (flight.origin == "Singapore (SIN)")
                    {
                        flightTotalFee += 800;
                    }
                    else if (flight.destination == "Singapore (SIN)")
                    {
                        flightTotalFee += 500;
                    }

                    foreach (var item in flightList)
                    {
                        if (item[0] == flight.flightNumber)
                        {
                            if (item.Count > 4)
                            {
                                string specialRequestCode = item[4];

                                if (specialRequestCode == "DDJB")
                                {
                                    DDJBFlight ddjbflight = new DDJBFlight(item[0], item[1], item[2], Convert.ToDateTime(item[3]));
                                    specialRequestFee = ddjbflight.requestFee;
                                    //Console.WriteLine($"{flight.flightNumber} {specialRequestFee}");
                                }
                                else if (specialRequestCode == "CFFT")
                                {
                                    CFFTFlight cfftflight = new CFFTFlight(item[0], item[1], item[2], Convert.ToDateTime(item[3]));
                                    specialRequestFee = cfftflight.requestFee;
                                    //Console.WriteLine($"{flight.flightNumber} {specialRequestFee}");
                                }
                                else if (specialRequestCode == "LWTT")
                                {
                                    LWTTFFlight lwttflight = new LWTTFFlight(item[0], item[1], item[2], Convert.ToDateTime(item[3]));
                                    specialRequestFee = lwttflight.requestFee;
                                    //Console.WriteLine($"{flight.flightNumber} {specialRequestFee}");
                                }
                            }
                            else
                            {
                                norm = true;
                            }
                        }
                    }

                    flightTotalFee += specialRequestFee;
                    totalFees += flightTotalFee;

                    // Calculate discounts
                    // Discount 1: For flights arriving/departing before 11am or after 9pm
                    if (flight.expectedTime.Hour < 11 || flight.expectedTime.Hour > 21)
                    {
                        totalDiscounts += 110;
                    }

                    // Discount 2: For flights with origin of Dubai (DXB), Bangkok (BKK), or Tokyo (NRT)
                    if (flight.origin == "Dubai (DXB)" || flight.origin == "Bangkok (BKK)" || flight.origin == "Tokyo (NRT)")
                    {
                        totalDiscounts += 25;
                    }

                    // Discount 3: For flights without any special request codes
                    if (norm)
                    {
                        totalDiscounts += 50;
                    }

                    flightCount++;
                }

                // Discount 4: For every 3 flights, $350 discount
                if (flightCount >= 3)
                {
                    totalDiscounts += 350 * (flightCount / 3);
                }

                // Discount 5: For airlines with more than 5 flights, 3% off the total bill
                if (flightCount > 5)
                {
                    totalDiscounts += (totalFees * 0.03);
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
