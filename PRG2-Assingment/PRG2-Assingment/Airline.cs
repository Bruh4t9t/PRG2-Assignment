using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10266941K
// Student Name : Puru Gulati
// Partner Name : Damian
//==========================================================
namespace PRG2_Assingment
{
    public class Airline
    {
        public string name {  get; set; }
        public string code { get; set; } 
        public Dictionary<string, Flight> flights { get; set; }
        public bool AddFlight(Flight flight)
        {

            flights.Add(flight.flightNumber, flight);
            return true;
        }
        public double CalculateFees()
        {
            return 0;
        }
        public bool RemoveFlight(Flight flight)
        {
            return false;
        }
        public override string ToString()
        {
            return "";
        }
        public Airline(string Name, string Code) 
        {
            name = Name;
            code = Code;
            flights = new Dictionary<string, Flight>();
            
        }
    }
}
