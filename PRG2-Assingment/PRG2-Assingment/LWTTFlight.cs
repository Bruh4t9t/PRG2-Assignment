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
    public class LWTTFFlight : Flight
    {
        public double requestFee { get; set; }

        public LWTTFFlight(string flightNumber, string origin, string destination, DateTime expectedTime) : base(flightNumber, origin, destination, expectedTime)
        {
            requestFee = 500;
        }

        public override double CalculateFees()
        {
            return base.CalculateFees() + requestFee;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}