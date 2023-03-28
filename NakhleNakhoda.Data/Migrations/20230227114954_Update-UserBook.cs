using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "BookFacility",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserBookId = table.Column<long>(type: "bigint", nullable: false),
                    RoomFacilityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookFacility", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookFacility_RoomFacility_RoomFacilityId",
                        column: x => x.RoomFacilityId,
                        principalTable: "RoomFacility",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookFacility_UserBook_UserBookId",
                        column: x => x.UserBookId,
                        principalTable: "UserBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookFacility_RoomFacilityId",
                table: "BookFacility",
                column: "RoomFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_BookFacility_UserBookId",
                table: "BookFacility",
                column: "UserBookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookFacility");

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
    }
}
