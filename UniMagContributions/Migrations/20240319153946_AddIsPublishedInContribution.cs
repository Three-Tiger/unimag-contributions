using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class AddIsPublishedInContribution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Contributions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 3, 19, 22, 39, 45, 882, DateTimeKind.Local).AddTicks(7707), "AQAAAAEAACcQAAAAEImbTTDmoZ4qRqMwyN/AaNhqEKgDYeGGLi0xaf2RXm307rPh7340WJ2uH5NuZU9QAA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Contributions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 8, 35, 28, DateTimeKind.Local).AddTicks(3284), "AQAAAAEAACcQAAAAEMcFyj7ejNEW02Egk90TSGU7srNzOQcyEBtshGPWAyJIWsdxuxWqNenSioFekVUtmg==" });
        }
    }
}
