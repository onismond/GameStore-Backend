using GameStore.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class GameGenreConfiguration
    : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder.ToTable("GameGenres");

        builder.HasKey(x => new { x.GameId, x.GenreId });

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Genres)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Genre)
            .WithMany(x => x.Games)
            .HasForeignKey(x => x.GenreId);
    }
}