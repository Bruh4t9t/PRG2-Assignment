using System;
using System.Collections.Generic;
using System.Linq;
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

        }
        public double CalculateFees()
        {

        }
        public bool RemoveFlight(Flight flight)
        {

        }
        public override string ToString()
        {

        }
        public Airline(string Name,string Description, Dictionary<string, Flight> Tags) 
        {
            name = Name;
            description = Description;
            tags = Tags;
            
        }
    }
}
