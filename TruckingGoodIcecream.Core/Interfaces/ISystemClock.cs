namespace TruckingGoodIcecream.Core.Interfaces;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}