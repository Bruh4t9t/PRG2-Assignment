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

    }
}
