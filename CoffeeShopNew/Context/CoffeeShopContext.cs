using CoffeeShopNew.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopNew.Context;

public class CoffeeShopContext:DbContext
{
    public CoffeeShopContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<CoffeeItem> CoffeeItems { get; set; }
    public DbSet<MiscItem> MiscItems { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Image> Images { get; set; }
}