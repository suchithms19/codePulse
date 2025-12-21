using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace codePulse.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "1e2a862d-234f-433d-a3ae-f11518baf83f";
            var writerRoleId = "e02d7a25-28e7-4674-89dc-6c7a76f82629";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id =readerRoleId,
                    Name="Reader",
                    NormalizedName="READER",
                    ConcurrencyStamp= readerRoleId
                },

                new IdentityRole()
                {
                    Id =writerRoleId,
                    Name="Writer",
                    NormalizedName="WRITER",
                    ConcurrencyStamp= writerRoleId
                }
            };

            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            //create an admin user
            var adminUserId = "c4b2f1e8-5f3a-4d2e-9c3b-1e2f3a4b5c6d";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            //seed the admin user
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    RoleId= readerRoleId,
                    UserId= adminUserId
                },
                new()
                {
                    RoleId= writerRoleId,
                    UserId= adminUserId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }

}