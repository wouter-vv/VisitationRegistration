using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitorRegistration.Migrations
{
    public partial class ChangeLicencePlate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numberplate",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "LicencePlate",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicencePlate",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "Numberplate",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
