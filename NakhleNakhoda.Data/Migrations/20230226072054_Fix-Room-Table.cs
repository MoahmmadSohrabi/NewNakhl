using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class FixRoomTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomCategory_RoomCategoryId",
                table: "Room");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomCategory_RoomCategoryId",
                table: "Room",
                column: "RoomCategoryId",
                principalTable: "RoomCategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomCategory_RoomCategoryId",
                table: "Room");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomCategory_RoomCategoryId",
                table: "Room",
                column: "RoomCategoryId",
                principalTable: "RoomCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
