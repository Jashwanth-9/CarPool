using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.Migrations
{
    /// <inheritdoc />
    public partial class CarPool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                { 
                    rideId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    fromLocation = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    toLocation = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    date = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    inTime = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    outTime = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    stop = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    availableSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.rideId);
                });

            migrationBuilder.CreateTable(
                name: "UserRides",
                columns: table => new
                {
                    rideId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    inTime = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    outTime = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRides", x => new { x.rideId, x.userId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emailId = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    mobileNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "UserRides");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
