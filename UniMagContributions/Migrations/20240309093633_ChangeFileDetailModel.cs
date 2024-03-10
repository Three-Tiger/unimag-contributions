using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class ChangeFileDetailModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("111422e5-588a-49a5-a868-b877afb21cd0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("562d51af-33c7-4575-85b9-32be2e2f99ca"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("809b08ec-295e-49e6-874b-468a0f2ecc24"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("9d614c5e-4437-4ddb-a3e2-ac9be01b2dfd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("c2ebbb18-4c62-4b01-bddb-7e77d95f7400"));

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "FileDetails");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "FileDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("19297c6f-9e38-4fc1-a975-9ad1c1c6114c"), "Student" },
                    { new Guid("4a3eaf7d-713f-4ef5-94a0-0cabbb525846"), "Manager" },
                    { new Guid("5f488335-15c7-4047-9923-da21aafb14a7"), "Administrator" },
                    { new Guid("6fd0e0e0-41cd-43c6-8aa6-20a709effd12"), "Coordinator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("19297c6f-9e38-4fc1-a975-9ad1c1c6114c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("4a3eaf7d-713f-4ef5-94a0-0cabbb525846"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("5f488335-15c7-4047-9923-da21aafb14a7"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("6fd0e0e0-41cd-43c6-8aa6-20a709effd12"));

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "FileDetails");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "FileDetails",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "AnnualMagazines",
                columns: new[] { "AnnualMagazineId", "AcademicYear", "ClosureDate", "Description", "FinalClosureDate", "Title" },
                values: new object[] { new Guid("111422e5-588a-49a5-a868-b877afb21cd0"), "2023-2024", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Academic Year Description", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sample" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("562d51af-33c7-4575-85b9-32be2e2f99ca"), "Student" },
                    { new Guid("809b08ec-295e-49e6-874b-468a0f2ecc24"), "Manager" },
                    { new Guid("9d614c5e-4437-4ddb-a3e2-ac9be01b2dfd"), "Administrator" },
                    { new Guid("c2ebbb18-4c62-4b01-bddb-7e77d95f7400"), "Coordinator" }
                });
        }
    }
}
