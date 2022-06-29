using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReservationId(x));

        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, x => new EmployeeName(x))
            .IsRequired();

        builder.Property(x => x.LicencePlate)
            .HasConversion(x => x.Value, x => new LicencePlate(x))
            .IsRequired();

        builder.Property(x => x.Date)
            .HasConversion(x => x.Value, x => new Date(x))
            .IsRequired();
    }
}