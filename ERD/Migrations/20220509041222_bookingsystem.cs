using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERD.Migrations
{
    public partial class bookingsystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityID = table.Column<int>(type: "int", nullable: true),
                    MaxLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Venues_Activitys_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activitys",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueID = table.Column<int>(type: "int", nullable: true),
                    ActivityID = table.Column<int>(type: "int", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    BookedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Activitys_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activitys",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Bookings_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Bookings_Venues_VenueID",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ActivityID",
                table: "Bookings",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EmployeeID",
                table: "Bookings",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VenueID",
                table: "Bookings",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Venues_ActivityID",
                table: "Venues",
                column: "ActivityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
