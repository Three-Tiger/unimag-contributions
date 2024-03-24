using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class RemoveUnusedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEBs5D1IG+j+h3Ceg2KvErJyMaBx5aU9VUar2eaK8O5jDDbTUyvSaTT+abSh/9/SZzA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContributionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_Contributions_ContributionId",
                        column: x => x.ContributionId,
                        principalTable: "Contributions",
                        principalColumn: "ContributionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEK4PPf2K6G4NrkLQFQr86rHO+JjGjxsqP5wiioMA0OnLMPiRUjsK9NiwGj3L5xi2mg==");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ContributionId",
                table: "Notifications",
                column: "ContributionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }
    }
}
