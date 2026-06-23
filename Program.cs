using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        List<Driver> drivers = DriverStorage.Load();

        while (true)
        {
            ShowMenu();

            string? input = Console.ReadLine();

            // Validation cycle
            while (input is not ("1" or "2" or "3" or "4"))
            {
                Console.Write("Unknown option, please try again: ");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    AddDriver(drivers);
                    break;

                case "2":
                    AddLapTime(drivers);
                    break;
                case "3":
                    ShowDrivers(drivers);
                    break;

                case "4":
                    DriverStorage.Save(drivers);
                    Console.WriteLine("Successfully saved and exited.");
                    return;
            }
        }
    }

    static void ShowDrivers(List<Driver> drivers)
    {
        Console.WriteLine("The list of all drivers:");

        if (drivers.Count == 0)
        {
            Console.WriteLine("No drivers yet...");
        }

        foreach (Driver driver in drivers)
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
    }

    static void AddDriver(List<Driver> drivers)
    {
        Console.Write("Name a driver: ");
        string? inputName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputName))
        {
            Console.WriteLine("Name cannot be empty.");
            return;
        }

        Driver newDriver = new(inputName);

        drivers.Add(newDriver);
        Console.WriteLine($"Driver '{inputName}' added.");
    }

    static void AddLapTime(List<Driver> drivers)
    {
        if (drivers.Count == 0)
        {
            Console.WriteLine("Add a driver first.");
            return;
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
            return;
        }

        Console.Write("Enter a lap time: ");
        string? driverLap = Console.ReadLine();

        if (double.TryParse(driverLap, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
        {
            if (value > 0)
            {
                selected.AddLapTime(value);
                Console.WriteLine($"Laptime {value:F3} for driver '{selected.Name}' added.");
                return;
            }
        }

        Console.WriteLine("Incorrect lap time, please try again.");
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n= Karting Lap Tracker = ");
        Console.WriteLine("1. Add driver");
        Console.WriteLine("2. Add lap time");
        Console.WriteLine("3. Show all drivers");
        Console.WriteLine("4. Exit");
        Console.Write("Choose an option: ");
    }
}