using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniMagContributions.Constraints;
using UniMagContributions.Repositories.Interface;

namespace UniMagContributions.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = Guid.NewGuid(), Name = ERole.Administrator.ToString() },
                new Role { RoleId = Guid.NewGuid(), Name = ERole.Manager.ToString() },
                new Role { RoleId = Guid.NewGuid(), Name = ERole.Coordinator.ToString() },
                new Role { RoleId = Guid.NewGuid(), Name = ERole.Student.ToString() }
            );
        }

        public static async Task SeedAdminAsync(IServiceProvider service)
        {
            var context = service.GetRequiredService<ApplicationDbContext>();
            var roleRepository = service.GetRequiredService<IRoleRepository>();

            if (context.Users.Any(u => u.Email == "admin@gmail.com"))
            {
                return;
            }

            Guid adminId = Guid.NewGuid();
            string adminPassword = "admin";

            PasswordHasher<string> passwordHasher = new();
            string password = passwordHasher.HashPassword(null, adminPassword);

            Role adminRole = roleRepository.GetRoleByName(ERole.Administrator.ToString()) ?? throw new Exception("Role not found");

            context.Users.Add(new User
            {
                UserId = adminId,
                Email = "admin@gmail.com",
                Password = password,
                FirstName = "Admin",
                LastName = "Admin",
                DateOfBirth = DateTime.Now,
                Address = "Admin Address",
                PhoneNumber = "1234567890",
                RoleId = adminRole.RoleId
            });
            context.SaveChanges();
        }
    }
}
