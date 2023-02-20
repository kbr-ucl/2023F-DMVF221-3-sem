using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfDemo.Api.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forfatter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forfatter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForfatterBog",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForfatterBog", x => new { x.ArticleId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_ForfatterBog_Bog_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Bog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ForfatterBog_Forfatter_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Forfatter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForfatterBog_AuthorId",
                table: "ForfatterBog",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForfatterBog");

            migrationBuilder.DropTable(
                name: "Bog");

            migrationBuilder.DropTable(
                name: "Forfatter");
        }
    }
}
