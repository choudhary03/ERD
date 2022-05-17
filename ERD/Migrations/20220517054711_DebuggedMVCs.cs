using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERD.Migrations
{
    public partial class DebuggedMVCs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxLimit",
                table: "Venues");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxLimit",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
