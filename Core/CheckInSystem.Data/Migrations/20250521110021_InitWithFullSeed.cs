using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CheckInSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitWithFullSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    PassportNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<string>(type: "TEXT", nullable: false),
                    IsOccupied = table.Column<bool>(type: "INTEGER", nullable: false),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK_Seats_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassengerId");
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "DepartureTime", "FlightNumber", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 15, 8, 0, 0, 0, DateTimeKind.Utc), "US123", "Scheduled" },
                    { 2, new DateTime(2025, 5, 15, 9, 0, 0, 0, DateTimeKind.Utc), "CN234", "Scheduled" },
                    { 3, new DateTime(2025, 5, 15, 10, 0, 0, 0, DateTimeKind.Utc), "FR345", "Scheduled" },
                    { 4, new DateTime(2025, 5, 15, 11, 0, 0, 0, DateTimeKind.Utc), "JP456", "Scheduled" },
                    { 5, new DateTime(2025, 5, 15, 12, 0, 0, 0, DateTimeKind.Utc), "DE567", "Scheduled" },
                    { 6, new DateTime(2025, 5, 15, 13, 0, 0, 0, DateTimeKind.Utc), "MN678", "Scheduled" }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "PassengerId", "FlightId", "FullName", "PassportNumber", "Status" },
                values: new object[,]
                {
                    { 1, 1, "Alice Johnson", "AA1234567", 0 },
                    { 2, 1, "Bob Smith", "BB2345678", 0 },
                    { 3, 1, "Charlie Lee", "CC3456789", 0 },
                    { 4, 1, "Diana King", "DD4567890", 0 },
                    { 5, 1, "Ethan Hall", "EE5678901", 0 },
                    { 6, 2, "Fiona White", "FF6789012", 0 },
                    { 7, 2, "George Brown", "GG7890123", 0 },
                    { 8, 2, "Hannah Scott", "HH8901234", 0 },
                    { 9, 2, "Ian Davis", "II9012345", 0 },
                    { 10, 2, "Jane Miller", "JJ0123456", 0 },
                    { 11, 3, "Kevin Wilson", "KK1234567", 0 },
                    { 12, 3, "Luna Thomas", "LL2345678", 0 },
                    { 13, 3, "Michael Lewis", "MM3456789", 0 },
                    { 14, 3, "Nora Harris", "NN4567890", 0 },
                    { 15, 3, "Oliver Young", "OO5678901", 0 },
                    { 16, 4, "Paula Green", "PP6789012", 0 },
                    { 17, 4, "Quentin Hill", "QQ7890123", 0 },
                    { 18, 4, "Rachel Adams", "RR8901234", 0 },
                    { 19, 4, "Samuel Baker", "SS9012345", 0 },
                    { 20, 4, "Tina Clark", "TT0123456", 0 },
                    { 21, 5, "Umar Turner", "UU1234567", 0 },
                    { 22, 5, "Violet Moore", "VV2345678", 0 },
                    { 23, 5, "William Wright", "WW3456789", 0 },
                    { 24, 5, "Xenia Lopez", "XX4567890", 0 },
                    { 25, 5, "Yusuf Walker", "YY5678901", 0 },
                    { 26, 6, "Zara Price", "ZZ6789012", 0 },
                    { 27, 6, "Aaron Murphy", "AB7890123", 0 },
                    { 28, 6, "Beatrice Wood", "CD8901234", 0 },
                    { 29, 6, "Caleb Reed", "EF9012345", 0 },
                    { 30, 6, "Dana Collins", "GH0123456", 0 }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "SeatId", "FlightId", "IsOccupied", "PassengerId", "SeatNumber" },
                values: new object[,]
                {
                    { 1, 1, false, null, "S01" },
                    { 2, 1, false, null, "S02" },
                    { 3, 1, false, null, "S03" },
                    { 4, 1, false, null, "S04" },
                    { 5, 1, false, null, "S05" },
                    { 6, 1, false, null, "S06" },
                    { 7, 1, false, null, "S07" },
                    { 8, 1, false, null, "S08" },
                    { 9, 1, false, null, "S09" },
                    { 10, 1, false, null, "S10" },
                    { 11, 1, false, null, "S11" },
                    { 12, 1, false, null, "S12" },
                    { 13, 1, false, null, "S13" },
                    { 14, 1, false, null, "S14" },
                    { 15, 1, false, null, "S15" },
                    { 16, 1, false, null, "S16" },
                    { 17, 1, false, null, "S17" },
                    { 18, 1, false, null, "S18" },
                    { 19, 1, false, null, "S19" },
                    { 20, 1, false, null, "S20" },
                    { 21, 2, false, null, "S01" },
                    { 22, 2, false, null, "S02" },
                    { 23, 2, false, null, "S03" },
                    { 24, 2, false, null, "S04" },
                    { 25, 2, false, null, "S05" },
                    { 26, 2, false, null, "S06" },
                    { 27, 2, false, null, "S07" },
                    { 28, 2, false, null, "S08" },
                    { 29, 2, false, null, "S09" },
                    { 30, 2, false, null, "S10" },
                    { 31, 2, false, null, "S11" },
                    { 32, 2, false, null, "S12" },
                    { 33, 2, false, null, "S13" },
                    { 34, 2, false, null, "S14" },
                    { 35, 2, false, null, "S15" },
                    { 36, 2, false, null, "S16" },
                    { 37, 2, false, null, "S17" },
                    { 38, 2, false, null, "S18" },
                    { 39, 2, false, null, "S19" },
                    { 40, 2, false, null, "S20" },
                    { 41, 3, false, null, "S01" },
                    { 42, 3, false, null, "S02" },
                    { 43, 3, false, null, "S03" },
                    { 44, 3, false, null, "S04" },
                    { 45, 3, false, null, "S05" },
                    { 46, 3, false, null, "S06" },
                    { 47, 3, false, null, "S07" },
                    { 48, 3, false, null, "S08" },
                    { 49, 3, false, null, "S09" },
                    { 50, 3, false, null, "S10" },
                    { 51, 3, false, null, "S11" },
                    { 52, 3, false, null, "S12" },
                    { 53, 3, false, null, "S13" },
                    { 54, 3, false, null, "S14" },
                    { 55, 3, false, null, "S15" },
                    { 56, 3, false, null, "S16" },
                    { 57, 3, false, null, "S17" },
                    { 58, 3, false, null, "S18" },
                    { 59, 3, false, null, "S19" },
                    { 60, 3, false, null, "S20" },
                    { 61, 4, false, null, "S01" },
                    { 62, 4, false, null, "S02" },
                    { 63, 4, false, null, "S03" },
                    { 64, 4, false, null, "S04" },
                    { 65, 4, false, null, "S05" },
                    { 66, 4, false, null, "S06" },
                    { 67, 4, false, null, "S07" },
                    { 68, 4, false, null, "S08" },
                    { 69, 4, false, null, "S09" },
                    { 70, 4, false, null, "S10" },
                    { 71, 4, false, null, "S11" },
                    { 72, 4, false, null, "S12" },
                    { 73, 4, false, null, "S13" },
                    { 74, 4, false, null, "S14" },
                    { 75, 4, false, null, "S15" },
                    { 76, 4, false, null, "S16" },
                    { 77, 4, false, null, "S17" },
                    { 78, 4, false, null, "S18" },
                    { 79, 4, false, null, "S19" },
                    { 80, 4, false, null, "S20" },
                    { 81, 5, false, null, "S01" },
                    { 82, 5, false, null, "S02" },
                    { 83, 5, false, null, "S03" },
                    { 84, 5, false, null, "S04" },
                    { 85, 5, false, null, "S05" },
                    { 86, 5, false, null, "S06" },
                    { 87, 5, false, null, "S07" },
                    { 88, 5, false, null, "S08" },
                    { 89, 5, false, null, "S09" },
                    { 90, 5, false, null, "S10" },
                    { 91, 5, false, null, "S11" },
                    { 92, 5, false, null, "S12" },
                    { 93, 5, false, null, "S13" },
                    { 94, 5, false, null, "S14" },
                    { 95, 5, false, null, "S15" },
                    { 96, 5, false, null, "S16" },
                    { 97, 5, false, null, "S17" },
                    { 98, 5, false, null, "S18" },
                    { 99, 5, false, null, "S19" },
                    { 100, 5, false, null, "S20" },
                    { 101, 6, false, null, "S01" },
                    { 102, 6, false, null, "S02" },
                    { 103, 6, false, null, "S03" },
                    { 104, 6, false, null, "S04" },
                    { 105, 6, false, null, "S05" },
                    { 106, 6, false, null, "S06" },
                    { 107, 6, false, null, "S07" },
                    { 108, 6, false, null, "S08" },
                    { 109, 6, false, null, "S09" },
                    { 110, 6, false, null, "S10" },
                    { 111, 6, false, null, "S11" },
                    { 112, 6, false, null, "S12" },
                    { 113, 6, false, null, "S13" },
                    { 114, 6, false, null, "S14" },
                    { 115, 6, false, null, "S15" },
                    { 116, 6, false, null, "S16" },
                    { 117, 6, false, null, "S17" },
                    { 118, 6, false, null, "S18" },
                    { 119, 6, false, null, "S19" },
                    { 120, 6, false, null, "S20" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_FlightId",
                table: "Seats",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_PassengerId",
                table: "Seats",
                column: "PassengerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");
        }
    }
}
