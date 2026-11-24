using GreenhouseIo.Models;

namespace GreenhouseIo.Repositories;

public class CsvReadingRepository : IReadingRepository
{
    private readonly string filePath;

    public CsvReadingRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public void Save(DailyReading reading)
    {
        bool fileExists = File.Exists(filePath);

        using StreamWriter writer = new(filePath, append: true);

        if (!fileExists)
        {
            writer.WriteLine(
                "Date," + string.Join(",", reading.ZoneMinutes.Keys));
        }

        writer.WriteLine(
            $"{reading.Date:yyyy-MM-dd}," +
            string.Join(",", reading.ZoneMinutes.Values));
    }

    public IEnumerable<DailyReading> GetAll()
    {
        if (!File.Exists(filePath))
            return Enumerable.Empty<DailyReading>();

        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length <= 1)
            return Enumerable.Empty<DailyReading>();

        string[] zoneNames = lines[0].Split(',').Skip(1).ToArray();

        return lines.Skip(1)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                string[] values = line.Split(',');

                DailyReading reading = new()
                {
                    Date = DateOnly.Parse(values[0])
                };

                for (int i = 0; i < zoneNames.Length; i++)
                {
                    reading.ZoneMinutes[zoneNames[i]] =
                        int.Parse(values[i + 1]);
                }

                return reading;
            })
            .ToList();
    }
}