using System.Globalization;
namespace KartingLapTracker;

class Program
{
    static void Main()
    {
        List<Driver> drivers = DriverStorage.Load();

        while (true)
        {
            ShowMenu();

            string? input = Console.ReadLine();
            input = input?.Trim();

            // Validation cycle
            while (input is not ("1" or "2" or "3" or "4"))
            {
                Console.Write("Unknown option, please try again: ");
                input = Console.ReadLine();
                input = input?.Trim();
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
        Console.WriteLine("\nThe list of all drivers and their records:");

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

        Console.WriteLine("\n = Summary = ");

        if (drivers.Any())
        {
            int totalDrivers = drivers.Count;
            int totalLaps = drivers.Sum(d => d.LapTimes.Count);
            Console.WriteLine($"Total: {totalDrivers} drivers, {totalLaps} laps.");

            var driversWithLaps = drivers.Where(d => d.LapTimes.Count > 0).ToList();
            if (driversWithLaps.Any())
            {
                var fastest = driversWithLaps.MinBy(d => d.LapTimes.Min());
                Console.WriteLine($"Fastest driver: {fastest!.Name} ({fastest.LapTimes.Min():F3} seconds)");
            }
        }

        Console.Write("\nPress enter to continue...");
        Console.ReadLine();
    }

    static void AddDriver(List<Driver> drivers)
    {
        Console.Write("\nName a driver: ");
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

        Console.WriteLine("\nThe list of all drivers:");

        for (int i = 0; i < drivers.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {drivers[i].Name}");
        }

        Console.Write("\nEnter a driver's number: ");
        string? input = Console.ReadLine();
        input = input?.Trim();

        Driver? selected = null;

        if (int.TryParse(input, out int number))
        {
            if (number >= 1 && number <= drivers.Count)
            {
                selected = drivers[number - 1];
            }
        }

        if (selected == null)
        {
            Console.WriteLine("Incorrect input, please try again.");
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
        Console.Write("\nChoose an option: ");
    }
}