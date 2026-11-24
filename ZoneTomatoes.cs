namespace GreenhouseIo;
public class ZoneTomatoes : Zone
{
    public const int prag = 35;
    public ZoneTomatoes(HumiditySensor hum, ElectroValve valve) : base(prag, hum, valve)
    {
    }


}