﻿using System;
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

        public string ToString()
        {
            return base.ToString();
        }

        public BoardingGate(Flight Flight,string GateName, bool SupportsCFFT, bool SupportsDDJB, bool SupportsLWTT)
        {
            flight = Flight;
            gateName = GateName;
            supportsCFFT = SupportsCFFT;
            supportsDDJB = SupportsDDJB;
            supportsLWTT = SupportsLWTT;
        }

    }
}