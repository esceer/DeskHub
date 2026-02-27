using DeskHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeskHub.Infrastructure.Data.Configurations;

public class DeskConfiguration : IEntityTypeConfiguration<Desk>
{
    public void Configure(EntityTypeBuilder<Desk> entity)
    {
        #region Properties

        entity.HasKey(d => d.Id);

        entity.Property(d => d.Code)
              .IsRequired()
              .HasMaxLength(50);

        #endregion

        #region Indexes

        entity.HasIndex(d => d.Code)
              .IsUnique();

        #endregion

        #region Data

        entity.HasData(
            new Desk
            {
                Id = Guid.Parse("f4cc7e3c-97a6-490a-bd0d-679c1f951be9"),
                Code = "1A",
                IsActive = true
            },
            new Desk
            {
                Id = Guid.Parse("5f360979-6d13-4bde-b501-8b1c502a3036"),
                Code = "2B",
                IsActive = false
            }
        );

        #endregion
    }
}
