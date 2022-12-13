using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class SpecialPropositionConfiguration : IEntityTypeConfiguration<SpecialProposition>
    {
        public void Configure(EntityTypeBuilder<SpecialProposition> builder)
        {
            builder.HasKey(x => new { x.SpecPropMetadataId, x.UserId })
                .IsClustered(true)
                .HasName("PK_CL_SpecialProposition_SpecPropMetadataIdUserId");

            builder.Property(x => x.Created)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.SpecialPropositionMetadata);
        }
    }
}
