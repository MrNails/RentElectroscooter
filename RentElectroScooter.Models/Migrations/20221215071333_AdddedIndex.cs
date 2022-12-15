using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentElectroScooter.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AdddedIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NC_ElectroScooter_UserId",
                table: "ElectroScooters",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_NC_ElectroScooter_UserId",
                table: "ElectroScooters");
        }
    }
}
