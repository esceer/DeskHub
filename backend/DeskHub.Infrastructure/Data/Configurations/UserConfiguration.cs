using DeskHub.Domain.Entities;
using DeskHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeskHub.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        #region Properties

        entity.HasKey(u => u.Id);

        entity.Property(u => u.Name)
              .IsRequired()
              .HasMaxLength(100);

        entity.Property(u => u.Email)
              .HasMaxLength(200);

        #endregion

        #region Value Objects

        entity.Property(u => u.Role)
              .HasConversion<string>();

        #endregion

        #region Indexes

        entity.HasIndex(u => u.Email)
              .IsUnique()
              .HasFilter("[Email] IS NOT NULL");

        #endregion

        #region Data

        entity.HasData(
            new User
            {
                Id = Guid.Parse("0cb5b3cf-4fb1-423a-bfa6-04176c4e8465"),
                Name = "Admin",
                Email = "admin@deskhub.com",
                Role = Role.Admin
            }
        );

        #endregion
    }
}
