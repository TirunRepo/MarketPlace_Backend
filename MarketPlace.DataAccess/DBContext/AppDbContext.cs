using MarketPlace.DataAccess.Entities;
using MarketPlace.DataAccess.Entities.Inventory;
using MarketPlace.DataAccess.Entities.Markup;
using MarketPlace.DataAccess.Entities.Promotions;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.DataAccess.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<PromotionModel> Promotions { get; set; }
        public DbSet<MarkupDetail> MarkupDetails { get; set; }

        public DbSet<CruiseLine> CruiseLines { get; set; }
        public DbSet<CruiseShip> CruiseShips { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<DeparturePort> DeparturePorts { get; set; }
        public DbSet<CruiseInventory> CruiseInventories { get; set; }
        public DbSet<CruisePricingInventory> CruisePricingInventories { get; set; }
        public DbSet<SailDate> SailDates { get; set; }
        public DbSet<CruisePricingCabin> Cabins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}
