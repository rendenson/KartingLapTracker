using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Driver> drivers = new List<Driver>();

        while (true)
        {
            Console.WriteLine("\n= Karting Lap Tracker = ");
            Console.WriteLine("1. Add driver");
            Console.WriteLine("2. Show all drivers");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: "); 

            string? input = Console.ReadLine();

            // Validation cycle
            while(input != "1" && input != "2" && input != "3")
            {
                Console.Write("Unknown option, please try again: ");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    Console.Write("Name a driver: ");
                    string? inputName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(inputName))
                    {
                        Console.WriteLine("Name cannot be empty.");
                        break;
                    }

                    Driver newDriver = new Driver(inputName);

                    drivers.Add(newDriver);
                    Console.WriteLine($"Driver '{inputName}' added.");
                    break;

                case "2":
                    Console.WriteLine("The list of all drivers:");
                    
                    if (drivers.Count == 0)
                    {
                        Console.WriteLine("No drivers yet...");
                    }

                    foreach(Driver driver in drivers)
                    {
                        Console.WriteLine($"\nDriver: {driver.Name}");  
                        
                        if (driver.LapTimes.Count != 0)
                        {
                            double? best = driver.GetBestLapTime();
                            Console.WriteLine($"Best lap: {best:F3} seconds");

                            double? avg = driver.GetAverageLapTime();
                            Console.WriteLine($"Average lap: {avg:F3} seconds");
                        }
                        else
                        {
                            Console.WriteLine("No lap times yet...");
                        }
                    }

                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.WriteLine("Successfully exited.");
                    return;
            }
        }
    }
}