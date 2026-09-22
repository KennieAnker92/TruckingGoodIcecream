namespace TruckingGoodIcecream.Core.Models.Dtos;

using System;

/*
 * WHAT IS A DTO (Data Transfer Object)?
 * - DTOs carry data into and out of application service boundaries.
 * - Unlike Entities (which map directly to SQLite tables), DTOs contain ONLY 
 *   the data required for a specific request, response, or receipt.
 * - Using C# 'record' types gives us immutable, lightweight data structures out of the box.
 */

// Used when creating a new flavor menu item (input into AddNewFlavor)
public record CreateFlavorDto(
    string Name, 
    decimal BasePrice, 
    int InitialScoops
);

// Used when querying flavor information (output returned to callers)
public record FlavorSummaryDto(
    int Id, 
    string Name, 
    decimal BasePrice, 
    int ScoopsInStock, 
    DateTime AddedAtUtc
);

// Used as a customer receipt after purchasing ice cream scoops
public record IcecreamReceiptDto(
    int FlavorId, 
    string FlavorName, 
    int ScoopsPurchased, 
    decimal TotalCost, 
    DateTime PurchasedAtUtc
);