using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using TeamLeasing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TeamLeasing.DAL
{

    public class SeedRoles
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            foreach (var role in Enum.GetNames(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    IdentityRole newRole = new IdentityRole(role.ToString());
                    var rezult = await _roleManager.CreateAsync(newRole);
                }
            }

        }
    }

    public enum Roles
    {
        Administrator,
        Employee,
        Developer
    }
}