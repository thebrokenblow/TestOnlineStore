using Microsoft.EntityFrameworkCore;
using TestOnlienStore.Domain;

namespace TestOnlineStore.Persistence;

public class TestOnlineStoreDBContext(DbContextOptions<TestOnlineStoreDBContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
}