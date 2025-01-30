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
    public class Flight : IComparable<Flight>
    {
        public string flightNumber { get; set; }
        public string origin { get; set; }
        public string destination {  get; set; }
        public DateTime expectedTime { get; set; }
        public string status { get; set; }
        public double CalculateFees() { return 0; }
        public override string ToString()
        {
            return "Expected Time: " + expectedTime;
        }
        public Flight(string FlightNumber,string Origin,string Destination,DateTime ExpectedTime) 
        { 
            flightNumber = FlightNumber;
            origin = Origin;
            destination = Destination;
            expectedTime = ExpectedTime;
            status = "On Time";
        }

        public int CompareTo(Flight other)
        {
            if (other == null) return 1;
            return this.expectedTime.CompareTo(other.expectedTime);
        }
    }
}
