using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class AddBookMiddleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReserve");

            migrationBuilder.CreateTable(
                name: "UserBook",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QtyAdult = table.Column<int>(type: "int", nullable: false),
                    QtyChild = table.Column<int>(type: "int", nullable: false),
                    QtyBaby = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBook_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBook_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookRoomCategory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    UserBookId = table.Column<long>(type: "bigint", nullable: false),
                    BookDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRoomCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookRoomCategory_RoomCategory_RoomCategoryId",
                        column: x => x.RoomCategoryId,
                        principalTable: "RoomCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRoomCategory_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookRoomCategory_UserBook_UserBookId",
                        column: x => x.UserBookId,
                        principalTable: "UserBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomCategory_RoomCategoryId",
                table: "BookRoomCategory",
                column: "RoomCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomCategory_RoomId",
                table: "BookRoomCategory",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRoomCategory_UserBookId",
                table: "BookRoomCategory",
                column: "UserBookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_MemberId",
                table: "UserBook",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_PaymentId",
                table: "UserBook",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRoomCategory");

            migrationBuilder.DropTable(
                name: "UserBook");

            migrationBuilder.CreateTable(
                name: "UserReserve",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    QtyAdult = table.Column<int>(type: "int", nullable: false),
                    QtyBaby = table.Column<int>(type: "int", nullable: false),
                    QtyChild = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReserve", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReserve_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReserve_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReserve_MemberId",
                table: "UserReserve",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReserve_PaymentId",
                table: "UserReserve",
                column: "PaymentId");
        }
    }
}
