using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Infrastructure.Persistence.Interceptors;
using System.Reflection;

namespace ReverseAnalytics.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<SaleItem> SaleItems { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<SupplyItem> SupplyItems { get; set; }
    public virtual DbSet<Supply> Supplies { get; set; }
    public virtual DbSet<Transaction> Transaction { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ConfigureDecimalAndDouble(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void ConfigureDecimalAndDouble(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType
                .ClrType
                .GetProperties()
                .Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(double));

            foreach (var property in properties)
            {
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasColumnType("decimal(18, 2)");
            }
        }
    }
}
