namespace GreenhouseIo;
public class ZoneCucumbers : Zone
{
    public const int prag = 45;
    public ZoneCucumbers(HumiditySensor hum, ElectroValve valve) : base(prag, hum, valve)
    {
    }


}