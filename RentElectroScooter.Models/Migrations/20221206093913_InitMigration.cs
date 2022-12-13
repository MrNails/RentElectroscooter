using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentElectroScooter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialPropositionMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AvailabilityTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ActivationRule = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_CL_SpecialPropositionMetadata_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AvailabilityTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DailyAvailabilityTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IX_CL_SubscriptionMetadata_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_User_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "VehicleDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaxBatteryCharge = table.Column<float>(type: "real", nullable: false),
                    MaxLoadWeight = table.Column<float>(type: "real", nullable: false),
                    MaxSpeed = table.Column<float>(type: "real", nullable: false),
                    PricePerTime = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<float>(type: "real", nullable: false),
                    TimeUnits = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NC_VehicleData_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionMetadataId = table.Column<int>(type: "int", nullable: false),
                    BeginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Subscription_SubsMetadataIdUserId", x => new { x.SubscriptionMetadataId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionMetadatas_SubscriptionMetadataId",
                        column: x => x.SubscriptionMetadataId,
                        principalTable: "SubscriptionMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElectroScooters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionLatitude = table.Column<float>(name: "Position_Latitude", type: "real", nullable: false),
                    PositionLongitude = table.Column<float>(name: "Position_Longitude", type: "real", nullable: false),
                    BatteryCharge = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AdditionalDataId = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NC_ElectroScooter_Id", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_ElectroScooters_VehicleDatas_AdditionalDataId",
                        column: x => x.AdditionalDataId,
                        principalTable: "VehicleDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    TotalDrivenDistance = table.Column<float>(type: "real", nullable: false),
                    TodayDrivenDistance = table.Column<float>(type: "real", nullable: false),
                    TotalDrivenTime = table.Column<int>(type: "int", nullable: false),
                    TodayDrivenTime = table.Column<int>(type: "int", nullable: false),
                    RegistrationAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubscriptionMetadataId = table.Column<int>(type: "int", nullable: true),
                    SubscriptionUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_UserProfile_UserId", x => x.UserId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Subscriptions_SubscriptionMetadataId_SubscriptionUserId",
                        columns: x => new { x.SubscriptionMetadataId, x.SubscriptionUserId },
                        principalTable: "Subscriptions",
                        principalColumns: new[] { "SubscriptionMetadataId", "UserId" });
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialPropositions",
                columns: table => new
                {
                    SpecPropMetadataId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpecialPropositionMetadataId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_SpecialProposition_SpecPropMetadataIdUserId", x => new { x.SpecPropMetadataId, x.UserId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_SpecialPropositions_SpecialPropositionMetadatas_SpecialPropositionMetadataId",
                        column: x => x.SpecialPropositionMetadataId,
                        principalTable: "SpecialPropositionMetadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialPropositions_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectroScooters_AdditionalDataId",
                table: "ElectroScooters",
                column: "AdditionalDataId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialPropositions_SpecialPropositionMetadataId",
                table: "SpecialPropositions",
                column: "SpecialPropositionMetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialPropositions_UserId",
                table: "SpecialPropositions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SubscriptionMetadataId_SubscriptionUserId",
                table: "UserProfiles",
                columns: new[] { "SubscriptionMetadataId", "SubscriptionUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_NC_User_Login",
                table: "Users",
                column: "Login")
                .Annotation("SqlServer:Include", new[] { "Password" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectroScooters");

            migrationBuilder.DropTable(
                name: "SpecialPropositions");

            migrationBuilder.DropTable(
                name: "VehicleDatas");

            migrationBuilder.DropTable(
                name: "SpecialPropositionMetadatas");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubscriptionMetadatas");
        }
    }
}
