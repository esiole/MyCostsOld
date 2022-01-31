using Microsoft.EntityFrameworkCore;

namespace MyCosts.API.Data;

public class MyCostsDbContext : DbContext
{
    public readonly object Locker = new();

    //public virtual DbSet<Cost> Costs { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    //public virtual DbSet<Role> Roles { get; set; }
    //public virtual DbSet<UserRole> UserRoles { get; set; }
    //public virtual DbSet<User> Users { get; set; }

    public MyCostsDbContext() { }
    public MyCostsDbContext(DbContextOptions<MyCostsDbContext> options) : base(options) { }
}
