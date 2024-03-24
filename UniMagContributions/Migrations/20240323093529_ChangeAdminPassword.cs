using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class ChangeAdminPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("04df80e1-fd59-4121-af73-e5356a2abd2e"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2020, 5, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 30, 23, 59, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("3801a15f-18d4-40c3-8a70-28c8a35ca010"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2021, 5, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 30, 23, 59, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("4f1e0068-0c6a-4a45-8f8c-92fc7f4e58b5"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 30, 23, 59, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("81495466-6fdb-45e6-bfd2-1995597d7df9"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2019, 5, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 30, 23, 59, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2022, 5, 1, 23, 59, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 23, 59, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2002, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAEAACcQAAAAEK4PPf2K6G4NrkLQFQr86rHO+JjGjxsqP5wiioMA0OnLMPiRUjsK9NiwGj3L5xi2mg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("04df80e1-fd59-4121-af73-e5356a2abd2e"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2020, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2020, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("3801a15f-18d4-40c3-8a70-28c8a35ca010"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2021, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2021, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("4f1e0068-0c6a-4a45-8f8c-92fc7f4e58b5"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2023, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2023, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("81495466-6fdb-45e6-bfd2-1995597d7df9"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2019, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"),
                columns: new[] { "ClosureDate", "FinalClosureDate" },
                values: new object[] { new DateTime(2022, 5, 1, 23, 59, 59, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 30, 23, 59, 59, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"),
                columns: new[] { "DateOfBirth", "Password" },
                values: new object[] { new DateTime(2024, 3, 23, 16, 30, 26, 619, DateTimeKind.Local).AddTicks(8528), "AQAAAAEAACcQAAAAENtQ7u4ISjxoA3svZNiNcpiJc0f+a1/7419FnPHOghPZ8ZpYJi+CEW/DB6ZRIJ3bDw==" });
        }
    }
}
