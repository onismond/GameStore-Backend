using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data
{
    public class GameStoreDbContext : DbContext
    {
        public GameStoreDbContext(
            DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(GameStoreDbContext).Assembly);
        }
    }
}
