using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class AddNewGuestRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { new Guid("636e7b42-1831-44cb-8e6d-fa90b6076edb"), "Guest" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 3, 19, 10, 8, 35, 28, DateTimeKind.Local).AddTicks(3284), "AQAAAAEAACcQAAAAEMcFyj7ejNEW02Egk90TSGU7srNzOQcyEBtshGPWAyJIWsdxuxWqNenSioFekVUtmg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("636e7b42-1831-44cb-8e6d-fa90b6076edb"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 3, 10, 23, 45, 15, 816, DateTimeKind.Local).AddTicks(9852), "AQAAAAEAACcQAAAAEL12XjVZj2eTXgU0FZLNfJWo2JvvpPAcLhRn0lixXNub8ZQAFokPxGuAgU5cdNk5mA==" });
        }
    }
}
