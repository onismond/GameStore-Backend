using GameStore.Api.Models.Common;

namespace GameStore.Api.Models.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Developer { get; set; }

    public string? Publisher { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? CoverImageUrl { get; set; }

    public ICollection<GamePlatform> Platforms { get; set; }
        = new List<GamePlatform>();

    public ICollection<GameGenre> Genres { get; set; }
        = new List<GameGenre>();

    public ICollection<Listing> Listings { get; set; }
        = new List<Listing>();
}