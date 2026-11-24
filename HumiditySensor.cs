namespace GreenhouseIo;
public class HumiditySensor
{
    private IoBoard ioBoard;
    public HumiditySensor(IoBoard ioBoard)
    {
        this.ioBoard = ioBoard;
    }
    public int checkHumidity(int zone)
    {
        int raw = ioBoard.ReadAnalog(zone);
        if (raw <= 0 || raw >= 1023)
        {
            throw new InvalidOperationException(
                "Sensor kaput gegangen."
            );
        }
        double percentage = (1023 - raw) * 100.0 / 1023;
        return (int)Math.Round(percentage);
    }
}