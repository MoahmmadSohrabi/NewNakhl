using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoomServiceName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomService");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "RoomCategory",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "RoomFacility",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomFacility", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomFacility");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RoomCategory",
                newName: "Title");

            migrationBuilder.CreateTable(
                name: "RoomService",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomService", x => x.Id);
                });
        }
    }
}
