using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookRoom.Infrastructure.Migrations
{
    public partial class DbCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    DatInc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DatAlt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    BornDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DatInc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DatAlt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_BookRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    DatInc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DatAlt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BookRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_BookRooms_TB_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "TB_Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_BookRooms_TB_User_UserId",
                        column: x => x.UserId,
                        principalTable: "TB_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_BookRooms_RoomId",
                table: "TB_BookRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_BookRooms_UserId",
                table: "TB_BookRooms",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_BookRooms");

            migrationBuilder.DropTable(
                name: "TB_Room");

            migrationBuilder.DropTable(
                name: "TB_User");
        }
    }
}
