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
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(o => new { o.UserId, o.RoleId });

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

            if (context.Users.Any(u => u.Username == "admin"))
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
                Username = "admin",
                Email = "admin@gmail.com",
                Password = password,
                FirstName = "Admin",
                LastName = "Admin",
                DateOfBirth = DateTime.Now,
                Address = "Admin Address",
                PhoneNumber = "1234567890",
                UserRoles = new List<UserRole>
                    {
                        new UserRole
                        {
                            UserId = adminId,
                            RoleId = adminRole.RoleId
                        }
                    }
            });
            context.SaveChanges();
        }
    }
}
