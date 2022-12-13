using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class SpecialPropositionMetadataConfiguration : IEntityTypeConfiguration<SpecialPropositionMetadata>
    {
        public void Configure(EntityTypeBuilder<SpecialPropositionMetadata> builder)
        {
            builder.HasKey(x => x.Id)
                .IsClustered()
                .HasName("IX_CL_SpecialPropositionMetadata_Id");

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

            builder.Property(x => x.ActivationRule)
                .HasColumnType("varchar")
                .HasMaxLength(255);
        }
    }
}
