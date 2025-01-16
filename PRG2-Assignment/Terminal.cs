using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    class Terminal
    {
        private string terminalName;
        public string TerminalName
        {
            get { return terminalName; }
            set { terminalName = value; }
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

    }
}
