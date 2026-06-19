class Driver
{
    public string Name { get; }
    public List<double> LapTimes { get; }

    public Driver(string name)
    {
        Name = name;
        LapTimes = new List<double>();
    }

    public void AddLapTime(double lapTime)
    {
        if (lapTime <= 0)
        {
            throw new ArgumentException("Lap time must be greater than zero.");
        }

        LapTimes.Add(lapTime);
    }

    public double GetBestLapTime()
    {
        return LapTimes.Count == 0 ? 0 : LapTimes.Min();
    }

    public double GetAverageLapTime()
    {
        return LapTimes.Count == 0 ? 0 : LapTimes.Average();
    }
}

