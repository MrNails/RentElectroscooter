using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class VehicleDataConfiguration : IEntityTypeConfiguration<VehicleData>
    {
        public void Configure(EntityTypeBuilder<VehicleData> builder)
        {
            builder.HasKey(x => x.Id)
                .IsClustered(false)
                .HasName("PK_NC_VehicleData_Id");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Created)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.ManufacturerName)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }
    }
}
