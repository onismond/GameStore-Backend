using Microsoft.AspNetCore.Identity;

namespace GameStore.Api.Models.Entities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string DisplayName { get; set; } = string.Empty;

        public string? Bio { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public ICollection<Listing> Listings { get; set; } = new List<Listing>();
    }
}
