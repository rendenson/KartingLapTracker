using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Driver> drivers = new List<Driver>();

        Driver oliver = new Driver("Oliver");
        oliver.AddLapTime(52.249);
        oliver.AddLapTime(51.977);
        oliver.AddLapTime(52.501);
        drivers.Add(oliver);

        Driver james = new Driver("James");
        james.AddLapTime(53.865);
        james.AddLapTime(52.903);
        james.AddLapTime(52.239);
        drivers.Add(james);

        Driver henry = new Driver("Henry");
        henry.AddLapTime(54.139);
        henry.AddLapTime(52.349);
        henry.AddLapTime(51.023);
        drivers.Add(henry);

        foreach (Driver driver in drivers)
        {
            Console.WriteLine($"Driver: {driver.Name}");
            Console.WriteLine($"Best lap: {driver.GetBestLapTime():F3} seconds");
            Console.WriteLine($"Average lap: {driver.GetAverageLapTime():F3} seconds");
            Console.WriteLine();
        }
    }
}