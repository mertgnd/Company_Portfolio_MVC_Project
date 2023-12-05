using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mid_Add_SliderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SliderLogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PopUpTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopUpSubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopUpDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrlLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlSlider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlPopUp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SliderLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SliderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SliderLogoId = table.Column<int>(type: "int", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SliderItem");

            migrationBuilder.DropTable(
                name: "SliderLogos");
        }
    }
}
