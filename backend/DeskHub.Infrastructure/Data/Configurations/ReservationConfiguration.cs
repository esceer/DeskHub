using DeskHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeskHub.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> entity)
    {
        #region Properties

        entity.HasKey(r => r.Id);

        entity.Property(r => r.Id)
              .ValueGeneratedOnAdd();

        entity.Property(r => r.Status)
              .HasConversion<string>()
              .IsRequired();

        #endregion

        #region Relationships

        entity.HasOne(r => r.User)
              .WithMany()
              .HasForeignKey(r => r.UserId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

        entity.HasOne(r => r.Desk)
              .WithMany()
              .HasForeignKey(r => r.DeskId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();

        #endregion

        #region Value Objects

        entity.OwnsOne(r => r.Time, time =>
        {
            time.Property(t => t.Start)
                .HasColumnName("Start")
                .IsRequired();

            time.Property(t => t.End)
                .HasColumnName("End")
                .IsRequired();
        });

        #endregion

        #region Indexes

        entity.HasIndex(r => r.UserId);
        entity.HasIndex(r => r.DeskId);

        entity.HasIndex(r => new { r.DeskId, r.Status });

        #endregion
    }
}