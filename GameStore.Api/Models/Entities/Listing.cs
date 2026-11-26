using GameStore.Api.Models.Common;
using GameStore.Api.Models.Enums;

namespace GameStore.Api.Models.Entities;

public class Listing : BaseEntity
{
    public Guid SellerId { get; set; }

    public ApplicationUser Seller { get; set; } = null!;

    public Guid GameId { get; set; }

    public Game Game { get; set; } = null!;

    public ListingType Type { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string CurrencyCode { get; set; } = "GHS";

    public PhysicalCondition? PhysicalCondition { get; set; }

    public DigitalDeliveryMethod? DigitalDeliveryMethod { get; set; }

    public int QuantityAvailable { get; set; } = 1;

    public bool IsNegotiable { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Draft;

    public DateTime? ExpiresAtUtc { get; set; }

    public ICollection<ListingImage> Images { get; set; }
        = new List<ListingImage>();
}