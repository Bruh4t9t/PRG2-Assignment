// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic.FileIO;
using PRG2_Assingment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
foreach (string line in File.ReadLines("flights.csv")) //Use your file path
{
    List<string> newList = new List<string>(line.Split(','));
    flightList.Add(newList);

}
flightList.RemoveAt(0);
//int padding = 15;
void DisplayAllFlights() // Turned into a method - Damian
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (List<string> flight in flightList)
    {
        foreach (List<string> name in nameList)
        {
            //Console.WriteLine(flight[0].Substring(0,2));
            if (flight[0].Substring(0, 2) == name[1])
            {

                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", flight[0], name[0], flight[1], flight[2], flight[3]);
            }
        }
    }
}
// Task 2(by Puru)
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
foreach (List<String> flightobj in flightList)
{
    flights.Add(flightobj[0], new Flight(flightobj[0], flightobj[1], flightobj[2], Convert.ToDateTime(flightobj[3])));
}
// Task 1 Cont'd ( By Puru)
List<Airline> airlines = new List<Airline>();
Dictionary<string,string> AirlinesDictionary = new Dictionary<string,string>();

foreach (List<string> name in nameList)
{
    airlines.Add(new Airline(name[0], name[1]));
    AirlinesDictionary[name[1]] = name[0];
}
//foreach(KeyValuePair<string,string> pair in AirlinesDictionary)
//{
//    Console.WriteLine(pair.Key);
//    Console.WriteLine(pair.Value);
//}
// Please keep below part we need it 
foreach (Airline airline in airlines)
{
    foreach (KeyValuePair<string, Flight> pair in flights)
    {
        if (pair.Key.Substring(0, 2) == airline.code)
        {
            airline.AddFlight(pair.Value);

        }
    }
}
//foreach(Airline airline in airlines)
//{
//    Console.WriteLine(airline.code);
//}

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

// Please use when you want to check if value is Y or N 
// Ok sigma
bool checkYN(string yn)
{
    if (yn.ToUpper() == "Y")
    {
        return true;
    }
    return false;
}


// Task 5 by Damian 
void AssignGate(Dictionary<string, Flight> flights)
{
    Console.Write("Enter Flight Number: ");
    string flightNum = Console.ReadLine().ToUpper();

    if (flights.ContainsKey(flightNum))
    {
        BoardingGate selectedGate = null;
        bool gateAssigned = false;

        Console.Write("Enter Boarding Gate Name: ");
        string boardinggatename = Convert.ToString(Console.ReadLine().ToUpper());

        Flight flight = flights[flightNum];

        Console.WriteLine("\nFlight Details:");
        Console.WriteLine($"Flight Number: {flight.flightNumber}");
        Console.WriteLine($"Origin: {flight.origin}");
        Console.WriteLine($"Destination: {flight.destination}");
        Console.WriteLine($"Expected Departure/Arrival: {flight.expectedTime}");

        string specialRequestCode = "";
        bool found = false;

        foreach (var flightDetails in flightList)
        {
            if (flightDetails[0] == flightNum && flightDetails.Count > 4)
            {
                specialRequestCode = flightDetails[4];
                found = true;
                break;
            }
        }

        if (found)
        {
            Console.WriteLine($"Special Request Code: {specialRequestCode}");
        }
        else
        {
            Console.WriteLine($"Special Request Code: None");
        }

        do
        {
            if (boardingGates.ContainsKey(boardinggatename))
            {
                selectedGate = boardingGates[boardinggatename];

                if (selectedGate.flight != null)
                {
                    Console.WriteLine($"Gate {boardinggatename} is already assigned to flight {selectedGate.flight.flightNumber}.");
                    break;
                }
                else
                {
                    gateAssigned = true;
                }
            }
            else
            {
                Console.WriteLine("Invalid Boarding Gate.");
                gateAssigned = false;
                break;
            }
        } while (!gateAssigned);

        if (gateAssigned != false)
        {
            Console.WriteLine($"Gate {selectedGate.gateName} supports the following:");
            Console.WriteLine($"Supports DDJB: {selectedGate.supportsDDJB}");
            Console.WriteLine($"Supports CFFT: {selectedGate.supportsCFFT}");
            Console.WriteLine($"Supports LWTT: {selectedGate.supportsLWTT}");

            Console.Write("Would you like to update the status of the flight? (Y/N): ");
            string option = Convert.ToString(Console.ReadLine().ToUpper());

            if (option == "Y")
            {
                Dictionary<int, string> flightStatusOptions = new Dictionary<int, string>
            {
                { 1, "Delayed" },
                { 2, "Boarding" },
                { 3, "On Time" }
            };
                Console.WriteLine("1.  Delayed");
                Console.WriteLine("2.  Boarding");
                Console.WriteLine("3.  On Time");
                Console.Write("Please select the new status of the flight: ");

                int newOption = Convert.ToInt32(Console.ReadLine());

                flight.status = flightStatusOptions[newOption];
                Console.WriteLine($"Flight {flightNum} status updated to {flight.status}.");  // For testing
            }

            selectedGate.flight = flight;
            Console.WriteLine($"Flight {flightNum} has been assigned to Boarding Gate {selectedGate.gateName}!");
        }
    }
    else
    {
        Console.WriteLine("Invalid Flight Number.");
        AssignGate(flights);
    }
}

// Task 6
void setFlight()
{
    string option = "Y";
    List<string> codelist = new List<string>();
    while (option == "Y")
    {
        Console.WriteLine("Enter Flight number");
        String flightNumber = Console.ReadLine();
        while (flightNumber.Length != 6)
        {
            Console.WriteLine("Invalid Innput");
            Console.WriteLine("Enter Flight number");
            flightNumber = Console.ReadLine();

        }
        Console.WriteLine(flightNumber.Substring(0, 2));

        foreach (List<string> name in nameList)
        {
            codelist.Add(name[1]);
            Console.WriteLine(name[1]);
        }
        while (!(codelist.Contains(flightNumber.Substring(0, 2))))
        {
            Console.WriteLine("Invalid Innput");
            Console.WriteLine("Enter Flight number");
            flightNumber = Console.ReadLine();
        }

        Console.WriteLine("Enter Origin");
        String origin = Console.ReadLine();
        Console.WriteLine("Enter Destination");
        String Destination = Console.ReadLine();
        Console.WriteLine("Enter Expected Departure/Arrival");
        String dep = Console.ReadLine();
        while (!DateTime.TryParse(dep, out DateTime result))
        {
            Console.WriteLine("Invalid Date Time");
            Console.WriteLine("Enter Expected Departure/Arrival");
            dep = Console.ReadLine();
        }
        string SpecialRequestCode = "";
        Console.WriteLine("Do you want to enter a special request code(N/Y)");
        if (Console.ReadLine().ToUpper() == "Y")
        {
            Console.WriteLine("Enter special code: ");
            SpecialRequestCode = Console.ReadLine();

        }
        Flight newFlight = new Flight(flightNumber, origin, Destination, Convert.ToDateTime(dep));
        flights.Add(flightNumber, newFlight);

        foreach (Airline airline in airlines)
        {
            foreach (KeyValuePair<string, Flight> pair in flights)
            {
                if (pair.Key.Substring(0, 2) == airline.code)
                {
                    airline.AddFlight(newFlight);
                    break;

                }
            }
        }
        if (SpecialRequestCode != "")
        {
            File.AppendAllText("flights.csv", $"\n{flightNumber},{origin},{Destination},{dep},{SpecialRequestCode}");
        }
        else
        {
            File.AppendAllText("flights.csv", $"\n{flightNumber},{origin},{Destination},{dep}");

        }
        Console.WriteLine("Do you want to enter another flight (Y/N)");
        option = Console.ReadLine().ToUpper();
        while (!checkYN(option))
        {
            Console.WriteLine("Do you want to enter another flight (Y/N)");
            option = Console.ReadLine().ToUpper();
        };
        Console.WriteLine(option);
        if (option == "N")
        {
            break;
        }
    }
}


// Task 7a by Damian

void DisplayAirlines(Dictionary<string, string> AirlinesDictionary)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15}{1,15}", "Airline Code", "Airline Name");
    foreach (var name in airlines)
    {
        Console.WriteLine("{0,-18}{1,-15}", name.code, name.name);
    }
}

//setFlight();
//AssignGate(flights);
//AssignGate(flights);


// Console Menu

Console.WriteLine("Loading Airlines...");
Console.WriteLine("8 Airlines Loaded!");
Console.WriteLine("Loading Boarding Gates...");
Console.WriteLine("66 Boarding Gates Loaded!");
Console.WriteLine("Loading Flights...");
Console.WriteLine("30 Flights Loaded!");

void DisplayMenu()
{
    int option;
    do
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("Welcome to Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("1. List All Flights");
        Console.WriteLine("2. List Boarding Gates");
        Console.WriteLine("3. Assign a Boarding Gate to a Flight");
        Console.WriteLine("4. Create Flight");
        Console.WriteLine("5. Display Airline Flights");
        Console.WriteLine("6. Modify Flight Details");
        Console.WriteLine("7. Display Flight Schedule");
        Console.WriteLine("0. Exit");
        Console.WriteLine();

        Console.Write("Please select your option: ");

        if (int.TryParse(Console.ReadLine(), out option))
        {
            switch (option)
            {
                case 1:
                    DisplayAllFlights();
                    break;

                case 2:
                    DisplayGates(boardingGates);
                    break;
                case 3:
                    AssignGate(flights);
                    break;
                case 4:
                    setFlight();
                    break;
                case 5:
                    DisplayAirlines(AirlinesDictionary);
                    enterFlightNumber();
                    break;
                case 6:
                    ModifyFlightDetails();                   
                    break;
                case 7:
                    SortDisplayList();
                    break;
                case 0:
                    Console.WriteLine("Goodbye!");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }

        if (option != 0)
        {
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }

    } while (option != 0);
}


DisplayMenu();
// Task 7

void enterFlightNumber()
{
    Console.Write("Enter a flight code: ");
    string flightCode = Console.ReadLine();
    bool codematch = false;
    
    while (!codematch)
    {
        foreach (List<string> name in nameList)
        {
            
            if (flightCode.ToUpper() == name[1])
            {
                
                codematch = true;
                break;
            }
        }
        //Console.WriteLine(codematch);
        if (codematch)
        {
            break;
        }
        else
        {
            Console.WriteLine("No Flight code found!");

        }
        Console.Write("Enter a flight code: ");
        flightCode = Console.ReadLine();

    };
    foreach (KeyValuePair<string,Flight> flight in flights)
    {
        
        
        if (flight.Key.Substring(0,2) == flightCode.ToUpper())
        {
            Console.WriteLine(flight.Key);
            Console.WriteLine(flight.Value);
        }

    }
    Console.Write("Enter a valid flight number");
    string number = Console.ReadLine();
    string code = flightCode + " " + number;
    bool inside = false;
    foreach (List<string> flight in flightList) 
    {
        
        if (code == flight[0])
        {
            inside = true;
            try
            {
                Console.WriteLine($"{flight[0]}, {flight[1]}, {flight[2]}, {flight[3]},{flight[4]}");
                
            }
            catch
            {
                Console.WriteLine($"{flight[0]}, {flight[1]}, {flight[2]}, {flight[3]}");
            }
        }
    }
    if (inside == false)
    {
        Console.WriteLine("Flight does not exist");
    }
}
//enterFlightNumber();



// Task 8 by Damian
void ModifyFlightDetails()
{
    DisplayAirlines(AirlinesDictionary);
    Console.Write("Enter a flight code: ");
    string flightCode = Console.ReadLine().ToUpper();
    bool codeMatch = false;

    while (!codeMatch)
    {
        if (AirlinesDictionary.ContainsKey(flightCode))
        {
            codeMatch = true;
        }
        else
        {
            Console.WriteLine("No Flight code found!");
            ModifyFlightDetails(); 
        }
    }

    string airlinesname = "";

    foreach (KeyValuePair<string, string> airline in AirlinesDictionary)
    {
        if (airline.Key == flightCode)
        {
            airlinesname = airline.Value;
            break;
        }
    }

    Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    foreach (var flight in flights)
    {
        if (flight.Key.Contains(flightCode))
        {
            Flight currentFlight = flight.Value;
            Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", currentFlight.flightNumber, airlinesname, currentFlight.origin, currentFlight.destination, currentFlight.expectedTime);
        }
    }

    Console.Write("Choose an existing Flight to modify or delete: ");
    string selectedflight = Console.ReadLine().ToUpper();

    if (!flights.ContainsKey(selectedflight))
    {
        Console.WriteLine("Flight code not found.");
        return;
    }

    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");

    int modordel;
    if (!int.TryParse(Console.ReadLine(), out modordel) || (modordel != 1 && modordel != 2))
    {
        Console.WriteLine("Invalid selection. Please enter 1 or 2.");
        return;
    }

    if (modordel == 1)
    {
        Console.WriteLine("1. Modify Basic Information");
        Console.WriteLine("2. Modify Status");
        Console.WriteLine("3. Modify Special Request Code");
        Console.WriteLine("4. Modify Boarding Gate");
        Console.Write("Choose an option: ");

        int option;
        if (!int.TryParse(Console.ReadLine(), out option) || option < 1 || option > 4)
        {
            Console.WriteLine("Invalid selection. Please choose a valid option.");
            return;
        }

        switch (option)
        {
            case 1:
                Console.Write("Enter new Origin: ");
                string neworigin = Console.ReadLine();

                Console.Write("Enter new Destination: ");
                string newdest = Console.ReadLine();

                Console.Write("Enter new Expected Departure/Arrival Time: ");
                DateTime newdatetime;
                if (DateTime.TryParse(Console.ReadLine(), out newdatetime))
                {
                    Flight selectedFlight = flights[selectedflight];
                    selectedFlight.origin = neworigin;
                    selectedFlight.destination = newdest;
                    selectedFlight.expectedTime = newdatetime;

                    Console.WriteLine("Flight information updated.");
                    DisplayFlightDetails(selectedFlight, AirlinesDictionary, boardingGates);
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
                break;

            case 2:
                Console.Write("Would you like to update the status of the flight? (Y/N): ");
                string option1 = Convert.ToString(Console.ReadLine().ToUpper());

                if (option1 == "Y")
                {
                    Dictionary<int, string> flightStatusOptions = new Dictionary<int, string>
                    {
                        { 1, "Delayed" },
                        { 2, "Boarding" },
                        { 3, "On Time" }
                    };
                    Console.WriteLine("1.  Delayed");
                    Console.WriteLine("2.  Boarding");
                    Console.WriteLine("3.  On Time");
                    Console.Write("Please select the new status of the flight: ");

                    int newOption = Convert.ToInt32(Console.ReadLine());
                    Flight selectedFlight = flights[selectedflight];
                    selectedFlight.status = flightStatusOptions[newOption];
                    Console.WriteLine($"Flight {selectedFlight.flightNumber} status updated to {selectedFlight.status}.");
                    DisplayFlightDetails(selectedFlight, AirlinesDictionary, boardingGates);
                }
                break;

            case 3:
                string specialRequestCode = "";
                bool found = false;

                foreach (var flightDetails in flightList)
                {
                    if (flightDetails[0] == selectedflight && flightDetails.Count > 4)
                    {
                        specialRequestCode = flightDetails[4];
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    Console.WriteLine($"Special Request Code: {specialRequestCode}");
                }
                else
                {
                    Console.WriteLine($"Special Request Code: None");
                }
                break;

            case 4:
                Console.Write("Choose a gate to assign to the selected flight: ");
                string selectedGateName = Console.ReadLine().ToUpper();

                if (boardingGates.ContainsKey(selectedGateName) && boardingGates[selectedGateName].flight == null)
                {
                    BoardingGate selectedGate = boardingGates[selectedGateName];
                    selectedGate.flight = flights[selectedflight]; 
                    Console.WriteLine($"Gate {selectedGateName} has been successfully assigned to Flight {selectedflight}.");
                }
                else
                {
                    Console.WriteLine("Invalid gate or gate is already assigned to another flight.");
                }

                DisplayFlightDetails(flights[selectedflight], AirlinesDictionary, boardingGates);
                break;

            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
    else if (modordel == 2)
    {
        flights.Remove(selectedflight);
        Console.WriteLine($"Flight {selectedflight} deleted.");
    }
}


void DisplayFlightDetails(Flight selectedFlight, Dictionary<string, string> AirlinesDictionary, Dictionary<string, BoardingGate> boardingGates)
{
    string airlineCode = selectedFlight.flightNumber.Substring(0, 2);

    string airlineName = "";
    if (AirlinesDictionary.ContainsKey(airlineCode))
    {
        airlineName = AirlinesDictionary[airlineCode];
    }

    Console.WriteLine($"Flight Number: {selectedFlight.flightNumber}");
    Console.WriteLine($"Airline Name: {airlineName}");
    Console.WriteLine($"Origin: {selectedFlight.origin}");
    Console.WriteLine($"Destination: {selectedFlight.destination}");
    Console.WriteLine($"Expected Departure/Arrival Time: {selectedFlight.expectedTime}");
    Console.WriteLine($"Status: {selectedFlight.status}");
    string specialRequestCode = "";
    bool found = false;

    foreach (var flightDetails in flightList)
    {
        if (flightDetails[0] == selectedFlight.flightNumber && flightDetails.Count > 4)
        {
            specialRequestCode = flightDetails[4];
            found = true;
            break;
        }
    }
    if (found)
    {
        Console.WriteLine($"Special Request Code: {specialRequestCode}");
    }
    else
    {
        Console.WriteLine($"Special Request Code: None");
    }

    string boardingGate = "N/A";
    foreach (var gate in boardingGates.Values)
    {
        if (gate.flight == selectedFlight)
        {
            boardingGate = gate.gateName;
            break;
        }
    }
    Console.WriteLine($"Boarding Gate: {boardingGate}");
    Console.WriteLine();
}


void SortDisplayList()
{
    List<Flight> sortedFlights = flights.Values.ToList();

    sortedFlights.Sort();
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine("{0,-12}{1,-22}{2,-18}{3,-18}{4,-20}{5,-14}{6,-15}{7,-10}", "Flight No.", "Airline Name", "Origin", "Destination", "Departure Time", "Status", "Special Code", "Gate");

    foreach (var flight in sortedFlights)
    {
        string airlineCode = flight.flightNumber.Substring(0, 2);
        string airlineName = AirlinesDictionary.ContainsKey(airlineCode) ? AirlinesDictionary[airlineCode] : "Unknown";

        string specialRequestCode = "N/A";
        foreach (var flightDetails in flightList)
        {
            if (flightDetails[0] == flight.flightNumber && flightDetails.Count > 4)
            {
                specialRequestCode = flightDetails[4];
                break;
            }
        }

        string boardingGate = "N/A";
        foreach (var gate in boardingGates.Values)
        {
            if (gate.flight == flight)
            {
                boardingGate = gate.gateName;
                break;
            }
        }

        Console.WriteLine("{0,-12}{1,-22}{2,-18}{3,-18}{4,-20}{5,-14}{6,-15}{7,-10}", flight.flightNumber, airlineName, flight.origin, flight.destination, flight.expectedTime.ToString("hh:mm tt"), flight.status, specialRequestCode, boardingGate);
    }
}


