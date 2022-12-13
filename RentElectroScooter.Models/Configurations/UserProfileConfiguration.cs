using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(x => x.UserId)
                .IsClustered()
                .HasName("PK_CL_UserProfile_UserId");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(50);

            builder.Property(x => x.Modified)
                .ValueGeneratedOnUpdate();

            builder.Property(x => x.TotalDrivenTime)
                .HasColumnType("int")
                .HasConversion(x => x.TotalSeconds, x => TimeSpan.FromSeconds(x));

            builder.Property(x => x.TodayDrivenTime)
                .HasColumnType("int")
                .HasConversion(x => x.TotalSeconds, x => TimeSpan.FromSeconds(x));

            builder.Property(x => x.Balance)
                .HasColumnType("decimal(18,3)");
        }
    }
}
