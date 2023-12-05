using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class modified_address_class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description4",
                table: "Addresses",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Description3",
                table: "Addresses",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Description2",
                table: "Addresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "Description1",
                table: "Addresses",
                newName: "Adress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Addresses",
                newName: "Description4");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Addresses",
                newName: "Description3");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Addresses",
                newName: "Description2");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Addresses",
                newName: "Description1");
        }
    }
}
