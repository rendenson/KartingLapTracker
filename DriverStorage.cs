using System.Text.Json;
using System.IO;
using System.Collections.Generic;

static class DriverStorage
{
    private const string FilePath = "drivers.json"; 

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static void Save(List<Driver> drivers)
    {
        List<DriverData> data = new();

        foreach(Driver driver in drivers)
        {
            List<double> driverLapTimes = new(driver.LapTimes);
            DriverData driverData = new() { Name = driver.Name, LapTimes = driverLapTimes };
            data.Add(driverData);
        }

        string json = JsonSerializer.Serialize(data, JsonOptions);
        File.WriteAllText(FilePath, json);
    }

    public static List<Driver> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new();
        }

        List<DriverData>? data;

        try
        {
            string json = File.ReadAllText(FilePath);
            data = JsonSerializer.Deserialize<List<DriverData>>(json);
        }
        catch(JsonException)
        {
            data = null;
        }
        
        if (data == null)
        {
            return new();
        }
        
        List<Driver> drivers = new();

        foreach(DriverData driverData in data)
        {
            if (string.IsNullOrWhiteSpace(driverData.Name))
            {
                continue;
            }
            Driver driver = new(driverData.Name);

            foreach(double lapTime in driverData.LapTimes ?? new())
            {
                driver.AddLapTime(lapTime);
            }
            
            drivers.Add(driver);
        }

        return drivers;
    }
}