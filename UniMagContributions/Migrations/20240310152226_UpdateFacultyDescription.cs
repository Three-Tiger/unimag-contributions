using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class UpdateFacultyDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Faculties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("156670bd-1025-455a-9a7b-546239182540"), "Student" },
                    { new Guid("5460bf39-7f5f-4879-b7fe-c217874bd586"), "Manager" },
                    { new Guid("7a499324-ecbd-4109-891a-4bd42d9a845a"), "Administrator" },
                    { new Guid("ca558437-997e-47f5-8aa2-32c9b65b4c1a"), "Coordinator" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("156670bd-1025-455a-9a7b-546239182540"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("5460bf39-7f5f-4879-b7fe-c217874bd586"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("7a499324-ecbd-4109-891a-4bd42d9a845a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("ca558437-997e-47f5-8aa2-32c9b65b4c1a"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Faculties",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
