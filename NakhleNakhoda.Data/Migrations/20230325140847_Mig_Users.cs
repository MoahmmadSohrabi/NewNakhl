using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class Mig_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookFacility_RoomFacility_RoomFacilityId",
                table: "BookFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_BookFacility_UserBook_UserBookId",
                table: "BookFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_RoomCategory_RoomCategoryId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_Room_RoomId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_UserBook_UserBookId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_emkanatHotels_hotels_Emkanat_IdHotel",
                table: "emkanatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_hotels_Jozeyat_HotelID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_tedadStarehs_Jozeyat_TedadStareID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_tedadTakhtHotels_Jozeyat_TedadTakhtID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_nazarats_hotels_Nazarat_HotelID",
                table: "nazarats");

            migrationBuilder.DropForeignKey(
                name: "FK_pardakhtHotels_hotels_Pardakh_IDHotel",
                table: "pardakhtHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_PictureBinary_Picture_PictureId",
                table: "PictureBinary");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_Role_RoleId",
                table: "RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_tasavirHotels_hotels_Tasavir_IDHotel",
                table: "tasavirHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Payment_PaymentId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_User_MemberId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_UserId",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_User_UserId",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_User_UserId",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaim",
                table: "UserClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "UserToken",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogin",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaim",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogin_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaim_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaim_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "AspNetUserTokens",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoleId1",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "AspNetUserLogins",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "AspNetUserClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoleId1",
                table: "AspNetRoleClaims",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permission_Permission_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Permission",
                        principalColumn: "PermissionId");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ActiveCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserAvatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UR_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId1",
                table: "AspNetUserLogins",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_ParentID",
                table: "Permission",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                table: "AspNetUserClaims",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId1",
                table: "AspNetUserLogins",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId1",
                table: "AspNetUserTokens",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFacility_RoomFacility_RoomFacilityId",
                table: "BookFacility",
                column: "RoomFacilityId",
                principalTable: "RoomFacility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookFacility_UserBook_UserBookId",
                table: "BookFacility",
                column: "UserBookId",
                principalTable: "UserBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_RoomCategory_RoomCategoryId",
                table: "BookRoomCategory",
                column: "RoomCategoryId",
                principalTable: "RoomCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_Room_RoomId",
                table: "BookRoomCategory",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_UserBook_UserBookId",
                table: "BookRoomCategory",
                column: "UserBookId",
                principalTable: "UserBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_emkanatHotels_hotels_Emkanat_IdHotel",
                table: "emkanatHotels",
                column: "Emkanat_IdHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_hotels_Jozeyat_HotelID",
                table: "jozeyatHotels",
                column: "Jozeyat_HotelID",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_tedadStarehs_Jozeyat_TedadStareID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadStareID",
                principalTable: "tedadStarehs",
                principalColumn: "TedadStareh_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_tedadTakhtHotels_Jozeyat_TedadTakhtID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadTakhtID",
                principalTable: "tedadTakhtHotels",
                principalColumn: "TedadTakh_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels",
                column: "Jozeyat_ZarfiatOtaghID",
                principalTable: "zarfyatOtaghHotels",
                principalColumn: "ZarfyatOtagh_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_nazarats_hotels_Nazarat_HotelID",
                table: "nazarats",
                column: "Nazarat_HotelID",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_pardakhtHotels_hotels_Pardakh_IDHotel",
                table: "pardakhtHotels",
                column: "Pardakh_IDHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PictureBinary_Picture_PictureId",
                table: "PictureBinary",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tasavirHotels_hotels_Tasavir_IDHotel",
                table: "tasavirHotels",
                column: "Tasavir_IDHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_MemberId",
                table: "UserBook",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Payment_PaymentId",
                table: "UserBook",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId1",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_BookFacility_RoomFacility_RoomFacilityId",
                table: "BookFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_BookFacility_UserBook_UserBookId",
                table: "BookFacility");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_RoomCategory_RoomCategoryId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_Room_RoomId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRoomCategory_UserBook_UserBookId",
                table: "BookRoomCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_emkanatHotels_hotels_Emkanat_IdHotel",
                table: "emkanatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_hotels_Jozeyat_HotelID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_tedadStarehs_Jozeyat_TedadStareID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_tedadTakhtHotels_Jozeyat_TedadTakhtID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_nazarats_hotels_Nazarat_HotelID",
                table: "nazarats");

            migrationBuilder.DropForeignKey(
                name: "FK_pardakhtHotels_hotels_Pardakh_IDHotel",
                table: "pardakhtHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_PictureBinary_Picture_PictureId",
                table: "PictureBinary");

            migrationBuilder.DropForeignKey(
                name: "FK_tasavirHotels_hotels_Tasavir_IDHotel",
                table: "tasavirHotels");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_MemberId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Payment_PaymentId",
                table: "UserBook");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserTokens_UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserLogins_UserId1",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoleClaims_RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserLogins");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UserToken");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UserLogin");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UserClaim");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RoleClaim");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "UserLogin",
                newName: "IX_UserLogin_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "UserClaim",
                newName: "IX_UserClaim_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "RoleClaim",
                newName: "IX_RoleClaim_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToken",
                table: "UserToken",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogin",
                table: "UserLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaim",
                table: "UserClaim",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookFacility_RoomFacility_RoomFacilityId",
                table: "BookFacility",
                column: "RoomFacilityId",
                principalTable: "RoomFacility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookFacility_UserBook_UserBookId",
                table: "BookFacility",
                column: "UserBookId",
                principalTable: "UserBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_RoomCategory_RoomCategoryId",
                table: "BookRoomCategory",
                column: "RoomCategoryId",
                principalTable: "RoomCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_Room_RoomId",
                table: "BookRoomCategory",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRoomCategory_UserBook_UserBookId",
                table: "BookRoomCategory",
                column: "UserBookId",
                principalTable: "UserBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_emkanatHotels_hotels_Emkanat_IdHotel",
                table: "emkanatHotels",
                column: "Emkanat_IdHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_hotels_Jozeyat_HotelID",
                table: "jozeyatHotels",
                column: "Jozeyat_HotelID",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_tedadStarehs_Jozeyat_TedadStareID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadStareID",
                principalTable: "tedadStarehs",
                principalColumn: "TedadStareh_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_tedadTakhtHotels_Jozeyat_TedadTakhtID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadTakhtID",
                principalTable: "tedadTakhtHotels",
                principalColumn: "TedadTakh_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_jozeyatHotels_zarfyatOtaghHotels_Jozeyat_ZarfiatOtaghID",
                table: "jozeyatHotels",
                column: "Jozeyat_ZarfiatOtaghID",
                principalTable: "zarfyatOtaghHotels",
                principalColumn: "ZarfyatOtagh_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_nazarats_hotels_Nazarat_HotelID",
                table: "nazarats",
                column: "Nazarat_HotelID",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pardakhtHotels_hotels_Pardakh_IDHotel",
                table: "pardakhtHotels",
                column: "Pardakh_IDHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PictureBinary_Picture_PictureId",
                table: "PictureBinary",
                column: "PictureId",
                principalTable: "Picture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_Role_RoleId",
                table: "RoleClaim",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tasavirHotels_hotels_Tasavir_IDHotel",
                table: "tasavirHotels",
                column: "Tasavir_IDHotel",
                principalTable: "hotels",
                principalColumn: "Hotel_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Payment_PaymentId",
                table: "UserBook",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_User_MemberId",
                table: "UserBook",
                column: "MemberId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_UserId",
                table: "UserClaim",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_User_UserId",
                table: "UserLogin",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                table: "UserRole",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_User_UserId",
                table: "UserToken",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
