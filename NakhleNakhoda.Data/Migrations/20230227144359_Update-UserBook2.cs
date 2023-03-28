using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserBook2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "RoomPrice",
                table: "BookRoomCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomPrice",
                table: "BookRoomCategory");

        }
    }
}
