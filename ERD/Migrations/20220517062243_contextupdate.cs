using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERD.Migrations
{
    public partial class contextupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Venues_ActivityID",
                table: "Venues");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MatchFix",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_ActivityID",
                table: "Venues",
                column: "ActivityID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name_ActivityID",
                table: "Teams",
                columns: new[] { "Name", "ActivityID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BookedOn_VenueID_ActivityID_MatchFix",
                table: "Bookings",
                columns: new[] { "BookedOn", "VenueID", "ActivityID", "MatchFix" },
                unique: true,
                filter: "[MatchFix] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Venues_ActivityID",
                table: "Venues");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Name_ActivityID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_BookedOn_VenueID_ActivityID_MatchFix",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MatchFix",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Venues_ActivityID",
                table: "Venues",
                column: "ActivityID");
        }
    }
}
