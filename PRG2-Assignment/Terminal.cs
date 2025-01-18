using System;
using System.Collections.Generic;

namespace PRG2_Assignment
{
    public class Terminal
    {
        public string terminalName
        {
            get; set;
        }

        public Dictionary<string, Airline> airlines
        {
            get; set;
        }

        public Dictionary<string, Flight> flights
        {
            get; set;
        }

        public Dictionary<string, BoardingGate> boardingGates
        {
            get; set;
        }

        public Dictionary<string, double> gateFees
        {
            get; set;
        }

        public Terminal(string TerminalName, Dictionary<string, Airline> Airlines, Dictionary<string, Flight> Flights, Dictionary<string, BoardingGate> BoardingGates, Dictionary<string, double> GateFees)
        {
            terminalName = TerminalName;
            airlines = Airlines;
            flights = Flights;
            boardingGates = BoardingGates;
            gateFees = GateFees;
        }

        public bool AddAirline(Airline airline)
        {
            return;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            return;
        }

        public void GetAirlineFromFlight(Flight flight)
        {
            return;
        }

        public void PrintAirlineFee()
        {
            return;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
