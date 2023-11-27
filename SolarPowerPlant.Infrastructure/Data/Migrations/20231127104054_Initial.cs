using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarPowerPlant.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(100)", nullable: true),
                    LastName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address_Address = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: true),
                    Password = table.Column<string>(type: "varchar(100)", nullable: true),
                    Salt = table.Column<string>(type: "varchar(100)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SolarPowerPlant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    InstalledPower = table.Column<int>(type: "int", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address_City = table.Column<string>(type: "varchar(50)", nullable: true),
                    Address_Address = table.Column<string>(type: "varchar(100)", nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(50)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(12,9)", precision: 12, scale: 9, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(12,9)", precision: 12, scale: 9, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolarPowerPlant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolarPowerPlant_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeseriesProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolarPowerPlantId = table.Column<int>(type: "int", nullable: false),
                    TimeSerieType = table.Column<int>(type: "int", nullable: false),
                    ProductionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Production = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeseriesProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeseriesProduction_SolarPowerPlant_SolarPowerPlantId",
                        column: x => x.SolarPowerPlantId,
                        principalTable: "SolarPowerPlant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolarPowerPlant_UserId",
                table: "SolarPowerPlant",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeseriesProduction_SolarPowerPlantId",
                table: "TimeseriesProduction",
                column: "SolarPowerPlantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeseriesProduction");

            migrationBuilder.DropTable(
                name: "SolarPowerPlant");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
