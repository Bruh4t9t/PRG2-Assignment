using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    public class DDJBFlight : Flight
    {
        public double requestFee 
        { 
            get; set; 
        }
        public override double CalculateFees()
        {

        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}