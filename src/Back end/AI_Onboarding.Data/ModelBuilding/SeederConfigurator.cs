using AI_Onboarding.Common;
using AI_Onboarding.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AI_Onboarding.Data.ModelBuilding
{
    public static class SeederConfigurator
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = Roles.Administrator
                },
                new Role
                {
                    Id = 2,
                    Name = Roles.Employee
                }
            );

            var password = "pass1234";
            var passwordHasher = new PasswordHasher<User>();

            var admin1 = new User
            {
                Id = 1,
                Email = "admin1@admin.com",
                UserName = "admin1@admin.com",
                FirstName = "Admin1",
                LastName = "Admin1",
                RoleId = 1
            };

            var admin2 = new User
            {
                Id = 2,
                Email = "admin2@admin.com",
                UserName = "admin2@admin.com",
                FirstName = "Admin2",
                LastName = "Admin2",
                RoleId = 1
            };

            var user = new User
            {
                Id = 3,
                Email = "user@mail.com",
                UserName = "user@mail.com",
                FirstName = "User",
                LastName = "User",
                RoleId = 2
            };

            admin1.PasswordHash = passwordHasher.HashPassword(admin1, password);
            admin1.NormalizedEmail = admin1.Email.ToUpper();
            admin1.NormalizedUserName = admin1.UserName.ToUpper();
            admin1.SecurityStamp = Guid.NewGuid().ToString();

            admin2.PasswordHash = passwordHasher.HashPassword(admin2, password);
            admin2.NormalizedEmail = admin2.Email.ToUpper();
            admin2.NormalizedUserName = admin2.UserName.ToUpper();
            admin2.SecurityStamp = Guid.NewGuid().ToString();

            user.PasswordHash = passwordHasher.HashPassword(user, password);
            user.NormalizedEmail = user.Email.ToUpper();
            user.NormalizedUserName = user.UserName.ToUpper();
            user.SecurityStamp = Guid.NewGuid().ToString();

            var users = new List<User>()
            {
                admin1,
                admin2,
                user
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
