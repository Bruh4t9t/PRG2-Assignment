using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assingment
{
    public class Airline
    {
        public string name {  get; set; }
        public string description { get; set; } 
        public Dictionary<string, Flight> tags { get; set; }
        public bool AddFlight(Flight flight)
        {
            return false;
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
        public Airline(string Name,string Description, Dictionary<string, Flight> Tags) 
        {
            name = Name;
            description = Description;
            tags = Tags;
            
        }
    }
}
