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
                    Name = Roles.Administrator,
                    NormalizedName = Roles.Administrator.ToUpper()
                },
                new Role
                {
                    Id = 2,
                    Name = Roles.Employee,
                    NormalizedName = Roles.Employee.ToUpper()
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
                EmailConfirmed = true
            };

            var admin2 = new User
            {
                Id = 2,
                Email = "admin2@admin.com",
                UserName = "admin2@admin.com",
                FirstName = "Admin2",
                LastName = "Admin2",
                EmailConfirmed = true
            };

            var user = new User
            {
                Id = 3,
                Email = "user@mail.com",
                UserName = "user@mail.com",
                FirstName = "User",
                LastName = "User",
                EmailConfirmed = true
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

            var userRoles = new List<UserRole>();

            userRoles.Add(new UserRole
            {
                UserId = users[0].Id,
                RoleId = 1
            });
            userRoles.Add(new UserRole
            {
                UserId = users[1].Id,
                RoleId = 1
            });
            userRoles.Add(new UserRole
            {
                UserId = users[2].Id,
                RoleId = 2
            });

            modelBuilder.Entity<UserRole>().HasData(userRoles);
        }
    }
}
