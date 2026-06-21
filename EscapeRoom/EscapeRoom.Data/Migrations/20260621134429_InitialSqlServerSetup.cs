using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscapeRoom.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialSqlServerSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DifficultyLevels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DifficultyLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EscapeRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifficultyLevelId = table.Column<int>(type: "int", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscapeRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscapeRooms_DifficultyLevels_DifficultyLevelId",
                        column: x => x.DifficultyLevelId,
                        principalTable: "DifficultyLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    EscapeRoomId = table.Column<int>(type: "int", nullable: false),
                    BookingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfParticipants = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_EscapeRooms_EscapeRoomId",
                        column: x => x.EscapeRoomId,
                        principalTable: "EscapeRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EscapeRoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hints_EscapeRooms_EscapeRoomId",
                        column: x => x.EscapeRoomId,
                        principalTable: "EscapeRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_bookings_datetime",
                table: "Bookings",
                column: "BookingDateTime");

            migrationBuilder.CreateIndex(
                name: "idx_bookings_player_id",
                table: "Bookings",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "idx_bookings_room_id",
                table: "Bookings",
                column: "EscapeRoomId");

            migrationBuilder.CreateIndex(
                name: "idx_rooms_difficulty_id",
                table: "EscapeRooms",
                column: "DifficultyLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Hints_EscapeRoomId",
                table: "Hints",
                column: "EscapeRoomId");

            migrationBuilder.CreateIndex(
                name: "idx_players_email",
                table: "Players",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Hints");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "EscapeRooms");

            migrationBuilder.DropTable(
                name: "DifficultyLevels");
        }
    }
}
