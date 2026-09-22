namespace TruckingGoodIcecream.Core.Interfaces;

public interface IRandomNumberGenerator
{
    int Next(int minValue, int maxValue);
}