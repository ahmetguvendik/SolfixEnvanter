using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_12082025 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CabinetId",
                table: "Assets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cabinets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UHeight = table.Column<int>(type: "int", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerFeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxLoadKg = table.Column<int>(type: "int", nullable: true),
                    MaxPowerWatts = table.Column<int>(type: "int", nullable: true),
                    CoolingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cabinets_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_CabinetId",
                table: "Assets",
                column: "CabinetId");

            migrationBuilder.CreateIndex(
                name: "IX_Cabinets_LocationId",
                table: "Cabinets",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Cabinets_CabinetId",
                table: "Assets",
                column: "CabinetId",
                principalTable: "Cabinets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Cabinets_CabinetId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "Cabinets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_CabinetId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "CabinetId",
                table: "Assets");
        }
    }
}
