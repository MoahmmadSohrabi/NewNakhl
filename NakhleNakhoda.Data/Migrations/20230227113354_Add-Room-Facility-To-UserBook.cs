using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomFacilityToUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserBookId",
                table: "RoomFacility",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomFacility_UserBookId",
                table: "RoomFacility",
                column: "UserBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacility_UserBook_UserBookId",
                table: "RoomFacility",
                column: "UserBookId",
                principalTable: "UserBook",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacility_UserBook_UserBookId",
                table: "RoomFacility");

            migrationBuilder.DropIndex(
                name: "IX_RoomFacility_UserBookId",
                table: "RoomFacility");

            migrationBuilder.DropColumn(
                name: "UserBookId",
                table: "RoomFacility");
        }
    }
}
