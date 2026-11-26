using GameStore.Api.Models.Common;

namespace GameStore.Api.Models.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<GameGenre> Games { get; set; }
        = new List<GameGenre>();
}