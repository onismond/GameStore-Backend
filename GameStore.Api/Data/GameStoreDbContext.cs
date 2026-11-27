using GameStore.Api.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public class GameStoreDbContext : IdentityDbContext<
        ApplicationUser,
        IdentityRole<Guid>,
        Guid>
    {
        public GameStoreDbContext(
            DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<Game> Games => Set<Game>();

        public DbSet<Platform> Platforms => Set<Platform>();

        public DbSet<Genre> Genres => Set<Genre>();

        public DbSet<GamePlatform> GamePlatforms => Set<GamePlatform>();

        public DbSet<GameGenre> GameGenres => Set<GameGenre>();

        public DbSet<Listing> Listings => Set<Listing>();

        public DbSet<ListingImage> ListingImages => Set<ListingImage>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(GameStoreDbContext).Assembly);
        }
    }
}
