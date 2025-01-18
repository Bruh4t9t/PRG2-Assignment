using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    public class LWTTFFlight : Flight
    {
        public double requestFee
        {
            get; set;
        }

        public override double CalculateFees()
        {
            return;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}