namespace GreenhouseIo;
public class ZoneSeedlings : Zone
{
    public const int prag = 55;
    public ZoneSeedlings(HumiditySensor hum, ElectroValve valve) : base(prag, hum, valve)
    {
    }

}