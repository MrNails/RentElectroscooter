using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class ElectroScooterConfiguration : IEntityTypeConfiguration<ElectroScooter>
    {
        public void Configure(EntityTypeBuilder<ElectroScooter> builder)
        {
            builder.HasKey(e => e.Id)
                .IsClustered(false)
                .HasName("PK_NC_ElectroScooter_Id");

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(500);

            builder.OwnsOne(x => x.Position);

            builder.HasOne(x => x.AdditionalData);

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .HasConversion(x => (int)x, x => (VehicleStatus)x);
        }
    }
}
