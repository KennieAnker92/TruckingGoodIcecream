namespace TruckingGoodIcecream.Core.Services;

using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Data;
using TruckingGoodIcecream.Core.Interfaces;
using TruckingGoodIcecream.Core.Models.Dtos;
using TruckingGoodIcecream.Core.Models.Entities;

public class IcecreamService
{
    private readonly DataConnection _db;
    private readonly ISystemClock _clock;
    private readonly IRandomNumberGenerator _rng;
    private readonly ITemperatureSensor _tempSensor;

    public IcecreamService(
        DataConnection db,
        ISystemClock clock,
        IRandomNumberGenerator rng,
        ITemperatureSensor tempSensor)
    {
        _db = db;
        _clock = clock;
        _rng = rng;
        _tempSensor = tempSensor;
    }

    // --- Exercises 1 & 2 function as examples ---

    /*
     * Example exercise 1: Create Flavor
     * Explanation: When adding a new flavor to the menu, we must stamp it with the current timestamp.
     * We use _clock.UtcNow so that in tests, the creation date is 100% deterministic.
     */
    public FlavorSummaryDto AddNewFlavor(CreateFlavorDto dto)
    {
        var now = _clock.UtcNow;
        var entity = new FlavorEntity
        {
            Name = dto.Name,
            BasePrice = dto.BasePrice,
            ScoopsInStock = dto.InitialScoops,
            IsActive = true,
            AddedAtUtc = now
        };

        int newId = Convert.ToInt32(_db.InsertWithDecimalIdentity(entity));
        return new FlavorSummaryDto(newId, entity.Name, entity.BasePrice, entity.ScoopsInStock, entity.AddedAtUtc);
    }

    /*
     * Example exercise 2: Calculate Happy Hour Price
     * Explanation: Between 2 PM (14) and 4 PM (16), all scoops are 50% off. 
     * Reading _clock.UtcNow.Hour allows unit tests to freeze time at different hours.
     */
    public decimal CalculateScoopPrice(decimal basePrice)
    {
        int hour = _clock.UtcNow.Hour;
        bool isHappyHour = hour >= 14 && hour < 16;
        return isHappyHour ? basePrice * 0.50m : basePrice;
    }

    // --- These exercises are partially complete. ---

    /*
     * Exercise 3: Mystery Coupon
     * Explanation: Generate a mystery discount code for customers. The discount percentage 
     * is randomly chosen between 5% and 25% using _rng.Next().
     */
    public string GenerateMysteryCoupon()
    {
        // TODO: Complete the line below using _rng.Next(5, 25)
        int percentage = 0; // REPLACE THIS
        return $"DISCOUNT-{percentage}%";
    }

    /*
     * Exercise 4: Sell Scoops
     * Explanation: When scoops are sold, update the database by subtracting scoopsSold 
     * from the current inventory.
     */
    public void SellScoops(int flavorId, int scoopsSold)
    {
        var flavor = _db.GetTable<FlavorEntity>().FirstOrDefault(f => f.Id == flavorId);
        if (flavor == null) return;

        // TODO: Subtract scoopsSold from flavor.ScoopsInStock and update the record in _db
        // _db.Update(flavor);
    }

    /*
     * Exercise 5: Get Flavors Added After Threshold
     * Explanation: Retrieve active ice cream flavors added on or after a specified cutoff date.
     */
    public List<FlavorSummaryDto> GetFlavorsAddedAfter(DateTime threshold)
    {
        // TODO: Filter database records by AddedAtUtc >= threshold and IsActive == true
        return _db.GetTable<FlavorEntity>()
            .Where(f => f.IsActive /* ADD CONDITION HERE */)
            .Select(f => new FlavorSummaryDto(f.Id, f.Name, f.BasePrice, f.ScoopsInStock, f.AddedAtUtc))
            .ToList();
    }

    /*
     * Exercise 6: Retire Seasonal Flavors
     * Explanation: At the end of a season, deactivate all flavors created before a cutoff date.
     */
    public int RetireSeasonalFlavors(DateTime cutoffDate)
    {
        var staleFlavors = _db.GetTable<FlavorEntity>()
            .Where(f => f.IsActive && f.AddedAtUtc < cutoffDate)
            .ToList();

        // TODO: Loop through staleFlavors, set IsActive = false, call _db.Update(f), and return count retired.
        return 0;
    }

    /*
     * Exercise 7: Purchase Ice cream
     * Explanation: Calculate total price and return a structured DTO receipt stamped with _clock.UtcNow.
     */
    public IcecreamReceiptDto PurchaseIcecream(int flavorId, int scoopsPurchased)
    {
        var flavor = _db.GetTable<FlavorEntity>().First(f => f.Id == flavorId);
        decimal totalCost = flavor.BasePrice * scoopsPurchased;

        // TODO: Return an IcecreamReceiptDto stamped with _clock.UtcNow
        return new IcecreamReceiptDto(flavor.Id, flavor.Name, scoopsPurchased, totalCost, DateTime.MinValue);
    }

    /*
     * Exercise 8: Select Mystery Flavor
     * Explanation: Pick a random active flavor from the database using _rng.Next().
     */
    public int SelectMysteryFlavorId()
    {
        var activeIds = _db.GetTable<FlavorEntity>()
            .Where(f => f.IsActive)
            .Select(f => f.Id)
            .ToList();

        if (!activeIds.Any()) return 0;

        // TODO: Use _rng.Next(0, activeIds.Count) to select an index and return activeIds[selectedIndex]
        return 0;
    }

    /*
     * Exercise 9: Calculate Melt Time
     * Explanation: Ice cream melts faster on hot days! Check ambient temperature via _tempSensor:
     * - >= 30.0°C: 5 minutes
     * - >= 20.0°C: 12 minutes
     * - Otherwise: 20 minutes
     */
    public int CalculateMeltTimeMinutes()
    {
        double temp = _tempSensor.GetAmbientTemperatureCelsius();

        // TODO: Complete temperature threshold checks
        if (temp >= 30.0) return 5;
        return 20;
    }

    /*
     * Exercise 10: Heatwave Price Adjustment
     * Explanation: If ambient temperature > 32.0°C, apply a 20% emergency heatwave discount (basePrice * 0.80m).
     */
    public decimal GetHeatwaveAdjustedPrice(decimal basePrice)
    {
        double temp = _tempSensor.GetAmbientTemperatureCelsius();

        // TODO: If temp > 32.0, return basePrice * 0.80m, else return basePrice
        return basePrice;
    }

    // --- BLANK LOGIC EXERCISES FOR FULL TDD (11 - 20) ---

    /* Exercise 11: Night Shift Detector (true if hour >= 22 or < 6) */
    public bool IsNightShift() => throw new NotImplementedException();

    /* Exercise 12: Issue Raffle Ticket ("TICKET-{number}" via _rng.Next(1000, 9999)) */
    public string IssueRaffleTicket() => throw new NotImplementedException();

    /* Exercise 13: Restock Flavor Scoops in DB */
    public void RestockScoops(int flavorId, int additionalScoops) => throw new NotImplementedException();

    /* Exercise 14: Return sum of ScoopsInStock across all active flavors */
    public int GetTotalScoopsInStock() => throw new NotImplementedException();

    /* Exercise 15: Free Cone Day Evaluator (true if Month == 7 and Day == 15) */
    public bool IsFreeConeDay() => throw new NotImplementedException();

    /* Exercise 16: Custom Cone Order Cost ((basePrice + (wantsWaffleCone ? 1.50m : 0m)) * scoops) */
    public decimal CalculateCustomOrderPrice(decimal basePrice, int scoops, bool wantsWaffleCone) => throw new NotImplementedException();

    /* Exercise 17: Free Topping Spinner (_rng.Next(0,3) -> Sprinkles, Hot Fudge, Whipped Cream) */
    public string SpinForFreeTopping() => throw new NotImplementedException();

    /* Exercise 18: Monday Stock Audit Checklist (true if _clock.UtcNow.DayOfWeek == DayOfWeek.Monday) */
    public bool IsMondayStockAuditDue() => throw new NotImplementedException();

    /* Exercise 19: Check Fast Melt Condition (true if temp > 28.0 AND CalculateMeltTimeMinutes() < 10) */
    public bool IsFastMeltHazard() => throw new NotImplementedException();

    /* Exercise 20: Cold Day Hot Fudge Promo (true if temp < 15.0°C AND hour >= 12) */
    public bool IsColdDayPromoActive() => throw new NotImplementedException();
}