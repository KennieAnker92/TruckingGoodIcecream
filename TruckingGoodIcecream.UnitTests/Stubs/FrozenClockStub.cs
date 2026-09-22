namespace TruckingGoodIcecream.UnitTests.Stubs;

using TruckingGoodIcecream.Core.Interfaces;

public class FrozenClockStub : ISystemClock
{
    public DateTime FrozenTime { get; set; } = new DateTime(2026, 7, 15, 14, 0, 0, DateTimeKind.Utc);
    public DateTime UtcNow => FrozenTime;
}