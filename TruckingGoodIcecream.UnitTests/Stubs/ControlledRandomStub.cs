namespace TruckingGoodIcecream.UnitTests.Stubs;

using TruckingGoodIcecream.Core.Interfaces;

public class ControlledRandomStub : IRandomNumberGenerator
{
    public int ValueToReturn { get; set; } = 0;
    public int Next(int minValue, int maxValue) => ValueToReturn;
}