class Driver
{
    private readonly List<double> _lapTimes = new();
    public IReadOnlyList<double> LapTimes => _lapTimes;
    public string Name { get; }
    

    public Driver(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Driver name cannot be empty.", nameof(name));
        }

        Name = name.Trim();
    }

    public void AddLapTime(double lapTime)
    {
        if (lapTime <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(lapTime), "Lap time must be greater than zero.");
        }

        _lapTimes.Add(lapTime);
    }

    public double? GetBestLapTime()
    {
        return _lapTimes.Count == 0 ? null : _lapTimes.Min();
    }

    public double? GetAverageLapTime()
    {
        return _lapTimes.Count == 0 ? null : _lapTimes.Average();
    }
}

