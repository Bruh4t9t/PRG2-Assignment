// See https://aka.ms/new-console-template for more information
using PRG2_Assingment;
using System;
using System.IO;

string filepath = "flights.csv";

List<List<string>> flightList = new List<List<string>>();
List<List<string>> nameList = new List<List<string>>();
foreach (string line in File.ReadLines("airlines.csv"))
{
    List<string> newList = new List<string>(line.Split(','));
    nameList.Add(newList);



}
nameList.RemoveAt(0);

foreach(List<string> lista in nameList)
{
    foreach(string name in lista)
    {
        Console.WriteLine(name);
    }
}
foreach  (string line in File.ReadLines("flights.csv")) //Use your file path
{
    List<string> newList = new List<string>(line.Split(','));
    flightList.Add(newList);

}
flightList.RemoveAt(0);
int padding = 30;
Console.WriteLine($"{"Flight number":padding}{"Airline Name":padding}{"Origin":padding}{"Destination":padding}{"Expected Departure/Arrival":padding}");
foreach(List<string> flight in flightList)
{
    foreach (List<string> name in nameList)
    {
        if (flight[0].Substring(0,2) == name[1])
        {
            
            Console.WriteLine($"{name[0]:padding}{flight[0]:padding}{flight[1]:padding}{flight[2]:padding}{flight[3]:padding}");
        }
    }
}