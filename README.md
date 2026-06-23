# Karting Lap Tracker

A console application for tracking karting drivers and their lap times. Drivers and their recorded laps are stored persistently, so the data is kept between runs.

## Features

- Add drivers with input validation (no empty or whitespace-only names)
- Record lap times for a driver, with validation against non-numeric or non-positive values
- Select a driver from a numbered list when adding lap times
- View all drivers with their best and average lap times
- A summary showing the total number of drivers, total laps, and the fastest driver
- Automatic saving and loading of data via a JSON file
- Robust loading that recovers gracefully from a missing or corrupted data file

## Technologies

- C#
- .NET 10.0
- System.Text.Json (for serialization)

## How to run

Requires the [.NET SDK](https://dotnet.microsoft.com/download) installed.

```
git clone https://github.com/rendenson/KartingLapTracker
cd KartingLapTracker
dotnet run
```

Driver data is stored in `drivers.json`, which is created automatically in the project folder on first run. The application starts with an empty list if no data file exists yet.

## Usage

When the application starts, it shows a menu:

1. **Add driver** — enter a driver's name.
2. **Add lap time** — choose a driver from the numbered list and enter a lap time in seconds (e.g. `52.249`).
3. **Show all drivers** — lists every driver with their best and average lap time, followed by a summary.
4. **Exit** — saves all data to `drivers.json` and closes the application.

Invalid menu options and malformed input (empty names, non-numeric or non-positive lap times, out-of-range driver numbers) are handled gracefully without crashing. Lap times are parsed using `CultureInfo.InvariantCulture`, so a dot is always used as the decimal separator regardless of the system locale.

## Project structure

- `Program.cs` — application entry point and console menu loop
- `Driver.cs` — the domain model; encapsulates a driver's name and lap times, and exposes lap times as a read-only collection
- `DriverData.cs` — a data transfer object (DTO) used for JSON serialization
- `DriverStorage.cs` — handles saving and loading drivers to and from the JSON file

## What I learned

- Encapsulation: hiding the internal list and exposing it as an `IReadOnlyList`
- Separating the domain model (`Driver`) from the serialization model (`DriverData`) using a DTO
- JSON serialization and safe file handling, including missing or corrupted data files
- Input validation and keeping validation rules consistent across the application
- Using LINQ (`Where`, `Sum`, `MinBy`) to compute summary statistics
- Locale-independent number parsing with `CultureInfo.InvariantCulture`

## Future plans

- Add the ability to add multiple drivers in a row without returning to the menu
- Prevent duplicate driver names
- Allow editing or removing drivers and lap times
