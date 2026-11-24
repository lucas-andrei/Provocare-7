namespace GreenhouseIo;
public abstract class Zone
{
    static int ZoneCounter = 0;
    private int zoneNumber;
    private HumiditySensor hum;
    private ElectroValve valve;
    private readonly int prag;
    private bool moreWateringNeeded = false;
    public int WateringMinutes {get; private set;}
    public Zone(int prag, HumiditySensor hum, ElectroValve valve)
    {
        zoneNumber = ZoneCounter;
        ZoneCounter++;
        this.prag = prag;
        this.hum = hum;
        this.valve = valve;
        
    }
    private int checkHumidity()
    {
        return hum.checkHumidity(zoneNumber);
    }
    private string startElectricalValve()
    {
        int currentHumidity;
        try
        {
            currentHumidity = checkHumidity();
            Console.WriteLine($"Zone {zoneNumber} current humidity: {currentHumidity}%");
        }
        catch (Exception)
        {
            return "Error reading humidity";
        }
        if(moreWateringNeeded&&currentHumidity>=prag+15)
        {
           try
            {
                valve.Close(zoneNumber);
                moreWateringNeeded = false;

                return $"Zone {zoneNumber} watering stopped. " +
                    $"Current humidity: {currentHumidity}%";
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error closing valve for zone {zoneNumber}: {ex.Message}");
                return "";
            }
        }
        if (!moreWateringNeeded && currentHumidity < prag)
        {
            try
            {
                valve.Open(zoneNumber);
                moreWateringNeeded = true;

                Console.WriteLine(
                    $"Zone {zoneNumber} watering started. " +
                    $"Current humidity: {currentHumidity}%");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error opening valve for zone {zoneNumber}: {ex.Message}");
                return "";
            }
        }
        if(moreWateringNeeded==true)
        {
            WateringMinutes++;
        }
        return "";
    }
    public string Manage()
    {
        return startElectricalValve();
    }

}