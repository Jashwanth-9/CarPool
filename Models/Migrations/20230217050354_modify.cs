using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookedRides",
                table: "BookedRides");

            migrationBuilder.AddColumn<string>(
                name: "fromLoation",
                table: "BookedRides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "toLocation",
                table: "BookedRides",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fromLoation",
                table: "BookedRides");

            migrationBuilder.DropColumn(
                name: "toLocation",
                table: "BookedRides");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookedRides",
                table: "BookedRides",
                columns: new[] { "rideId", "userId" });
        }
    }
}
