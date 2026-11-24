namespace GreenhouseIo.Models;

public class DailyReading
{
    public DateOnly Date { get; set; }

    public Dictionary<string, int> ZoneMinutes { get; set; } = new();
}