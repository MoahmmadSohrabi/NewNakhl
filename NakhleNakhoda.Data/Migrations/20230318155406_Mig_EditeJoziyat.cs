using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class Mig_EditeJoziyat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_ZarfyatOtaghHotel",
                table: "jozeyatHotels");

            migrationBuilder.DropIndex(
                name: "IX_jozeyatHotels_ZarfyatOtaghHotel",
                table: "jozeyatHotels");

            migrationBuilder.DropColumn(
                name: "ZarfyatOtaghHotel",
                table: "jozeyatHotels");

            migrationBuilder.DropColumn(
                name: "Nameostan",
                table: "Jostejo");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels",
                column: "Jozeyat_ZarfiatOtaghID");

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels",
                column: "Jozeyat_ZarfiatOtaghID",
                principalTable: "zarfyatOtaghHotels",
                principalColumn: "ZarfyatOtagh_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels");

            migrationBuilder.DropIndex(
                name: "IX_jozeyatHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels");

            migrationBuilder.AddColumn<int>(
                name: "ZarfyatOtaghHotel",
                table: "jozeyatHotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nameostan",
                table: "Jostejo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_ZarfyatOtaghHotel",
                table: "jozeyatHotels",
                column: "ZarfyatOtaghHotel");

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_ZarfyatOtaghHotel",
                table: "jozeyatHotels",
                column: "ZarfyatOtaghHotel",
                principalTable: "zarfyatOtaghHotels",
                principalColumn: "ZarfyatOtagh_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
