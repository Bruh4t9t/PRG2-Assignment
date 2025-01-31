using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10266941K
// Student Name : Puru Gulati
// Partner Name : Damian
//==========================================================
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

        public BoardingGate(Flight flight, string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT)
        {
            this.flight = flight;
            this.gateName = gateName;
            this.supportsCFFT = supportsCFFT;
            this.supportsDDJB = supportsDDJB;
            this.supportsLWTT = supportsLWTT;
        }

        public override string ToString()
        {
            return $"Gate: {gateName}, Supports CFFT: {supportsCFFT}, Supports DDJB: {supportsDDJB}, Supports LWTT: {supportsLWTT}, Flight: {flight?.flightNumber ?? "None"}";
        }

    }
}