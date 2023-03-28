using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class Mig_NewDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dargahPardakhts",
                columns: table => new
                {
                    DargahPardakht_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DargahPardakht_NameBank = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DargahPardakht_Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dargahPardakhts", x => x.DargahPardakht_ID);
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    Hotel_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Hotel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Jozeyat_Gheymat = table.Column<long>(type: "bigint", nullable: false),
                    ZamanShoroa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ZamanPayan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Faal = table.Column<bool>(type: "bit", nullable: false),
                    Tozihat = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotels", x => x.Hotel_ID);
                });

            migrationBuilder.CreateTable(
                name: "Jostejo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    NameHotel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GheymatYekShab = table.Column<long>(type: "bigint", nullable: false),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nameostan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stareh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jostejo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rezervHotels",
                columns: table => new
                {
                    Rezerv_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rezerv_IDHotel = table.Column<int>(type: "int", nullable: false),
                    Rezerv_Jensiat = table.Column<int>(type: "int", nullable: false),
                    Rezerv_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rezerv_NameKhanevadgi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rezerv_Codemeli = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rezerv_Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Rezerv_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rezerv_TeadadNafarat = table.Column<int>(type: "int", nullable: false),
                    Rezerv_TeadadOthagh = table.Column<int>(type: "int", nullable: false),
                    Rezerv_Vorod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rezerv_Khoroj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rezerv_Mablagh = table.Column<long>(type: "bigint", nullable: false),
                    Rezerv_Vazeyt = table.Column<bool>(type: "bit", nullable: false),
                    Rezerv_Roz = table.Column<int>(type: "int", nullable: false),
                    Rezerv_IP = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rezervHotels", x => x.Rezerv_ID);
                });

            migrationBuilder.CreateTable(
                name: "tanzimatEmails",
                columns: table => new
                {
                    Eamil_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eamil_EmailSend = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Eamil_Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Eamil_UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Eamil_Port = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Eamil_Smtp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tanzimatEmails", x => x.Eamil_ID);
                });

            migrationBuilder.CreateTable(
                name: "tedadStarehs",
                columns: table => new
                {
                    TedadStareh_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedadStareh_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tedadStarehs", x => x.TedadStareh_ID);
                });

            migrationBuilder.CreateTable(
                name: "tedadTakhtHotels",
                columns: table => new
                {
                    TedadTakh_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedadTakh_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tedadTakhtHotels", x => x.TedadTakh_ID);
                });

            migrationBuilder.CreateTable(
                name: "zarfyatOtaghHotels",
                columns: table => new
                {
                    ZarfyatOtagh_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZarfyatOtagh_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_zarfyatOtaghHotels", x => x.ZarfyatOtagh_ID);
                });

            migrationBuilder.CreateTable(
                name: "emkanatHotels",
                columns: table => new
                {
                    Emkanat_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emkanat_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Emkanat_Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Emkanat_IdHotel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emkanatHotels", x => x.Emkanat_ID);
                    table.ForeignKey(
                        name: "FK_emkanatHotels_hotels_Emkanat_IdHotel",
                        column: x => x.Emkanat_IdHotel,
                        principalTable: "hotels",
                        principalColumn: "Hotel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "nazarats",
                columns: table => new
                {
                    Nazarat_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazarat_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazarat_Matn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Nazarat_Zaman = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazarat_HotelID = table.Column<int>(type: "int", nullable: false),
                    Nazarat_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nazarat_Emtyaz = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nazarats", x => x.Nazarat_ID);
                    table.ForeignKey(
                        name: "FK_nazarats_hotels_Nazarat_HotelID",
                        column: x => x.Nazarat_HotelID,
                        principalTable: "hotels",
                        principalColumn: "Hotel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasavirHotels",
                columns: table => new
                {
                    Tasavir_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tasavir_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tasavir_IDHotel = table.Column<int>(type: "int", nullable: false),
                    Tasavir_Asli = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasavirHotels", x => x.Tasavir_ID);
                    table.ForeignKey(
                        name: "FK_tasavirHotels_hotels_Tasavir_IDHotel",
                        column: x => x.Tasavir_IDHotel,
                        principalTable: "hotels",
                        principalColumn: "Hotel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pardakhtHotels",
                columns: table => new
                {
                    Pardakh_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pardakh_IDHotel = table.Column<int>(type: "int", nullable: false),
                    Pardakh_ZamanVariz = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pardakh_Pigiry = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Pardakh_Mablagh = table.Column<long>(type: "bigint", nullable: false),
                    Pardakh_Vazeiat = table.Column<bool>(type: "bit", nullable: false),
                    Pardakh_Trakonesh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pardakh_Marjaa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pardakh_Rezerv = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pardakhtHotels", x => x.Pardakh_ID);
                    table.ForeignKey(
                        name: "FK_pardakhtHotels_hotels_Pardakh_IDHotel",
                        column: x => x.Pardakh_IDHotel,
                        principalTable: "hotels",
                        principalColumn: "Hotel_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pardakhtHotels_rezervHotels_Pardakh_Rezerv",
                        column: x => x.Pardakh_Rezerv,
                        principalTable: "rezervHotels",
                        principalColumn: "Rezerv_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "jozeyatHotels",
                columns: table => new
                {
                    Jozeyat_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jozeyat_HotelID = table.Column<int>(type: "int", nullable: false),
                    Jozeyat_ZarfiatOtaghID = table.Column<int>(type: "int", nullable: false),
                    Jozeyat_TedadTakhtID = table.Column<int>(type: "int", nullable: false),
                    Jozeyat_TedadStareID = table.Column<int>(type: "int", nullable: false),
                    Jozeyat_Teadad = table.Column<int>(type: "int", nullable: false),
                    ZarfyatOtaghHotel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jozeyatHotels", x => x.Jozeyat_ID);
                    table.ForeignKey(
                        name: "FK_jozeyatHotels_hotels_Jozeyat_HotelID",
                        column: x => x.Jozeyat_HotelID,
                        principalTable: "hotels",
                        principalColumn: "Hotel_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jozeyatHotels_tedadStarehs_Jozeyat_TedadStareID",
                        column: x => x.Jozeyat_TedadStareID,
                        principalTable: "tedadStarehs",
                        principalColumn: "TedadStareh_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jozeyatHotels_tedadTakhtHotels_Jozeyat_TedadTakhtID",
                        column: x => x.Jozeyat_TedadTakhtID,
                        principalTable: "tedadTakhtHotels",
                        principalColumn: "TedadTakh_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jozeyatHotels_zarfyatOtaghHotels_ZarfyatOtaghHotel",
                        column: x => x.ZarfyatOtaghHotel,
                        principalTable: "zarfyatOtaghHotels",
                        principalColumn: "ZarfyatOtagh_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_emkanatHotels_Emkanat_IdHotel",
                table: "emkanatHotels",
                column: "Emkanat_IdHotel");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_Jozeyat_HotelID",
                table: "jozeyatHotels",
                column: "Jozeyat_HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_Jozeyat_TedadStareID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadStareID");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_Jozeyat_TedadTakhtID",
                table: "jozeyatHotels",
                column: "Jozeyat_TedadTakhtID");

            migrationBuilder.CreateIndex(
                name: "IX_jozeyatHotels_ZarfyatOtaghHotel",
                table: "jozeyatHotels",
                column: "ZarfyatOtaghHotel");

            migrationBuilder.CreateIndex(
                name: "IX_nazarats_Nazarat_HotelID",
                table: "nazarats",
                column: "Nazarat_HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_pardakhtHotels_Pardakh_IDHotel",
                table: "pardakhtHotels",
                column: "Pardakh_IDHotel");

            migrationBuilder.CreateIndex(
                name: "IX_pardakhtHotels_Pardakh_Rezerv",
                table: "pardakhtHotels",
                column: "Pardakh_Rezerv");

            migrationBuilder.CreateIndex(
                name: "IX_tasavirHotels_Tasavir_IDHotel",
                table: "tasavirHotels",
                column: "Tasavir_IDHotel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dargahPardakhts");

            migrationBuilder.DropTable(
                name: "emkanatHotels");

            migrationBuilder.DropTable(
                name: "Jostejo");

            migrationBuilder.DropTable(
                name: "jozeyatHotels");

            migrationBuilder.DropTable(
                name: "nazarats");

            migrationBuilder.DropTable(
                name: "pardakhtHotels");

            migrationBuilder.DropTable(
                name: "tanzimatEmails");

            migrationBuilder.DropTable(
                name: "tasavirHotels");

            migrationBuilder.DropTable(
                name: "tedadStarehs");

            migrationBuilder.DropTable(
                name: "tedadTakhtHotels");

            migrationBuilder.DropTable(
                name: "zarfyatOtaghHotels");

            migrationBuilder.DropTable(
                name: "rezervHotels");

            migrationBuilder.DropTable(
                name: "hotels");
        }
    }
}
