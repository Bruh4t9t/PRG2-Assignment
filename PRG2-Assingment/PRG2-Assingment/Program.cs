// See https://aka.ms/new-console-template for more information
using PRG2_Assingment;
using System;
using System.IO;

// Task 3 ( By Puru )
List<List<string>> flightList = new List<List<string>>();
List<List<string>> nameList = new List<List<string>>();
foreach (string line in File.ReadLines("airlines.csv"))
{
    List<string> newList = new List<string>(line.Split(','));
    nameList.Add(newList);
}
nameList.RemoveAt(0);
foreach  (string line in File.ReadLines("flights.csv")) //Use your file path
{
    List<string> newList = new List<string>(line.Split(','));
    flightList.Add(newList);

}
flightList.RemoveAt(0);
int padding = 15;
Console.WriteLine($"{"Flight number":-15}{"Airline Name":-15}{"Origin":padding}{"Destination":padding}{"Expected Departure/Arrival":padding}");
foreach(List<string> flight in flightList)
{
    foreach (List<string> name in nameList)
    {
        if (flight[0].Substring(0,2) == name[1])
        {
            
            Console.WriteLine($"{name[0]:-15}{flight[0]:-15}{flight[1]:padding}{flight[2]:padding}{flight[3]:padding}");
        }
    }
}

// Task 2(by Puru)
Dictionary<string,Flight> flights = new Dictionary<string, Flight>();
foreach (List<String> flight in flightList)
{
    flights.Add(flight[0],new Flight(flight[0], flight[1], flight[2], Convert.ToDateTime(flight[3])));   
}
// Task 1 Cont'd ( By Puru)
List<Airline> airlines = new List<Airline>();
foreach (List<string> name in nameList)
{
    airlines.Add(new Airline(name[0], name[1]));
}
foreach (Airline airline in airlines)
{
    foreach(KeyValuePair<string,Flight> pair in flights)
    {
        if (pair.Key.Substring(0,2) == airline.code)
        {
            airline.AddFlight(pair.Value);
            
        }
    }    
}

// For BoardingGates
// Task 4( By Damian)
string[] file_data = File.ReadAllLines("boardinggates.csv").Skip(1).ToArray();

Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();

foreach (string item in file_data)
{
    string[] gateData = item.Split(",");
    string gatename = gateData[0];
    bool supportsCFFT = Convert.ToBoolean(gateData[1]);
    bool supportsDDJB = Convert.ToBoolean(gateData[2]);
    bool supportsLWTT = Convert.ToBoolean(gateData[3]);

    BoardingGate addGateinfo = new BoardingGate(gatename, supportsCFFT, supportsDDJB, supportsLWTT);

    boardingGates[gatename] = addGateinfo;
}


void DisplayGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("{0,-15}{1,-15}{2,15}{3,15}", "Boarding Gate", "DDJB", "CFFT", "LWTT");
    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine("{0,-15}{1,-15}{2,15}{3,15}", gate.gateName, gate.supportsDDJB, gate.supportsCFFT, gate.supportsLWTT);

    }
}

DisplayGates(boardingGates);