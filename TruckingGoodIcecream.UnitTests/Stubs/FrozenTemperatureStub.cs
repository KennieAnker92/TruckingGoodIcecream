namespace TruckingGoodIcecream.UnitTests.Stubs;

using TruckingGoodIcecream.Core.Interfaces;

public class FrozenTemperatureStub : ITemperatureSensor
{
    public double TemperatureToReturn { get; set; } = 22.0;
    public double GetAmbientTemperatureCelsius() => TemperatureToReturn;
}