using GameStore.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class ListingConfiguration : IEntityTypeConfiguration<Listing>
{
    public void Configure(EntityTypeBuilder<Listing> builder)
    {
        builder.ToTable("Listings", table =>
        {
            table.HasCheckConstraint(
                "CK_Listings_Price",
                "\"Price\" >= 0");

            table.HasCheckConstraint(
                "CK_Listings_Quantity",
                "\"QuantityAvailable\" > 0");

            table.HasCheckConstraint(
                "CK_Listings_CurrencyCode",
                "char_length(\"CurrencyCode\") = 3");

            table.HasCheckConstraint(
                "CK_Listings_TypeDetails",
                """
                (
                    "Type" = 1
                    AND "PhysicalCondition" IS NOT NULL
                    AND "DigitalDeliveryMethod" IS NULL
                )
                OR
                (
                    "Type" = 2
                    AND "PhysicalCondition" IS NULL
                    AND "DigitalDeliveryMethod" IS NOT NULL
                )
                """);
        });

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(5000);

        builder.Property(x => x.Price)
            .HasPrecision(12, 2);

        builder.Property(x => x.CurrencyCode)
            .HasMaxLength(3)
            .IsRequired()
            .IsFixedLength();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasOne(x => x.Seller)
            .WithMany(x => x.Listings)
            .HasForeignKey(x => x.SellerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Listings)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.GameId, x.Status });

        builder.HasIndex(x => new { x.SellerId, x.CreatedAtUtc });

        builder.HasIndex(x => new { x.Status, x.CreatedAtUtc });
    }
}