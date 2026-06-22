using System.Text.Json;
using System.IO;
using System.Collections.Generic;

static class DriverStorage
{
    private const string FilePath = "drivers.json"; 
    public static void Save(List<Driver> drivers)
    {
        List<DriverData> data = new();

        foreach(Driver driver in drivers)
        {
            List<double> driverLapTimes = new(driver.LapTimes);
            DriverData driverData = new DriverData { Name = driver.Name, LapTimes = driverLapTimes };
            data.Add(driverData);
        }

        string json = JsonSerializer.Serialize(data, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(FilePath, json);
    }

    public static List<Driver> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Driver>();
        }

        string json = File.ReadAllText(FilePath);
        List<DriverData>? data = JsonSerializer.Deserialize<List<DriverData>>(json);

        if (data == null)
        {
            return new List<Driver>();
        }
        
        List<Driver> drivers = new();

        foreach(DriverData driverData in data)
        {
            if (string.IsNullOrWhiteSpace(driverData.Name))
            {
                continue;
            }
            Driver driver = new Driver(driverData.Name);

            foreach(double lapTime in driverData.LapTimes)
            {
                driver.AddLapTime(lapTime);
            }
            
            drivers.Add(driver);
        }

        return drivers;
    }
}