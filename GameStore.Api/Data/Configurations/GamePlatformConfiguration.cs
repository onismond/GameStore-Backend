using GameStore.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class GamePlatformConfiguration
    : IEntityTypeConfiguration<GamePlatform>
{
    public void Configure(EntityTypeBuilder<GamePlatform> builder)
    {
        builder.ToTable("GamePlatforms");

        builder.HasKey(x => new { x.GameId, x.PlatformId });

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Platforms)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Platform)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.PlatformId);
    }
}