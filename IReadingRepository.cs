using GreenhouseIo.Models;

namespace GreenhouseIo.Repositories;

public interface IReadingRepository
{
    void Save(DailyReading reading);

    IEnumerable<DailyReading> GetAll();
}