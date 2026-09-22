namespace TruckingGoodIcecream.UnitTests;

using System;
using System.Collections.Generic;
using LinqToDB;
using Xunit;
using TruckingGoodIcecream.Core.Models.Entities;
using TruckingGoodIcecream.Core.Models.Dtos;
using TruckingGoodIcecream.Core.Services;
using TruckingGoodIcecream.UnitTests.Fixtures;
using TruckingGoodIcecream.UnitTests.Stubs;

public class IcecreamServiceTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;
    private readonly FrozenClockStub _clockStub = new();
    private readonly ControlledRandomStub _randomStub = new();
    private readonly FrozenTemperatureStub _tempStub = new();

    public IcecreamServiceTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    // --- Example exercises ---

    /*
     * Example exercise 1: Create Flavor
     * AddNewFlavor stamps AddedAtUtc directly from the clock stub.
     */
    [Fact]
    public void AddNewFlavor_SetsAddedAtFromClockStub()
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        _clockStub.FrozenTime = new DateTime(2026, 7, 15, 10, 0, 0, DateTimeKind.Utc);
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        // Act
        var result = service.AddNewFlavor(new CreateFlavorDto("Mint Chip", 3.50m, 50));

        // Assert
        Assert.Equal(new DateTime(2026, 7, 15, 10, 0, 0, DateTimeKind.Utc), result.AddedAtUtc);
    }

    /*
     * Example exercise 2: Calculate Happy Hour Price
     * Happy hour discount behavior across hour boundaries using [Theory].
     */
    [Theory]
    [InlineData(14, 4.00, 2.00)]
    [InlineData(11, 4.00, 4.00)]
    public void CalculateScoopPrice_AppliesHappyHourDiscountBasedOnClockHour(int hour, decimal basePrice, decimal expectedPrice)
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        _clockStub.FrozenTime = new DateTime(2026, 7, 15, hour, 0, 0, DateTimeKind.Utc);
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        // Act
        decimal price = service.CalculateScoopPrice(basePrice);

        // Assert
        Assert.Equal(expectedPrice, price);
    }

    // --- These tests will be in a state of mixed completion (03 - 10) ---

    /*
     * Exercise 3: Mystery Coupon
     * Verify coupon formatting when ControlledRandomStub returns 15.
     * Missing: Act & Assert
     */
    [Fact]
    public void GenerateMysteryCoupon_ReturnsFormattedCouponFromRandomStub()
    {
        // Arrange provided
        using var db = _fixture.CreateConnection();
        _randomStub.ValueToReturn = 15;
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        // TODO Act: Call service.GenerateMysteryCoupon()
        // TODO Assert: Verify coupon equals "DISCOUNT-15%"
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 4: Sell Scoops
     * Verify inventory subtraction in SQLite database.
     * Missing: Arrange (Needs flavor inserted into DB)
     */
    [Fact]
    public void SellScoops_UpdatesScoopsInDatabase()
    {
        // TODO Arrange: Create db connection, service, and add a flavor with 20 scoops
        
        // Act provided
        // service.SellScoops(flavor.Id, scoopsSold: 5);

        // Assert provided
        // var updated = db.GetTable<FlavorEntity>().First(f => f.Id == flavor.Id);
        // Assert.Equal(15, updated.ScoopsInStock);
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 5: Get Flavors Added After Threshold
     * Filtering entities by timestamp using frozen clock stubs.
     * Missing: Assert
     */
    [Fact]
    public void GetFlavorsAddedAfter_FiltersOutOlderFlavors()
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        _clockStub.FrozenTime = new DateTime(2026, 7, 15, 9, 0, 0, DateTimeKind.Utc);
        service.AddNewFlavor(new CreateFlavorDto("Vanilla", 2.50m, 100));

        _clockStub.FrozenTime = new DateTime(2026, 7, 15, 13, 0, 0, DateTimeKind.Utc);
        service.AddNewFlavor(new CreateFlavorDto("Mango", 3.50m, 80));

        // Act
        var results = service.GetFlavorsAddedAfter(new DateTime(2026, 7, 15, 11, 0, 0, DateTimeKind.Utc));

        // TODO Assert: Assert results.Count is 1 and results[0].Name is "Mango"
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 6: Retire Seasonal Flavors
     * Bulk update verification.
     * Missing: Act
     */
    [Fact]
    public void RetireSeasonalFlavors_DeactivatesFlavorsOlderThanCutoff()
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);
        db.Insert(new FlavorEntity { Name = "Pumpkin Spice", BasePrice = 4.00m, IsActive = true, AddedAtUtc = new DateTime(2026, 6, 1, 0, 0, 0, DateTimeKind.Utc) });

        // TODO Act: Call service.RetireSeasonalFlavors(new DateTime(2026, 7, 1, 0, 0, 0, DateTimeKind.Utc))

        // Assert
        // Assert.Equal(1, retiredCount);
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 7: Purchase Ice cream
     * DTO mapping and receipt timestamping.
     * Missing: Arrange
     */
    [Fact]
    public void PurchaseIcecream_StampsCurrentTimeFromClockStub()
    {
        // TODO Arrange: Freeze clock at 15:45, create service, add flavor "Strawberry" ($3.00)

        // Act & Assert provided
        // var receipt = service.PurchaseIcecream(flavor.Id, scoopsPurchased: 2);
        // Assert.Equal(new DateTime(2026, 7, 15, 15, 45, 0, DateTimeKind.Utc), receipt.PurchasedAtUtc);
        // Assert.Equal(6.00m, receipt.TotalCost);
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 8: Select Mystery Flavor
     * Using random index stub to pick from database.
     * Missing: Arrange (Setting up ControlledRandomStub)
     */
    [Fact]
    public void SelectMysteryFlavorId_UsesRandomStubToPickIndex()
    {
        using var db = _fixture.CreateConnection();
        // TODO Arrange: Set _randomStub.ValueToReturn = 1 to force picking index 1 (f2)
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);
        var f1 = service.AddNewFlavor(new CreateFlavorDto("Cookie Dough", 4.00m, 30));
        var f2 = service.AddNewFlavor(new CreateFlavorDto("Rocky Road", 4.50m, 20));

        // Act
        int selectedId = service.SelectMysteryFlavorId();

        // Assert
        Assert.Equal(f2.Id, selectedId);
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 9: Calculate Melt Time
     * Stubbing ambient hardware temperatures.
     * Missing: Act & Assert
     */
    [Theory]
    [InlineData(34.0, 5)]
    [InlineData(22.0, 12)]
    [InlineData(15.0, 20)]
    public void CalculateMeltTimeMinutes_EvaluatesTemperatureThresholds(double ambientTemp, int expectedMinutes)
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        _tempStub.TemperatureToReturn = ambientTemp;
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        // TODO Act: Call service.CalculateMeltTimeMinutes()
        // TODO Assert: Assert result equals expectedMinutes
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /*
     * Exercise 10: Heatwave Price Adjustment
     * Discount evaluation based on temperature sensor.
     * Missing: Act
     */
    [Theory]
    [InlineData(35.0, 5.00, 4.00)]
    [InlineData(25.0, 5.00, 5.00)]
    public void GetHeatwaveAdjustedPrice_AppliesDiscountOnHotDays(double temp, decimal basePrice, decimal expectedPrice)
    {
        // Arrange
        using var db = _fixture.CreateConnection();
        _tempStub.TemperatureToReturn = temp;
        var service = new IcecreamService(db, _clockStub, _randomStub, _tempStub);

        // TODO Act: Call service.GetHeatwaveAdjustedPrice(basePrice)

        // Assert
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
        // Assert.Equal(expectedPrice, adjustedPrice);
    }

    // --- FULL TDD BLANK TESTS (11 - 20) ---

    /* Exercise 11: Verify IsNightShift returns true for hour 23 and false for hour 14 */
    [Theory]
    [InlineData(23, true)]
    [InlineData(14, false)]
    public void IsNightShift_EvaluatesClockHour(int hour, bool expectedResult)
    {
        // TODO: Full TDD Setup (Arrange, Act, Assert)
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 12: Verify IssueRaffleTicket returns "TICKET-7777" when _randomStub returns 7777 */
    [Fact]
    public void IssueRaffleTicket_FormatsTicketFromRandomStub()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 13: Verify RestockScoops adds 20 scoops to a flavor starting with 10 */
    [Fact]
    public void RestockFlavorScoops_UpdatesScoopsInDatabase()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 14: Verify GetTotalScoopsInStock sums 20 + 30 = 50 across active flavors */
    [Fact]
    public void GetTotalScoopsInStock_SumsAllActiveFlavorStock()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 15: Verify IsFreeConeDay returns true for July 15 and false for Aug 20 */
    [Theory]
    [InlineData(7, 15, true)]
    [InlineData(8, 20, false)]
    public void IsFreeConeDay_ValidatesCalendarDate(int month, int day, bool expected)
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 16: Verify CalculateCustomOrderPrice waffle cone surcharge calculation */
    [Theory]
    [InlineData(3.00, 2, true, 9.00)]
    [InlineData(3.00, 2, false, 6.00)]
    public void CalculateCustomOrderPrice_AddsWaffleConeSurcharge(decimal basePrice, int scoops, bool waffle, decimal expected)
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 17: Verify SpinForFreeTopping maps index 1 to "Hot Fudge" */
    [Fact]
    public void SpinForFreeTopping_MapsRandomIndexToToppingName()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 18: Verify IsMondayStockAuditDue returns true when clock is frozen on a Monday */
    [Fact]
    public void IsMondayStockAuditDue_ReturnsTrueOnMondays()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 19: Verify IsFastMeltHazard returns true when ambient temp is 35.0°C */
    [Fact]
    public void IsFastMeltHazard_ReturnsTrueWhenTemperatureIsHigh()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }

    /* Exercise 20: Verify IsColdDayPromoActive returns true when temp is 10.0°C at 2 PM */
    [Fact]
    public void IsColdDayPromoActive_ReturnsTrueOnColdAfternoons()
    {
        // TODO: Full TDD Setup
        Assert.Fail("Test incomplete! Remove this line after writing the Arrange, Act, and Assert steps.");
    }
}