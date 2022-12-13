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
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => new { x.SubscriptionMetadataId, x.UserId })
                .IsClustered(true)
                .HasName("PK_CL_Subscription_SubsMetadataIdUserId");

            builder.Property(x => x.Created)
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.SubscriptionMetadata);
        }
    }
}
