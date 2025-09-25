using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class Initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("a8507b28-e2b7-457a-bb8b-ec15f1871d79"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("308ce876-7545-45fd-af67-a5856d681b16"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("674f6969-995f-49bd-b2b3-60156681e181"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("e851ec9f-5bc9-4304-9654-9dde451201a4"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("fddf90ca-b08a-4f35-aced-338c37827f18"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("02c3be5b-7d30-4113-bce3-a8e5693ca858"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("5e6c90db-0b2c-427f-b7b6-2b93298dd307"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
