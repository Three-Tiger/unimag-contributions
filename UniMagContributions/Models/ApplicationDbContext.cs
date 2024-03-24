using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using UniMagContributions.Constraints;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
                {
                    if (!databaseCreator.CanConnect())
                    {
                        databaseCreator.Create();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AnnualMagazine> AnnualMagazines { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<ImageDetails> ImageDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feedback>()
                .HasOne(u => u.User)
                .WithMany(fb => fb.Feedbacks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(u => u.Contribution)
                .WithMany(fb => fb.Feedbacks)
                .HasForeignKey(u => u.ContributionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = new Guid("0D160F4D-3D44-4D73-B6D2-501B034D8DC6"), Name = ERole.Administrator.ToString() },
                new Role { RoleId = new Guid("75AFCA28-6716-4A50-8CCB-EAC3DC2D5470"), Name = ERole.Manager.ToString() },
                new Role { RoleId = new Guid("AD0762DD-F0CE-48D6-9A9E-D58D986AEC49"), Name = ERole.Coordinator.ToString() },
                new Role { RoleId = new Guid("B57DAB76-301D-49D2-883A-15EF47C19630"), Name = ERole.Student.ToString() },
                new Role { RoleId = new Guid("636E7B42-1831-44CB-8E6D-FA90B6076EDB"), Name = ERole.Guest.ToString() }
            );

            string adminPassword = "ThreeTiger@2024";
            PasswordHasher<string> passwordHasher = new();
            string password = passwordHasher.HashPassword(null, adminPassword);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = new Guid("86F04EA5-9421-42D0-BFDE-E75A7B01DC3F"),
                    Email = "admin@gmail.com",
                    FacultyId = new Guid("52B9EF2E-0E06-4443-8EC7-B008A0FFD30B"),
                    Password = password,
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(2002, 8, 5),
                    Address = "Admin Address",
                    PhoneNumber = "1234567890",
                    RoleId = new Guid("0D160F4D-3D44-4D73-B6D2-501B034D8DC6")
                }
            );

            // Seed Faculties
            modelBuilder.Entity<Faculty>().HasData(
                new Faculty
                {
                    FacultyId = new Guid("52B9EF2E-0E06-4443-8EC7-B008A0FFD30B"),
                    Name = "Admin",
                    Description = "Admin Description"
                },
                new Faculty
                {
                    FacultyId = new Guid("3CF4F7FA-FDF1-420A-9E78-21FFB518144F"),
                    Name = "Information Technology",
                    Description = "This faculty focuses on the study and application of computing technology, encompassing areas such as computer systems, software development, networking, cybersecurity, database management, and more."
                },
                new Faculty
                {
                    FacultyId = new Guid("CA127F5C-126A-4F6A-9A03-167A6F6F5F44"),
                    Name = "Artificial Intelligence",
                    Description = "Artificial Intelligence (AI) involves the development of computer systems capable of performing tasks that typically require human intelligence. This faculty delves into machine learning, natural language processing, robotics, computer vision, and other AI techniques"
                },
                new Faculty
                {
                    FacultyId = new Guid("F14662FF-C8E2-45AF-BEF1-AEB00413AF73"),
                    Name = "Graphic Design",
                    Description = "Graphic design is the art and practice of visual communication. It involves creating visual content using typography, images, and other elements to convey messages or ideas effectively. Students in this faculty learn about design principles, software tools, branding, and digital media."
                },
                new Faculty
                {
                    FacultyId = new Guid("B338EDED-0C32-421C-80C9-5AEF828BE112"),
                    Name = "Business Administration",
                    Description = "Business administration involves the management of operations and resources within an organization to achieve its objectives. This faculty covers various aspects of business management, including finance, human resources, marketing, operations, strategy, and organizational behavior."
                },
                new Faculty
                {
                    FacultyId = new Guid("A852F27C-F94D-48A0-8C94-D0467AFC3B12"),
                    Name = "International Business",
                    Description = "International business focuses on the study of business activities that cross national borders. This faculty covers topics such as global trade, international finance, cultural differences, market entry strategies, global supply chain management, and international marketing."
                },
                new Faculty
                {
                    FacultyId = new Guid("2C0983C8-BDED-48EE-A2FA-9389313152C8"),
                    Name = "Marketing Management",
                    Description = "Marketing management involves the planning, implementation, and control of marketing programs to achieve organizational goals. This faculty covers market research, consumer behavior, advertising, branding, digital marketing, and strategic marketing planning."
                }
            );

            // Seed Annual Magazines
            modelBuilder.Entity<AnnualMagazine>().HasData(
                new AnnualMagazine
                {
                    AnnualMagazineId = new Guid("81495466-6FDB-45E6-BFD2-1995597D7DF9"),
                    AcademicYear = "2018-2019",
                    Title = "Effect of Using Mobile Phone",
                    Description = "The use of mobile phones has various effects on individuals and society. While they provide convenience and connectivity, overuse can lead to negative impacts such as decreased attention spans, disrupted sleep patterns, and increased risk of accidents due to distracted driving. Additionally, the production and disposal of mobile phones contribute to environmental pollution through the extraction of raw materials, energy consumption during manufacturing, and electronic waste generation.",
                    ClosureDate = new DateTime(2019, 5, 1, 23, 59, 0),
                    FinalClosureDate = new DateTime(2019, 6, 30, 23, 59, 0)
                },
                new AnnualMagazine
                {
                    AnnualMagazineId = new Guid("04DF80E1-FD59-4121-AF73-E5356A2ABD2E"),
                    AcademicYear = "2019-2020",
                    Title = "Environmental pollution and its consequences",
                    Description = "Environmental pollution refers to the contamination of the natural environment by harmful substances, resulting in adverse effects on ecosystems, human health, and biodiversity. Pollution sources include industrial activities, transportation, agriculture, and improper waste disposal. Consequences of pollution include air and water quality degradation, soil contamination, climate change, and negative impacts on wildlife and human populations.",
                    ClosureDate = new DateTime(2020, 5, 1, 23, 59, 0),
                    FinalClosureDate = new DateTime(2020, 6, 30, 23, 59, 0)
                },
                new AnnualMagazine
                {
                    AnnualMagazineId = new Guid("3801A15F-18D4-40C3-8A70-28C8A35CA010"),
                    AcademicYear = "2020-2021",
                    Title = "The impact of social media on society",
                    Description = "Social media platforms have profoundly influenced modern society by changing communication patterns, social interactions, and information dissemination. While they facilitate connectivity and access to diverse perspectives, social media also raise concerns about privacy, mental health, and the spread of misinformation. The addictive nature of social media usage can lead to decreased productivity and increased feelings of loneliness and depression among users.",
                    ClosureDate = new DateTime(2021, 5, 1, 23, 59, 0),
                    FinalClosureDate = new DateTime(2021, 6, 30, 23, 59, 0)
                },
                new AnnualMagazine
                {
                    AnnualMagazineId = new Guid("A3A3A3A3-A3A3-A3A3-A3A3-A3A3A3A3A3A3"),
                    AcademicYear = "2021-2022",
                    Title = "Should we beat children to educate them or not?",
                    Description = "The practice of physically disciplining children, such as spanking or corporal punishment, remains a contentious issue. While some argue that it can be an effective way to discipline and teach obedience, others advocate for non-violent forms of discipline that focus on positive reinforcement and communication. Research suggests that physical punishment can have negative long-term effects on children's mental health and behavior, leading to aggression, low self-esteem, and increased likelihood of engaging in violent behavior.",
                    ClosureDate = new DateTime(2022, 5, 1, 23, 59, 0),
                    FinalClosureDate = new DateTime(2022, 6, 30, 23, 59, 0)
                },
                new AnnualMagazine
                {
                    AnnualMagazineId = new Guid("4F1E0068-0C6A-4A45-8F8C-92FC7F4E58B5"),
                    AcademicYear = "2022-2023",
                    Title = "Blockchain application in supply chain management",
                    Description = "Blockchain technology offers innovative solutions to enhance transparency, traceability, and efficiency in supply chain management. By creating a decentralized and immutable ledger of transactions, blockchain enables secure and verifiable recording of supply chain activities, including sourcing, production, transportation, and delivery. This technology can help mitigate risks such as counterfeiting, fraud, and supply chain disruptions while improving trust among stakeholders and ensuring ethical practices.",
                    ClosureDate = new DateTime(2023, 5, 1, 23, 59, 0),
                    FinalClosureDate = new DateTime(2023, 6, 30, 23, 59, 0)
                }
            );
        }
    }
}
