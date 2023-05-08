using Business.Services.Enumerations;
using Data.Access.Layer.Models;
using Data.Transfer.Object.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace CvThèque.Extensions
{
    public static class ConfigureDefaultRolesAndUser
    {
        public async static Task ConfigureDefaultUserAndRoles(this WebApplication app)
        {
            try
            {
                using (var serviceScope = app.Services.CreateScope())
                {
                    await CreateDefaultRoles(serviceScope);
                    await CreateDefaultSuperAdmin(serviceScope);
                }

            }
            catch
            {
                throw;
            }
        }

        private static async Task CreateDefaultRoles(IServiceScope serviceScope)
        {
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<Guid>>>();

            if (roleManager == null)
                throw new Exception("Role Manager not registred");

            foreach (var role in Enum.GetNames(typeof(ROLES)))
                if (await roleManager.RoleExistsAsync(role) == false)
                    await roleManager.CreateAsync(new IdentityRole<Guid>()
                    {
                        Name = role
                    });
        }

        private static async Task CreateDefaultSuperAdmin(IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<Admin>>();
            var configurationSettingOptions = serviceScope.ServiceProvider.GetService<IOptions<DefaultUserConfiguration>>();

            if (userManager == null)
                throw new Exception("User manager not registred");

            if (configurationSettingOptions == null)
                throw new Exception("Configuration service not registred");

            var DefaultSuperAdminConfiguration = configurationSettingOptions.Value;

            if (DefaultSuperAdminConfiguration == null)
                throw new Exception("Default Super admin configuration incorrect");

            if (await userManager.FindByEmailAsync(DefaultSuperAdminConfiguration.SuperAdminEmail) == null)
            {
                //We create first the new SUPERADMIN
                var identityResult = await userManager.CreateAsync(new Admin()
                {
                    Email = DefaultSuperAdminConfiguration.SuperAdminEmail,
                    EmailConfirmed = true,
                    CreatedDate = DateTime.Now,
                    IsSuperAdmin = true,
                    Nom = DefaultSuperAdminConfiguration.SuperAdminUserName,
                    Prenom = DefaultSuperAdminConfiguration.SuperAdminUserName,
                    UserName = DefaultSuperAdminConfiguration.SuperAdminUserName,
                    Id = Guid.NewGuid()
                });

                if (!identityResult.Succeeded)
                    throw new Exception("Could not create the default SUPERADMIN");

                var superAdmin = await userManager.FindByEmailAsync(DefaultSuperAdminConfiguration.SuperAdminEmail);

                if (superAdmin == null)
                    throw new Exception("Default super admin could not be found");

                //We assign the SuperAdmin password
                identityResult = await userManager.AddPasswordAsync(superAdmin, DefaultSuperAdminConfiguration.SuperAdminPassword);

                if (!identityResult.Succeeded)
                    throw new Exception("Could not set the password for the default super admin");

                //We assign the identity role to the default super admin user
                identityResult = await userManager.AddToRoleAsync(superAdmin, ROLES.SUPERADMIN.ToString());

                if (!identityResult.Succeeded)
                    throw new Exception("Could not set the identity role for the default super admin user");
            }
        }
    }
}