using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.DAL.EF;

/*
 * Custom DbContext factory
 * This is needed for using dotnet codegenerator for MVC pages.
 * https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
 */
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5888;Username=postgres;Password=postgres;database=inventoryApp;");
        
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}