// See https://aka.ms/new-console-template for more information
using PRG2_Assingment;
using System;
using System.IO;

// Task 3 ( By Puru )
List<List<string>> flightList = new List<List<string>>();
List<List<string>> nameList = new List<List<string>>();

foreach (string line in File.ReadLines("C:\\Users\\pg200\\Documents\\GitHub\\PRG2-Assignment\\PRG2-Assingment\\PRG2-Assingment\\airlines.csv"))
{
    List<string> newList = new List<string>(line.Split(','));
    nameList.Add(newList);
}
nameList.RemoveAt(0);
foreach (string line in File.ReadLines("C:\\Users\\pg200\\Documents\\GitHub\\PRG2-Assignment\\PRG2-Assingment\\PRG2-Assingment\\flights.csv")) //Use your file path
{
    List<string> newList = new List<string>(line.Split(','));
    flightList.Add(newList);

}
flightList.RemoveAt(0);
int padding = 15;
Console.WriteLine($"{"Flight number":-15}{"Airline Name":-15}{"Origin":padding}{"Destination":padding}{"Expected Departure/Arrival":padding}");
foreach (List<string> flight in flightList)
{
    foreach (List<string> name in nameList)
    {
        Console.WriteLine(flight[0].Substring(0,2));
        if (flight[0].Substring(0, 2) == name[1])
        {

            Console.WriteLine($"{name[0]:-15}{flight[0]:-15}{flight[1]:padding}{flight[2]:padding}{flight[3]:padding}");
            
        }
    }
}

// Task 2(by Puru)
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
foreach (List<String> flight in flightList)
{
    flights.Add(flight[0], new Flight(flight[0], flight[1], flight[2], Convert.ToDateTime(flight[3])));
}
// Task 1 Cont'd ( By Puru)
List<Airline> airlines = new List<Airline>();
Dictionary<string,string> AirlinesDictionary = new Dictionary<string,string>();

foreach (List<string> name in nameList)
{
    airlines.Add(new Airline(name[0], name[1]));
    AirlinesDictionary[name[1]] = name[0];
}
foreach(KeyValuePair<string,string> pair in AirlinesDictionary)
{
    Console.WriteLine(pair.Key);
    Console.WriteLine(pair.Value);
}
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
foreach(Airline airline in airlines)
{
    Console.WriteLine(airline.code);
}

// For BoardingGates
// Task 4( By Damian)
string[] file_data = File.ReadAllLines("C:\\Users\\pg200\\Documents\\GitHub\\PRG2-Assignment\\PRG2-Assingment\\PRG2-Assingment\\boardinggates.csv").Skip(1).ToArray();

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


// Task 7 by Damian

void DisplayAirlines(List<List<string>> nameList)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15}{1,15}", "Airline Code", "Airline Name");
    foreach (var name in nameList)
    {
        Console.WriteLine("{0,-18}{1,-15}", name[1], name[0]);
    }
    Console.Write("Enter Airline Code: ");
    string selectedcode = Convert.ToString(Console.ReadLine().ToUpper());
    // To be implemented after dictionary fix

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
                    Console.WriteLine(""); // Add flights
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
                    DisplayAirlines(nameList);
                    break;
                case 6:
                    Console.WriteLine(""); // Modify status
                    break;
                case 7:
                    Console.WriteLine(""); // Display
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