using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assingment
{
    class Terminal
    {
  
        public string TerminalName { get; set; }
        

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

        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gateFees)
        {
            TerminalName = terminalName;
            airlines = new Dictionary<string, Airline>();
            flights = new Dictionary<string, Flight>();
            boardingGates = new Dictionary<string, BoardingGate>();
            gateFees = new Dictionary<string, double>();
        }

        public bool AddAirline(Airline airline)
        {
            return false;
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            return false;
        }

        public void GetAirlineFromFlight(Flight flight)
        {
            return;
        }
        public void PrintAirlineFees()
        {
            return;
        }

        public string ToString()
        {
            return "";
        }
    }
}
