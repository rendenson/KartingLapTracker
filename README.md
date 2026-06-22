# Karting Lap Tracker

A console application for tracking karting drivers and their lap times. Drivers and their recorded laps are stored persistently, so the data is kept between runs.

## Features

- Add drivers with input validation (no empty or whitespace-only names)
- Record lap times for a driver, with validation against invalid or non-positive values
- View all drivers with their best and average lap times
- Case-insensitive driver lookup when adding lap times
- Automatic saving and loading of data via a JSON file

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
2. **Add lap time** — choose an existing driver by name and enter a lap time in seconds (e.g. `52.249`).
3. **Show all drivers** — lists every driver with their best and average lap time.
4. **Exit** — saves all data to `drivers.json` and closes the application.

Invalid menu options and malformed input (empty names, non-numeric or non-positive lap times) are handled gracefully without crashing.

## Project structure

- `Program.cs` — application entry point and console menu loop
- `Driver.cs` — the domain model; encapsulates a driver's name and lap times, and exposes lap times as a read-only collection
- `DriverData.cs` — a data transfer object (DTO) used for JSON serialization
- `DriverStorage.cs` — handles saving and loading drivers to and from the JSON file

## What I learned

- Encapsulation: hiding the internal list and exposing it as an `IReadOnlyList`
- Separating the domain model (`Driver`) from the serialization model (`DriverData`) using a DTO
- JSON serialization and safe file handling, including the case where the data file does not exist
- Input validation and keeping validation rules consistent across the application

## Future plans

- Add the ability to add multiple drivers in a row without returning to the menu
- Show an indexed list of drivers when selecting one
- Use `CultureInfo.InvariantCulture` for fully locale-independent number parsing
