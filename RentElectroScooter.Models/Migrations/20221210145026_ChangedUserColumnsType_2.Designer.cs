// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentElectroScooter.DAL.Repositories;

#nullable disable

namespace RentElectroScooter.DAL.Migrations
{
    [DbContext(typeof(RentElectroscooterDBContext))]
    [Migration("20221210145026_ChangedUserColumnsType_2")]
    partial class ChangedUserColumnsType2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RentElectroScooter.DAL.Models.ElectroScooter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AdditionalDataId")
                        .HasColumnType("int");

                    b.Property<float>("BatteryCharge")
                        .HasColumnType("real");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK_NC_ElectroScooter_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.HasIndex("AdditionalDataId");

                    b.ToTable("ElectroScooters");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.SpecialProposition", b =>
                {
                    b.Property<int>("SpecPropMetadataId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("SpecialPropositionMetadataId")
                        .HasColumnType("int");

                    b.HasKey("SpecPropMetadataId", "UserId")
                        .HasName("PK_CL_SpecialProposition_SpecPropMetadataIdUserId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("SpecPropMetadataId", "UserId"));

                    b.HasIndex("SpecialPropositionMetadataId");

                    b.HasIndex("UserId");

                    b.ToTable("SpecialPropositions");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.SpecialPropositionMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivationRule")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar");

                    b.Property<TimeSpan>("AvailabilityTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id")
                        .HasName("IX_CL_SpecialPropositionMetadata_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("SpecialPropositionMetadatas");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.Subscription", b =>
                {
                    b.Property<int>("SubscriptionMetadataId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BeginAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FinishAt")
                        .HasColumnType("datetime2");

                    b.HasKey("SubscriptionMetadataId", "UserId")
                        .HasName("PK_CL_Subscription_SubsMetadataIdUserId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("SubscriptionMetadataId", "UserId"));

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.SubscriptionMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("AvailabilityTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("DailyAvailabilityTime")
                        .HasColumnType("time");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id")
                        .HasName("IX_CL_SubscriptionMetadata_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.ToTable("SubscriptionMetadatas");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar");

                    b.HasKey("Id")
                        .HasName("PK_CL_User_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"));

                    b.HasIndex("Login")
                        .HasDatabaseName("IX_NC_User_Login");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("Login"), new[] { "Password" });

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.UserProfile", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,3)");

                    b.Property<DateTime>("Modified")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar");

                    b.Property<DateTime>("RegistrationAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SubscriptionMetadataId")
                        .HasColumnType("int");

                    b.Property<Guid?>("SubscriptionUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("TodayDrivenDistance")
                        .HasColumnType("real");

                    b.Property<double>("TodayDrivenTime")
                        .HasColumnType("int");

                    b.Property<float>("TotalDrivenDistance")
                        .HasColumnType("real");

                    b.Property<double>("TotalDrivenTime")
                        .HasColumnType("int");

                    b.HasKey("UserId")
                        .HasName("PK_CL_UserProfile_UserId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("UserId"));

                    b.HasIndex("SubscriptionMetadataId", "SubscriptionUserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.VehicleData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("ManufacturerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<float>("MaxBatteryCharge")
                        .HasColumnType("real");

                    b.Property<float>("MaxLoadWeight")
                        .HasColumnType("real");

                    b.Property<float>("MaxSpeed")
                        .HasColumnType("real");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<double>("PricePerTime")
                        .HasColumnType("float");

                    b.Property<float>("Time")
                        .HasColumnType("real");

                    b.Property<int>("TimeUnits")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_NC_VehicleData_Id");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("Id"), false);

                    b.ToTable("VehicleDatas");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.ElectroScooter", b =>
                {
                    b.HasOne("RentElectroScooter.DAL.Models.VehicleData", "AdditionalData")
                        .WithMany()
                        .HasForeignKey("AdditionalDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("RentElectroScooter.DAL.Coordinate", "Position", b1 =>
                        {
                            b1.Property<Guid>("ElectroScooterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<float>("Latitude")
                                .HasColumnType("real");

                            b1.Property<float>("Longitude")
                                .HasColumnType("real");

                            b1.HasKey("ElectroScooterId");

                            b1.ToTable("ElectroScooters");

                            b1.WithOwner()
                                .HasForeignKey("ElectroScooterId");
                        });

                    b.Navigation("AdditionalData");

                    b.Navigation("Position")
                        .IsRequired();
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.SpecialProposition", b =>
                {
                    b.HasOne("RentElectroScooter.DAL.Models.SpecialPropositionMetadata", "SpecialPropositionMetadata")
                        .WithMany()
                        .HasForeignKey("SpecialPropositionMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentElectroScooter.DAL.Models.UserProfile", null)
                        .WithMany("SpecialPropositions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpecialPropositionMetadata");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.Subscription", b =>
                {
                    b.HasOne("RentElectroScooter.DAL.Models.SubscriptionMetadata", "SubscriptionMetadata")
                        .WithMany()
                        .HasForeignKey("SubscriptionMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubscriptionMetadata");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.UserProfile", b =>
                {
                    b.HasOne("RentElectroScooter.DAL.Models.User", null)
                        .WithOne("UserProfile")
                        .HasForeignKey("RentElectroScooter.DAL.Models.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentElectroScooter.DAL.Models.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionMetadataId", "SubscriptionUserId");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.User", b =>
                {
                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("RentElectroScooter.DAL.Models.UserProfile", b =>
                {
                    b.Navigation("SpecialPropositions");
                });
#pragma warning restore 612, 618
        }
    }
}
