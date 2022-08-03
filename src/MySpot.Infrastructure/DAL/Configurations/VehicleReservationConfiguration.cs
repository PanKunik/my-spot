using Microsoft.EntityFrameworkCore;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(x => x.EmployeeName)
            .HasConversion(x => x.Value, x => new EmployeeName(x))
            .IsRequired();

        builder.Property(x => x.LicencePlate)
            .HasConversion(x => x.Value, x => new LicencePlate(x))
            .IsRequired();
    }
}