using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Driver> drivers = DriverStorage.Load();

        while (true)
        {
            Console.WriteLine("\n= Karting Lap Tracker = ");
            Console.WriteLine("1. Add driver");
            Console.WriteLine("2. Add lap time");
            Console.WriteLine("3. Show all drivers");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: "); 

            string? input = Console.ReadLine();

            // Validation cycle
            while(input != "1" && input != "2" && input != "3" && input != "4")
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
                    if (drivers.Count == 0)
                    {
                        Console.WriteLine("Add a driver first.");
                        break;
                    }

                    Console.Write("Enter a driver's name: ");
                    string? driverName = Console.ReadLine();
                    driverName = driverName?.Trim();

                    Driver? selected = null;

                    foreach (Driver driver in drivers)
                    {
                        if (string.Equals(driver.Name, driverName, StringComparison.OrdinalIgnoreCase))
                        {
                            selected = driver;
                            break;
                        }
                    }    

                    if (selected == null)
                    {
                        Console.WriteLine("A driver is not found, please try again.");
                        break;
                    }         

                    Console.Write("Enter a lap time: ");
                    string? driverLap = Console.ReadLine();

                    if (double.TryParse(driverLap, out double value))
                    {
                        if (value > 0)
                        {
                            selected.AddLapTime(value);
                            Console.WriteLine($"Laptime {value:F3} for driver '{selected.Name}' added.");
                            break;
                        }
                    }

                    Console.WriteLine("Incorrect lap time, please try again.");

                    break;
                case "3":
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

                case "4":
                    DriverStorage.Save(drivers);
                    Console.WriteLine("Successfully saved and exited.");
                    return;
            }
        }
    }
}