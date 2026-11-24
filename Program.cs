// ============================================================================
//  Simularea unei zile în seră.
//
//  Bucla de mai jos NU se modifică: ea joacă rolul timpului real
//  (un pas = un minut) și al serei (board.Tick()).
//  Voi scrieți: crearea obiectelor înainte de buclă, apelul controlerului
//  în locul comentariului TODO și afișarea jurnalului după buclă.
// ============================================================================
using GreenhouseIo;
using GreenhouseIo.Repositories;
IoBoard board = new("COM5");
IReadingRepository repository =
    new CsvReadingRepository("irigare.csv");
Farm farm = new Farm(board, new SmsGateway("0722 123 456"), repository);
DateTime start = new(2026, 7, 15, 0, 0, 0);
for (int minute = 0; minute < 24 * 60; minute++)
{
    DateTime now = start.AddMinutes(minute);
    try
    {
        farm.Manage(now);
    }
    catch (Exception)
    {
    }
    board.Tick();
}
farm.SaveDailyReport(DateOnly.FromDateTime(start));

Console.WriteLine($"Încălcări ale instalației: {IoBoard.Violations}");
