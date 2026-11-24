namespace GreenhouseIo;
public class ElectroValve
{
    private static IoBoard board;
    private static int activeZone = -1;
    public ElectroValve(IoBoard ioBoard)
    {
        board = ioBoard;
    }

    public void Open(int zone)
    {
        if (activeZone != -1)
        {
            throw new InvalidOperationException(
                "O altă electrovalvă este deja activă."
            );
        }

        board.WriteDigital(zone, true);
        board.WriteDigital(7, true);

        activeZone = zone;
    }

    public void Close(int zone)
    {
        if (activeZone == -1)
            return;
        if(activeZone != zone)
        {
            throw new InvalidOperationException(
                "Se încearcă închiderea unei electrovalve care nu este activă."
            );
        }
        board.WriteDigital(7, false);
        board.WriteDigital(activeZone, false);

        activeZone = -1;
    }
}