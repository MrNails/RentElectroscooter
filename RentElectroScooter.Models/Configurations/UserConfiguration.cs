using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentElectroScooter.CoreModels.Models;

namespace RentElectroScooter.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .IsClustered()
                .HasName("PK_CL_User_Id");

            builder.Property(x => x.Modified)
                .ValueGeneratedOnUpdate();

            builder.Property(x => x.Login)
                .HasColumnType("nvarchar")
                .HasMaxLength(20);

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar")
                .HasMaxLength(32);

            builder.Property(x => x.Salt)
                .HasColumnType("nvarchar")
                .HasMaxLength(32);

            builder.HasIndex(x => x.Login)
                .IncludeProperties(x => x.Password)
                .HasDatabaseName("IX_NC_User_Login");

            builder.HasOne(x => x.UserProfile);
        }
    }
}
