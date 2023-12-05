using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig_remove_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "SliderDescription",
                table: "SliderLogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SliderTitle",
                table: "SliderLogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderDescription",
                table: "SliderLogos");

            migrationBuilder.DropColumn(
                name: "SliderTitle",
                table: "SliderLogos");

            migrationBuilder.CreateTable(
                name: "SliderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderLogoId = table.Column<int>(type: "int", nullable: true),
                    SliderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SliderItem_SliderLogos_SliderLogoId",
                        column: x => x.SliderLogoId,
                        principalTable: "SliderLogos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SliderItem_SliderLogoId",
                table: "SliderItem",
                column: "SliderLogoId");
        }
    }
}
