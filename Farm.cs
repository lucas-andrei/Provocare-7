namespace GreenhouseIo;
using GreenhouseIo.Repositories;
using GreenhouseIo.Models;
public class Farm
{
    ZoneSeedlings zoneSeedlings;
    ZoneCucumbers zoneCucumbers;
    ZoneTomatoes zoneTomatoes;
    SmsGateway smsGateway;
    private readonly IReadingRepository readingRepository;
    public Farm(IoBoard ioBoard, SmsGateway smsGateway,IReadingRepository readingRepository)
    {
        this.readingRepository=readingRepository;
        this.smsGateway = smsGateway;
        HumiditySensor hum = new HumiditySensor(ioBoard);
        ElectroValve valve1 = new ElectroValve(ioBoard);
        ElectroValve valve2 = new ElectroValve(ioBoard);
        ElectroValve valve3 = new ElectroValve(ioBoard);
        zoneSeedlings = new ZoneSeedlings(hum, valve1);
        zoneCucumbers = new ZoneCucumbers(hum, valve2);
        zoneTomatoes = new ZoneTomatoes(hum, valve3);
    }
    public void Manage(DateTime now)
    {
        if (now.Hour >= 12 && now.Hour < 16)
            throw new Exception("Nu se udă plantele între orele 12 și 16!");
        smsGateway.Send(zoneSeedlings.Manage());
        smsGateway.Send(zoneCucumbers.Manage());
        smsGateway.Send(zoneTomatoes.Manage());
    }
    public void SaveDailyReport(DateOnly date)
    {
        DailyReading reading = new()
        {
            Date = date,
            ZoneMinutes = new Dictionary<string, int>
            {
                { "Tomatoes", zoneTomatoes.WateringMinutes },
                { "Cucumbers", zoneCucumbers.WateringMinutes },
                { "Seedlings", zoneSeedlings.WateringMinutes }
            }
        };

    readingRepository.Save(reading);
}
}
