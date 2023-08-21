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
                name: "SavedChats",
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
                    table.PrimaryKey("PK_SavedChats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagQuery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Query = table.Column<string>(type: "TEXT", nullable: false),
                    SavedChatId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagQuery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagQuery_SavedChats_SavedChatId",
                        column: x => x.SavedChatId,
                        principalTable: "SavedChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagQuery_SavedChatId",
                table: "TagQuery",
                column: "SavedChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagQuery");

            migrationBuilder.DropTable(
                name: "SavedChats");
        }
    }
}
