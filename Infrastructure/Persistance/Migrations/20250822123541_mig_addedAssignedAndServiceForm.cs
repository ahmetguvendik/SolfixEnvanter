using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_addedAssignedAndServiceForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentForms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentFormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentFormDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentFormFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceForms",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ServiceFormName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceFormDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceFormFilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentForms");

            migrationBuilder.DropTable(
                name: "ServiceForms");
        }
    }
}
