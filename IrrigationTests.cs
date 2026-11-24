using NUnit.Framework;

namespace GreenhouseIo.Tests;

[TestFixture]
public class HumiditySensorTests
{
    private IoBoard board = null!;
    private HumiditySensor sensor = null!;

    [SetUp]
    public void Setup()
    {
        board = new IoBoard("COM1");
        sensor = new HumiditySensor(board);
    }

    [TestCase(0, 42)]
    [TestCase(1, 47)]
    [TestCase(2, 70)]
    public void CheckHumidity_ValidChannel_ReturnsCorrectHumidity(
        int zone, int expected)
    {
        // Act
        int result = sensor.checkHumidity(zone);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CheckHumidity_InvalidChannel_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sensor.checkHumidity(3));
    }

    [Test]
    public void CheckHumidity_NegativeChannel_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(
            () => sensor.checkHumidity(-1));
    }
}