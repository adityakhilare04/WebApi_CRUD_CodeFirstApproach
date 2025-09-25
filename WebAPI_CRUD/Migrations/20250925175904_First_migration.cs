using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class First_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("02c3be5b-7d30-4113-bce3-a8e5693ca858"), "HR" },
                    { new Guid("5e6c90db-0b2c-427f-b7b6-2b93298dd307"), "IT" },
                    { new Guid("a8507b28-e2b7-457a-bb8b-ec15f1871d79"), "Finance" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "DateOfBirth", "DepartmentId", "Email", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { new Guid("308ce876-7545-45fd-af67-a5856d681b16"), new DateOnly(1998, 2, 23), new Guid("02c3be5b-7d30-4113-bce3-a8e5693ca858"), "Sham.Patil@gmail.com", "Sham", 0, "Patil" },
                    { new Guid("674f6969-995f-49bd-b2b3-60156681e181"), new DateOnly(1990, 10, 10), new Guid("5e6c90db-0b2c-427f-b7b6-2b93298dd307"), "Lata.Mangeshkar@gmail.com", "Lata", 1, "Mangeshkar" },
                    { new Guid("e851ec9f-5bc9-4304-9654-9dde451201a4"), new DateOnly(1994, 12, 2), new Guid("5e6c90db-0b2c-427f-b7b6-2b93298dd307"), "Ram.Kumar@gmail.com", "Ram", 0, "Kumar" },
                    { new Guid("fddf90ca-b08a-4f35-aced-338c37827f18"), new DateOnly(1999, 3, 17), new Guid("02c3be5b-7d30-4113-bce3-a8e5693ca858"), "Asha.Bhosale@gmail.com", "Asha", 1, "Bhosale" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
