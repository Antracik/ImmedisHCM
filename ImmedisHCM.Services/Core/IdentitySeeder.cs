using ImmedisHCM.Data.Identity.Entities;
using System;
using Microsoft.AspNetCore.Identity;

namespace ImmedisHCM.Services.Core
{
    public class IdentitySeeder : IIdentitySeeder
    {

        private readonly UserManager<WebUser> _userManager;
        private readonly RoleManager<WebRole> _roleManager;

        public IdentitySeeder(UserManager<WebUser> userManager, RoleManager<WebRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Seed()
        {
            if(!_roleManager.RoleExistsAsync("Admin").Result)
            {
                 var res = _roleManager.CreateAsync(new WebRole { Name = "Admin" }).Result;
                ThrowIfNotSucceeded(res);
            }
            
            if(!_roleManager.RoleExistsAsync("Manager").Result)
            {
                var res = _roleManager.CreateAsync(new WebRole { Name = "Manager" }).Result;
                ThrowIfNotSucceeded(res);
            }

            if (!_roleManager.RoleExistsAsync("Employee").Result)
            {
                var res = _roleManager.CreateAsync(new WebRole { Name = "Employee" }).Result;
                ThrowIfNotSucceeded(res);
            }

            //if there's already an admin, then don't setup default admin
            if (_userManager.GetUsersInRoleAsync("Admin").Result.Count == 0)
            {
                var admin = _userManager.FindByEmailAsync("admin@admin.god").Result;

                if (admin == null)
                {
                    admin = new WebUser { Email = "admin@admin.god", UserName = "admin@admin.god" };
                    var res = _userManager.CreateAsync(admin, "P@ssw0rd").Result;
                    ThrowIfNotSucceeded(res);
                }

                if (!_userManager.IsInRoleAsync(admin, "Admin").Result)
                {
                    var res = _userManager.AddToRoleAsync(admin, "Admin").Result;
                    ThrowIfNotSucceeded(res);
                }
            }
        }


        private void ThrowIfNotSucceeded(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new Exception("There was a problem seeding the admin");
        }
    }
}
