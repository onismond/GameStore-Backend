using GameStore.Api.Models.Common;

namespace GameStore.Api.Models.Entities;

public class Platform : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Manufacturer { get; set; } = string.Empty;

    public ICollection<GamePlatform> Games { get; set; }
        = new List<GamePlatform>();
}