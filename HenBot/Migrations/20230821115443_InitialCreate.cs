using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HenBot.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Step = table.Column<int>(type: "INTEGER", nullable: false),
                    Page = table.Column<int>(type: "INTEGER", nullable: false),
                    Limit = table.Column<int>(type: "INTEGER", nullable: false),
                    IsConfiguring = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAyaya = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAyayaed = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastTag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagQuery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Query = table.Column<string>(type: "TEXT", nullable: false),
                    SavedUserId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagQuery_SavedUsers_SavedUserId",
                        column: x => x.SavedUserId,
                        principalTable: "SavedUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagQuery_SavedUserId",
                table: "TagQuery",
                column: "SavedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagQuery");

            migrationBuilder.DropTable(
                name: "SavedUsers");
        }
    }
}
