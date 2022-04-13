using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Helpers;

public class ApplicationSetupDataHelper
{
     public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

        if (context == null)
        {
            throw new ApplicationException("No ApplicationDbContext Found!");
        }

        if (config.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<ApplicationRole>>();

            if (userManager == null)
            {
                throw new NullReferenceException("UserManager not found");
            }
            
            AddRoles(roleManager);
            AddUsers(userManager);
        }

        if (config.GetValue<bool>("DataInitialization:SeedData"))
        {
           
        }
    }
     private static void AddUsers(UserManager<ApplicationUser>? userManager)
     {
         var users = new (string firstName, string lastName, string email, string password, string roles)[]
         {
             ("Super", "User", "suAdmin@test.ee", "adminPass", "user,admin")
         };
         
         foreach (var userInfo in users)
         {
             var user = userManager?.FindByEmailAsync(userInfo.email).Result;
             if (user == null)
             {
                 user = new ApplicationUser
                 {
                     FirstName = userInfo.firstName,
                     LastName = userInfo.lastName,
                     Email = userInfo.email,
                     UserName = userInfo.email,
                     EmailConfirmed = true
                 };
                 var identityResult = userManager?.CreateAsync(user, userInfo.password).Result;
                 if (identityResult != null && !identityResult.Succeeded)
                 {
                     throw new ApplicationException("User creation failed.");
                 }
             }

             var identityResultRole = userManager?.AddToRolesAsync(user, userInfo.roles.Split(",")).Result;
         }
     }
    private static void AddRoles(RoleManager<ApplicationRole>? roleManager)
    {
        if (roleManager == null)
        {
            throw new NullReferenceException("RoleManager not found");
        }

        var roleInfo = new[]
        {
            "admin",
            "user",
        };

        foreach (var roleName in roleInfo)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;
            if (role == null)
            {
                var identityResult = roleManager.CreateAsync(new ApplicationRole {Name = roleName}).Result;
                if (!identityResult.Succeeded)
                {
                    throw new ApplicationException("Role creation failed.");
                }
            }
        }
    }
}