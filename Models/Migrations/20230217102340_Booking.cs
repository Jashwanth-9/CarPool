using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class Booking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fromLoation",
                table: "BookedRides",
                newName: "fromLocation");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookedRides",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedRides",
                table: "BookedRides",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedRides",
                table: "BookedRides");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookedRides");

            migrationBuilder.RenameColumn(
                name: "fromLocation",
                table: "BookedRides",
                newName: "fromLoation");
        }
    }
}
