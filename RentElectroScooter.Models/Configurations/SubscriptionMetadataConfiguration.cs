using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentElectroScooter.DAL.Configurations
{
    internal class SubscriptionMetadataConfiguration : IEntityTypeConfiguration<SubscriptionMetadata>
    {
        public void Configure(EntityTypeBuilder<SubscriptionMetadata> builder)
        {
            builder.HasKey(x => x.Id)
                .IsClustered()
                .HasName("IX_CL_SubscriptionMetadata_Id");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Created)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(70);

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Discount)
                .HasColumnType("decimal(18,2)");
        }
    }
}
