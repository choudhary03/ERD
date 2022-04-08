using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERD.Migrations
{
    public partial class unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_EmployeeID",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EmployeeID_ActivityID",
                table: "Enrollments",
                columns: new[] { "EmployeeID", "ActivityID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Enrollments_EmployeeID_ActivityID",
                table: "Enrollments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EmployeeID",
                table: "Enrollments",
                column: "EmployeeID");
        }
    }
}
