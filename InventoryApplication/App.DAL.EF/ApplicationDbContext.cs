using App.Domain.Identity;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Storage> Storages { get; set; } = default!;
    public DbSet<StorageItem> StorageItems { get; set; } = default!;
    public DbSet<ItemAttribute> ItemAttributes { get; set; } = default!;
    public DbSet<AttributeInItem> AttributeInItems { get; set; } = default!;
    
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //add unique identifiers
        builder.Entity<ItemAttribute>()
            .HasIndex(x => x.AttributeName)
            .IsUnique();

        // //remove cascade delete
        // foreach (var relationship in builder.Model
        //              .GetEntityTypes()
        //              .SelectMany(e => e.GetForeignKeys()))
        // {
        //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
        // }
        //
        
    }
    
    public override int SaveChanges()
    {
        FixEntities(this);
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        FixEntities(this);
        return base.SaveChangesAsync(cancellationToken);
    }
    /*
     * Set DateTime to UTC
     * https://stackoverflow.com/questions/50727860/ef-core-2-1-hasconversion-on-all-properties-of-type-datetime
     * https://stackoverflow.com/questions/69961449/net6-and-datetime-problem-cannot-write-datetime-with-kind-utc-to-postgresql-ty/70142836#70142836
     */
    private static void FixEntities(DbContext context)
    {
        //get entities that have a DateTime property.
        var dateProperties = context.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
            .Select(z => new
            {
                ParentName = z.DeclaringEntityType.Name,
                PropertyName = z.Name
            });
        
        //Get entities to be persisted to db.
        var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified)
            .Select(x => x.Entity);

        //Get datetime properties of entities to be persisted to db. Set those properties to UTC
        foreach (var entity in editedEntitiesInTheDbContextGraph)
        {
            var entityFields = dateProperties.Where(d => d.ParentName == entity.GetType().FullName);

            foreach (var property in entityFields)
            {
                var prop = entity.GetType().GetProperty(property.PropertyName);

                if (prop == null)
                    continue;

                var originalValue = prop.GetValue(entity) as DateTime?;
                if (originalValue == null)
                    continue;

                prop.SetValue(entity, DateTime.SpecifyKind(originalValue.Value, DateTimeKind.Utc));
            }
        }
    }
}