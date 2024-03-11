using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniMagContributions.Migrations
{
    public partial class AddSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AnnualMagazines",
                columns: new[] { "AnnualMagazineId", "AcademicYear", "ClosureDate", "Description", "FinalClosureDate", "Title" },
                values: new object[,]
                {
                    { new Guid("04df80e1-fd59-4121-af73-e5356a2abd2e"), "2019-2020", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Environmental pollution refers to the contamination of the natural environment by harmful substances, resulting in adverse effects on ecosystems, human health, and biodiversity. Pollution sources include industrial activities, transportation, agriculture, and improper waste disposal. Consequences of pollution include air and water quality degradation, soil contamination, climate change, and negative impacts on wildlife and human populations.", new DateTime(2020, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Environmental pollution and its consequences" },
                    { new Guid("3801a15f-18d4-40c3-8a70-28c8a35ca010"), "2020-2021", new DateTime(2021, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Social media platforms have profoundly influenced modern society by changing communication patterns, social interactions, and information dissemination. While they facilitate connectivity and access to diverse perspectives, social media also raise concerns about privacy, mental health, and the spread of misinformation. The addictive nature of social media usage can lead to decreased productivity and increased feelings of loneliness and depression among users.", new DateTime(2021, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "The impact of social media on society" },
                    { new Guid("4f1e0068-0c6a-4a45-8f8c-92fc7f4e58b5"), "2022-2023", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blockchain technology offers innovative solutions to enhance transparency, traceability, and efficiency in supply chain management. By creating a decentralized and immutable ledger of transactions, blockchain enables secure and verifiable recording of supply chain activities, including sourcing, production, transportation, and delivery. This technology can help mitigate risks such as counterfeiting, fraud, and supply chain disruptions while improving trust among stakeholders and ensuring ethical practices.", new DateTime(2023, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blockchain application in supply chain management" },
                    { new Guid("81495466-6fdb-45e6-bfd2-1995597d7df9"), "2018-2019", new DateTime(2019, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The use of mobile phones has various effects on individuals and society. While they provide convenience and connectivity, overuse can lead to negative impacts such as decreased attention spans, disrupted sleep patterns, and increased risk of accidents due to distracted driving. Additionally, the production and disposal of mobile phones contribute to environmental pollution through the extraction of raw materials, energy consumption during manufacturing, and electronic waste generation.", new DateTime(2019, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Effect of Using Mobile Phone" },
                    { new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), "2021-2022", new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The practice of physically disciplining children, such as spanking or corporal punishment, remains a contentious issue. While some argue that it can be an effective way to discipline and teach obedience, others advocate for non-violent forms of discipline that focus on positive reinforcement and communication. Research suggests that physical punishment can have negative long-term effects on children's mental health and behavior, leading to aggression, low self-esteem, and increased likelihood of engaging in violent behavior.", new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Should we beat children to educate them or not?" }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "FacultyId", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("2c0983c8-bded-48ee-a2fa-9389313152c8"), "Marketing management involves the planning, implementation, and control of marketing programs to achieve organizational goals. This faculty covers market research, consumer behavior, advertising, branding, digital marketing, and strategic marketing planning.", "Marketing Management" },
                    { new Guid("3cf4f7fa-fdf1-420a-9e78-21ffb518144f"), "This faculty focuses on the study and application of computing technology, encompassing areas such as computer systems, software development, networking, cybersecurity, database management, and more.", "Information Technology" },
                    { new Guid("a852f27c-f94d-48a0-8c94-d0467afc3b12"), "International business focuses on the study of business activities that cross national borders. This faculty covers topics such as global trade, international finance, cultural differences, market entry strategies, global supply chain management, and international marketing.", "International Business" },
                    { new Guid("b338eded-0c32-421c-80c9-5aef828be112"), "Business administration involves the management of operations and resources within an organization to achieve its objectives. This faculty covers various aspects of business management, including finance, human resources, marketing, operations, strategy, and organizational behavior.", "Business Administration" },
                    { new Guid("ca127f5c-126a-4f6a-9a03-167a6f6f5f44"), "Artificial Intelligence (AI) involves the development of computer systems capable of performing tasks that typically require human intelligence. This faculty delves into machine learning, natural language processing, robotics, computer vision, and other AI techniques", "Artificial Intelligence" },
                    { new Guid("f14662ff-c8e2-45af-bef1-aeb00413af73"), "Graphic design is the art and practice of visual communication. It involves creating visual content using typography, images, and other elements to convey messages or ideas effectively. Students in this faculty learn about design principles, software tools, branding, and digital media.", "Graphic Design" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("0d160f4d-3d44-4d73-b6d2-501b034d8dc6"), "Administrator" },
                    { new Guid("75afca28-6716-4a50-8ccb-eac3dc2d5470"), "Manager" },
                    { new Guid("ad0762dd-f0ce-48d6-9a9e-d58d986aec49"), "Coordinator" },
                    { new Guid("b57dab76-301d-49d2-883a-15ef47c19630"), "Student" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "DateOfBirth", "Email", "FacultyId", "FirstName", "LastName", "Password", "PhoneNumber", "ProfilePicture", "RoleId" },
                values: new object[] { new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"), "Admin Address", new DateTime(2024, 3, 10, 23, 45, 15, 816, DateTimeKind.Local).AddTicks(9852), "admin@gmail.com", new Guid("52b9ef2e-0e06-4443-8ec7-b008a0ffd30b"), "Admin", "Admin", "AQAAAAEAACcQAAAAEL12XjVZj2eTXgU0FZLNfJWo2JvvpPAcLhRn0lixXNub8ZQAFokPxGuAgU5cdNk5mA==", "1234567890", null, new Guid("0d160f4d-3d44-4d73-b6d2-501b034d8dc6") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("04df80e1-fd59-4121-af73-e5356a2abd2e"));

            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("3801a15f-18d4-40c3-8a70-28c8a35ca010"));

            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("4f1e0068-0c6a-4a45-8f8c-92fc7f4e58b5"));

            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("81495466-6fdb-45e6-bfd2-1995597d7df9"));

            migrationBuilder.DeleteData(
                table: "AnnualMagazines",
                keyColumn: "AnnualMagazineId",
                keyValue: new Guid("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("2c0983c8-bded-48ee-a2fa-9389313152c8"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("3cf4f7fa-fdf1-420a-9e78-21ffb518144f"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("a852f27c-f94d-48a0-8c94-d0467afc3b12"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("b338eded-0c32-421c-80c9-5aef828be112"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("ca127f5c-126a-4f6a-9a03-167a6f6f5f44"));

            migrationBuilder.DeleteData(
                table: "Faculties",
                keyColumn: "FacultyId",
                keyValue: new Guid("f14662ff-c8e2-45af-bef1-aeb00413af73"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("75afca28-6716-4a50-8ccb-eac3dc2d5470"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("ad0762dd-f0ce-48d6-9a9e-d58d986aec49"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("b57dab76-301d-49d2-883a-15ef47c19630"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("86f04ea5-9421-42d0-bfde-e75a7b01dc3f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("0d160f4d-3d44-4d73-b6d2-501b034d8dc6"));

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
    }
}
