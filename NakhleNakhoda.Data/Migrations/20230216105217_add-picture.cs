using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NakhleNakhoda.Migrations
{
    /// <inheritdoc />
    public partial class AddPicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MimeType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    SeoFilename = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    AltAttribute = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    TitleAttribute = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    VirtualPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PictureBinary",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinaryData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PictureId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureBinary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PictureBinary_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PictureBinary_PictureId",
                table: "PictureBinary",
                column: "PictureId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PictureBinary");

            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
