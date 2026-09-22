namespace TruckingGoodIcecream.Core.Models.Entities;

using System;
using LinqToDB.Mapping;

[Table(Name = "Flavors")]
public class FlavorEntity
{
    [PrimaryKey, Identity] public int Id { get; set; }
    [Column, NotNull] public string Name { get; set; } = string.Empty;
    [Column] public decimal BasePrice { get; set; }
    [Column] public int ScoopsInStock { get; set; }
    [Column] public bool IsActive { get; set; } = true;
    [Column] public DateTime AddedAtUtc { get; set; }
}