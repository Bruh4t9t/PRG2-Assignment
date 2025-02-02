// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic.FileIO;
using PRG2_Assingment;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.XPath;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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


void DisplayAllFlights()
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
            if (flight[0].Substring(0, 2) == name[1])
            {

                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", flight[0], name[0], flight[1], flight[2], Convert.ToDateTime(flight[3]));
            }
        }
    }
}
// Task 2(by Puru)
Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
foreach (List<string> flightobj in flightList)
{
    flights.Add(flightobj[0], new Flight(flightobj[0], flightobj[1], flightobj[2], Convert.ToDateTime(flightobj[3])));
}
// Task 1 Cont'd ( By Puru)
List<Airline> airlines = new List<Airline>();
Dictionary<string, string> AirlinesDictionary = new Dictionary<string, string>();

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


    // Create a new object using the data
    BoardingGate addGateinfo = new BoardingGate(null,gatename, supportsCFFT, supportsDDJB, supportsLWTT);

    // Adds the BoardingGate object to the dictionary
    boardingGates[gatename] = addGateinfo;
}



// Display what the boarding gates supports 
void DisplayGates(Dictionary<string, BoardingGate> boardingGates)
{
    Console.WriteLine("{0,-15}{1,-15}{2,15}{3,15}", "Boarding Gate", "DDJB", "CFFT", "LWTT");
    foreach (var gate in boardingGates.Values)
    {
        Console.WriteLine("{0,-15}{1,-15}{2,15}{3,15}", gate.gateName, gate.supportsDDJB, gate.supportsCFFT, gate.supportsLWTT);

    }
}

// Please use when you want to check if value is Y or N 
bool checkYN(string yn)
{
    if (yn.ToUpper() == "Y")
    {
        return true;
    }
    return false;
}

int assigngates = 0;
// Task 5 by Damian 
void AssignGate(Dictionary<string, Flight> flights)
{
    Console.Write("Enter Flight Number: ");
    string flightNum = Console.ReadLine().ToUpper();

    try
    {
        // Check if the user infput exists in the flights dictionary
        if (flights.ContainsKey(flightNum))
        {
            BoardingGate selectedGate = null;
            bool gateAssigned = false;

            Console.Write("Enter Boarding Gate Name: ");
            string boardinggatename = Convert.ToString(Console.ReadLine().ToUpper());

            Flight flight = flights[flightNum];
            string formattedTime = flight.expectedTime.ToString("dd/MM/yyyy hh:mm tt");

            Console.WriteLine("\nFlight Details:");
            Console.WriteLine($"Flight Number: {flight.flightNumber}");
            Console.WriteLine($"Origin: {flight.origin}");
            Console.WriteLine($"Destination: {flight.destination}");
            Console.WriteLine($"Expected Departure/Arrival: {formattedTime}");

            string specialRequestCode = "";
            bool found = false;


            // Loop through flightList to find any special request code
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
                // Check if the gate exists in the dictionary and is available
                if (boardingGates.ContainsKey(boardinggatename))
                {
                    selectedGate = boardingGates[boardinggatename];

                    // Check if the gate is already assigne
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

             // gate assignment is successful
            if (gateAssigned != false)
            {
                Console.WriteLine($"Gate {selectedGate.gateName} supports the following:");
                Console.WriteLine($"Supports DDJB: {selectedGate.supportsDDJB}");
                Console.WriteLine($"Supports CFFT: {selectedGate.supportsCFFT}");
                Console.WriteLine($"Supports LWTT: {selectedGate.supportsLWTT}");

                // Assign the flight to the selected gate
                boardingGates[selectedGate.gateName] = new BoardingGate(flight, selectedGate.gateName, selectedGate.supportsCFFT, selectedGate.supportsDDJB, selectedGate.supportsLWTT);
                Console.Write("Would you like to update the status of the flight? (Y/N): ");
                string option = Convert.ToString(Console.ReadLine().ToUpper());

                try
                {
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

                        // Update the flight status
                        flight.status = flightStatusOptions[newOption];
                        Console.WriteLine($"Flight {flightNum} status updated to {flight.status}.");  // For testing
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a valid input for flight status.");
                }
                // Assign the gate to the flight
                selectedGate.flight = flight;
                assigngates += 1;
                Console.WriteLine($"Flight {flightNum} has been assigned to Boarding Gate {selectedGate.gateName}!");
            }
        }
        else
        {
            // flight number is invalid
            Console.WriteLine("Invalid Flight Number.");
            AssignGate(flights);
            return;
        }
    }
    catch (FormatException)
    {
        // input format exheption
        Console.WriteLine("ERnter a valid flight number or boarding gate.");
    }
}


// Task 6 (Puru)
void setFlight()
{
    string option = "Y";
    List<string> codelist = new List<string>();
    while (option == "Y")
    {
        Console.Write("Enter Flight number: ");
        string flightNumber = Console.ReadLine().ToUpper();
        while (flightNumber.Length != 6)
        {
            Console.WriteLine("Invalid Innput");
            Console.Write("Enter Flight number: ");
            flightNumber = Console.ReadLine().ToUpper();

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
            Console.Write("Enter Flight number: ");
            flightNumber = Console.ReadLine().ToUpper();
        }

        Console.Write("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.Write("Enter Destination: ");
        string Destination = Console.ReadLine();
        Console.Write("Enter Expected Departure/Arrival: ");
        string dep = Console.ReadLine();
        while (!DateTime.TryParse(dep, out DateTime result))
        {
            Console.WriteLine("Invalid Date Time");
            Console.Write("Enter Expected Departure/Arrival: ");
            dep = Console.ReadLine();
        }
        string SpecialRequestCode = "";
        Console.Write("Do you want to enter a special request code(N/Y): ");
        if (Console.ReadLine().ToUpper() == "Y")
        {
            Console.WriteLine("Enter special code: ");
            SpecialRequestCode = Console.ReadLine();
            while (SpecialRequestCode != "DDJB" && SpecialRequestCode != "CFFT" && SpecialRequestCode != "LWTT")
            {
                Console.WriteLine("Special request code is wrong");
                Console.Write("Enter special code: ");
                SpecialRequestCode = Console.ReadLine();

            }

        }
        Flight newFlight = new Flight(flightNumber, origin, Destination, Convert.ToDateTime(dep));
        flights.Add(flightNumber, newFlight);
        try
        {
            flightList.Add(new List<string> { newFlight.flightNumber, newFlight.origin, newFlight.destination,Convert.ToString(newFlight.expectedTime),SpecialRequestCode });
        }
        catch
        {
            flightList.Add(new List<string> { newFlight.flightNumber, newFlight.origin, newFlight.destination, Convert.ToString(newFlight.expectedTime) });

        }

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
        
        if (option == "N")
        {
            break;
        }
    }
}


// Task 7a by Damian
//List all airlines code and name
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
// Initialize gateFees (if not already done)
Dictionary<string, double> gateFees = new Dictionary<string, double>();

// Convert List<Airline> to Dictionary<string, Airline>
Dictionary<string, Airline> airlinesDictionary = airlines.ToDictionary(airline => airline.code, airline => airline);

// Create the Terminal object
// for advance b
Terminal terminal = new Terminal("Terminal 5", airlinesDictionary, flights, boardingGates, gateFees);

// Console Menu


Console.WriteLine("Loading Airlines...");
Console.WriteLine("8 Airlines Loaded!");
Console.WriteLine("Loading Boarding Gates...");
Console.WriteLine("66 Boarding Gates Loaded!");
Console.WriteLine("Loading Flights...");
Console.WriteLine("30 Flights Loaded!");

void DisplayMenu()
{
    int option = -1;
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
        Console.WriteLine("8. Automically assign flights");
        Console.WriteLine("9. Display Total Fee per Airline");
        Console.WriteLine("0. Exit");
        Console.WriteLine();

        Console.Write("Please select your option: ");
        try
        {
            option = int.Parse(Console.ReadLine());
        }
        catch
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            continue;
        }

        switch (option)
        {
            case 1:
                DisplayAllFlights();
                break;
            case 2:
                DisplayGates(boardingGates);
                break;
            case 3:
                SortDisplayList();
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

            case 8:
                autoAssignGates();
                break;

            case 9:
                DisplayTotalFee(terminal);
                break;

            case 0:
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    } while (option != 0);
}


DisplayMenu();
// Task 7 (Puru)

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
    foreach (KeyValuePair<string, Flight> flight in flights)
    {


        if (flight.Key.Substring(0, 2) == flightCode.ToUpper())
        {
            Console.WriteLine($"{flight.Key}    {flight.Value}");
        }

    }
    Console.Write("Enter a valid flight number: ");
    string number = Console.ReadLine();
    string code = flightCode.ToUpper() + " " + number;
    bool inside = false;
    string boardingGate = "";
    
    foreach (List<string> flight in flightList)
    {
        foreach (KeyValuePair<string, BoardingGate> pair in boardingGates)
        {
            try
            {
                if (pair.Value.flight is not null)
                {
                    if (code == pair.Value.flight.flightNumber)
                    {
                        boardingGate = pair.Key;
                    }
                }
            }
            catch 
            {
                
            }

        }

        if (code == flight[0])
        {
            inside = true;
            try
            {
                Console.WriteLine("{0,-12}{1,-22}{2,-20}{3,-20}{4,-22}{5,-12}", flight[0], flight[1], flight[2], flight[3],flight[4],boardingGate);

            }
            catch
            {
                Console.WriteLine("{0,-12}{1,-22}{2,-20}{3,-20}", flight[0], flight[1], flight[2], flight[3]);
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

    try
    {
        // Loop until a valid flight code is iput
        while (!codeMatch)
        {
            if (AirlinesDictionary.ContainsKey(flightCode))
            {
                codeMatch = true;
            }
            else
            {
                // loop if code not found
                Console.WriteLine("No Flight code found!");
                ModifyFlightDetails();
            }
        }
    }
    catch
    {
        Console.WriteLine("Error");
        return;
    }

    string airlinesname = "";

    try
    {
        // Loop through AirlinesDictionary to get the airline name with the flight code
        foreach (KeyValuePair<string, string> airline in AirlinesDictionary)
        {
            if (airline.Key == flightCode)
            {
                airlinesname = airline.Value;
                break;
            }
        }
    }
    catch
    {
        Console.WriteLine("Error");
        return;
    }

    Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    try
    {
        //loop through the flights dictionary and display flights that match the flight code
        foreach (var flight in flights)
        {
            if (flight.Key.Contains(flightCode))
            {
                Flight currentFlight = flight.Value;
                string formattedTime = currentFlight.expectedTime.ToString("dd/MM/yyyy hh:mm tt");
                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", currentFlight.flightNumber, airlinesname, currentFlight.origin, currentFlight.destination, formattedTime);
            }
        }
    }
    catch
    {
        Console.WriteLine("Error");
        return;
    }

    // Prompt user to select a flight to modfiy or delte
    Console.Write("Choose an existing Flight to modify or delete: ");
    string selectedflight = Console.ReadLine().ToUpper();

    try
    {
        // Check if the flight exists in the flights dictionary
        if (!flights.ContainsKey(selectedflight))
        {
            Console.WriteLine("Flight code not found.");
            return;
        }
    }
    catch
    {
        Console.WriteLine("Error");
        return;
    }

    Console.WriteLine("1. Modify Flight");
    Console.WriteLine("2. Delete Flight");
    Console.Write("Choose an option: ");

    int modordel;
    try
    {
        // Validate user input
        if (!int.TryParse(Console.ReadLine(), out modordel) || (modordel != 1 && modordel != 2))
        {
            Console.WriteLine("Invalid selection. Please enter 1 or 2.");
            return;
        }
    }
    catch
    {
        Console.WriteLine("Error");
        return;
    }

    try
    {
        // for modifying flight details
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
                Console.WriteLine("Invalid selection.");
                return;
            }

            switch (option)
            {
                // modifying basic info
                case 1:
                    try
                    {
                        Console.Write("Enter new Origin: ");
                        string neworigin = Console.ReadLine();

                        Console.Write("Enter new Destination: ");
                        string newdest = Console.ReadLine();

                        Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy hh:mm tt): ");
                        DateTime newdatetime;
                        if (DateTime.TryParse(Console.ReadLine(), out newdatetime))
                        {
                            Flight selectedFlight = flights[selectedflight];
                            selectedFlight.origin = neworigin;
                            selectedFlight.destination = newdest;
                            selectedFlight.expectedTime = newdatetime;

                            Console.WriteLine("Flight information updated!");
                            DisplayFlightDetails(selectedFlight, AirlinesDictionary, boardingGates);
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format!");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    break;

                // modifiying flight status
                case 2:
                    try
                    {
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
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    break;

                // update special code
                case 3:
                    try
                    {
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
                            Console.WriteLine("Special Request Code: None");
                        }

                        Console.Write("Enter new Special Request Code: ");
                        string src = Console.ReadLine().ToUpper();

                        if (src != "LWTT" && src != "DDJB" && src != "CFFT")
                        {
                            Console.WriteLine("Invalid Special Code!");
                            break;
                        }

                        if (found)
                        {
                            foreach (var flightDetails in flightList)
                            {
                                if (flightDetails[0] == selectedflight && flightDetails.Count > 4)
                                {
                                    flightDetails[4] = src;
                                    Console.WriteLine($"Flight {flightDetails[0]} Special Request updated to {flightDetails[4]}.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No flight found to update.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    break;

                // assign gate
                case 4:
                    try
                    {
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
                            Console.WriteLine("Invalid gate or gate is already assigned to another flight!");
                        }

                        DisplayFlightDetails(flights[selectedflight], AirlinesDictionary, boardingGates);
                    }
                    catch
                    {
                        Console.WriteLine("Error");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
        // delete flight
        else if (modordel == 2)
        {
            try
            {
                flights.Remove(selectedflight);
                Console.WriteLine($"Flight {selectedflight} deleted.");
            }
            catch
            {
                Console.WriteLine("Error");
            }
        }
    }
    catch
    {
        Console.WriteLine("Error");
    }
}



void DisplayFlightDetails(Flight selectedFlight, Dictionary<string, string> AirlinesDictionary, Dictionary<string, BoardingGate> boardingGates)
{
    // Extract the first two characters of the flight number as the airline code
    string airlineCode = selectedFlight.flightNumber.Substring(0, 2);

    string airlineName = "";
    // Look up the airline name using the airline code from airlines dictionary
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

    // Loops through flightList to find any matching special request code for the flight
    foreach (var flightDetails in flightList)
    {
        if (flightDetails[0] == selectedFlight.flightNumber && flightDetails.Count > 4)
        {
            specialRequestCode = flightDetails[4];
            found = true;
            break;
        }
    }

    // Display special request code or "None" if not found
    if (found)
    {
        Console.WriteLine($"Special Request Code: {specialRequestCode}");
    }
    else
    {
        Console.WriteLine($"Special Request Code: None");
    }

    string boardingGate = "N/A";
    // Look for the boarding gate assigned to the current flight
    foreach (var gate in boardingGates.Values)
    {
        if (gate.flight == selectedFlight)
        {
            boardingGate = gate.gateName;
            break;
        }
    }
    // Display the boarding gate status
    Console.WriteLine($"Boarding Gate: {boardingGate}");
    Console.WriteLine();
}


void SortDisplayList()
{
    // Convert the flights dictionary values to a list for sorting
    List<Flight> sortedFlights = flights.Values.ToList();

    sortedFlights.Sort();
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");

    Console.WriteLine("{0,-12}{1,-22}{2,-20}{3,-20}{4,-22}{5,-12}{6,-10}{7,10}", "Flight No.", "Airline Name", "Origin", "Destination", "Departure Time", "Status", "Special Code", "Gate");

    // loop through each sorted flight and display its details
    foreach (var flight in sortedFlights)
    {
        string airlineCode = flight.flightNumber.Substring(0, 2);
        string airlineName = AirlinesDictionary.ContainsKey(airlineCode) ? AirlinesDictionary[airlineCode] : "Unknown";

        string specialRequestCode = "N/A";

        // get special request code for the flight, else "N/A"
        foreach (var flightDetails in flightList)
        {
            if (flightDetails[0] == flight.flightNumber && flightDetails.Count > 4)
            {
                specialRequestCode = flightDetails[4];
                break;
            }
        }

        string boardingGate = "N/A";
        // Find the boarding gate for the flight else "N/A"
        foreach (var gate in boardingGates.Values)
        {
            if (gate.flight == flight)
            {
                boardingGate = gate.gateName;
                break;
            }
        }
        string formattedTime = flight.expectedTime.ToString("dd/MM/yyyy hh:mm tt");
        Console.WriteLine("{0,-12}{1,-22}{2,-20}{3,-20}{4,-22}{5,-12}{6,-10}{7,11}", flight.flightNumber, airlineName, flight.origin, flight.destination, formattedTime, flight.status, specialRequestCode, boardingGate);
    }
}

// Advanced(a) (Puru)
void autoAssignGates()
{
    Queue<Flight> flightQuence = new Queue<Flight>();
    List<string> tempFlightList = new List<string>();
    int assignedGates = 0;
    foreach (List<string> flight in flightList)
    {
        foreach (KeyValuePair<string, BoardingGate> pair in boardingGates)
        {
            if (pair.Value.flight is not null)
            {
                tempFlightList.Add(pair.Value.flight.flightNumber);
            }
        }
    }
    foreach(List<string> flight in flightList)
    {
        foreach (KeyValuePair<string, Flight> keyValuePair in flights) 
        {
            if (!tempFlightList.Contains(flight[0]))
            {
                if (flight[0] == keyValuePair.Key)
                {
                    flightQuence.Enqueue(keyValuePair.Value);
                }
            }

        }

        
        
    }
    Console.WriteLine("Unassigned flights");
    foreach(Flight flight in flightQuence)
    {
        //Console.WriteLine(flight.flightNumber);
    }
    Console.WriteLine("Unassigned Gates");
    List<BoardingGate> unassingedBG = new List<BoardingGate> ();
    foreach (KeyValuePair<string,BoardingGate> pair in boardingGates)
    {
        if (pair.Value.flight is null)
        {
            //Console.WriteLine(pair.Key);
            unassingedBG.Add(pair.Value);
        }
    }
    Console.WriteLine("Updated Gates");
    Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-15}{4}", "Flight Number", "Origin", "Destination", "Boarding Gate", "Special Code");
    while (flightQuence.Count != 0)
    {

        Flight proccessedFlight = flightQuence.Dequeue();
        string bg = "";
        string specialCode = "";
        foreach (Flight flight in flightQuence)
        {

            foreach (List<string> listFlight in flightList)
            {
                if (proccessedFlight.flightNumber == listFlight[0])
                {
                    try
                    {
                        specialCode = listFlight[4];
                    }
                    catch
                    {
                        specialCode = "";
                    }
                }
            }
        }


        if (specialCode == "DDJB")
        {
            foreach (BoardingGate boardingGate in unassingedBG)
            {
                
                if (boardingGate.supportsDDJB)
                {
                    boardingGate.flight = proccessedFlight;
                    bg = boardingGate.gateName;
                    unassingedBG.Remove(boardingGate);
                    break;
                }


            }

        }
        else if (specialCode == "CFFT")
        {
            foreach (BoardingGate boardingGate in unassingedBG)
            {
                if (boardingGate.supportsCFFT)
                {
                    boardingGate.flight = proccessedFlight;
                    bg = boardingGate.gateName;
                    unassingedBG.Remove(boardingGate);
                    break;
                }


            }
        }
        else if (specialCode == "LWTT")
        {
            foreach (BoardingGate boardingGate in unassingedBG)
            {
                if (boardingGate.supportsLWTT)
                {
                    boardingGate.flight = proccessedFlight;
                    bg = boardingGate.gateName;
                    unassingedBG.Remove(boardingGate);
                    break;
                }


            }
        }
        else if (specialCode == "")
        {
            foreach (BoardingGate boardingGate in unassingedBG)
            {
                if (!boardingGate.supportsLWTT && !boardingGate.supportsDDJB && !boardingGate.supportsCFFT)
                {
                    boardingGate.flight = proccessedFlight;
                    bg = boardingGate.gateName;
                    unassingedBG.Remove(boardingGate);
                    break;
                }

            }

        }
        assignedGates += 1;
        if (specialCode != "") 
        {

            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-15}{4}", proccessedFlight.flightNumber, proccessedFlight.origin, proccessedFlight.destination, bg,specialCode);

            //Console.WriteLine(proccessedFlight.flightNumber);
            //Console.WriteLine(bg);
            //Console.WriteLine(proccessedFlight.origin);
            //Console.WriteLine(proccessedFlight.destination);
            //Console.WriteLine(specialCode);
        }
        else
        {
            Console.WriteLine("{0,-20}{1,-20}{2,-20}{3,-15}", proccessedFlight.flightNumber, proccessedFlight.origin, proccessedFlight.destination, bg);
            //Console.WriteLine(proccessedFlight.flightNumber);
            //Console.WriteLine(bg);
            //Console.WriteLine(proccessedFlight.origin);
            //Console.WriteLine(proccessedFlight.destination);
        }

    }
    Console.WriteLine($"Total proccess flights: {assigngates + assignedGates}");
    Console.WriteLine($"Automically assgined gates: {assignedGates}");
    Console.WriteLine($"Manually assigned gates: {assigngates}");
    //try
    //{
            
    //}

}

//advance b
void DisplayTotalFee(Terminal terminal)
{
    terminal.PrintAirlineFees(flightList);
}



