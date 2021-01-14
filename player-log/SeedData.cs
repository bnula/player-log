using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log
{
    public static class SeedData
    {
        public static void Seed(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            // check if there is a default user
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                // initialize a default user
                var user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com"
                };
                // create the default user in the db
                var result = userManager.CreateAsync(user, "password").Result;
                // check if the creation was a success
                if (result.Succeeded)
                {
                    // assign the user to the admin role
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // check if the role exists
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                // initialize the role with a given name
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
                // create the role
                roleManager.CreateAsync(role);
            }

            // check if the role exists
            if (!roleManager.RoleExistsAsync("Player").Result)
            {
                // initialize the role with a given name
                var role = new IdentityRole
                {
                    Name = "Player"
                };
                // create the role
                roleManager.CreateAsync(role);
            }

            // check if the role exists
            if (!roleManager.RoleExistsAsync("DM").Result)
            {
                // initialize the role with a given name
                var role = new IdentityRole
                {
                    Name = "DM"
                };
                // create the role
                roleManager.CreateAsync(role);
            }
        }
    }
}
