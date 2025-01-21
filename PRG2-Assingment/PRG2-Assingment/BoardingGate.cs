using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assingment
{
    public class BoardingGate
    {
        public string gateName
        {
            get; set;
        }

        public bool supportsCFFT
        {
            get; set;
        }

        public bool supportsDDJB
        {
            get; set;
        }

        public bool supportsLWTT
        {
            get; set;
        }
        public Flight flight { get; set; }

        public double CalculateFees()
        {
            return 0;
        }

        public string ToString()
        {
            return base.ToString();
        }

        public BoardingGate(string GateName, bool SupportsCFFT, bool SupportsDDJB, bool SupportsLWTT)
        {
            gateName = GateName;
            supportsCFFT = SupportsCFFT;
            supportsDDJB = SupportsDDJB;
            supportsLWTT = SupportsLWTT;
        }

    }
}