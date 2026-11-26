using GameStore.Api.Models.Common;

namespace GameStore.Api.Models.Entities;

public class ListingImage : BaseEntity
{
    public Guid ListingId { get; set; }

    public Listing Listing { get; set; } = null!;

    public string StorageKey { get; set; } = string.Empty;

    public string? AltText { get; set; }

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; }
}