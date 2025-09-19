using MarketPlace.Common.CommonModel;
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

        //Users
        public DbSet<User> Users { get; set; } = null!;
        //Inventory
        public DbSet<CruiseInventory> CruiseInventories { get; set; }
        //CruiseCabinPricingInventory
        public DbSet<CruisePricingInventory> CruiseCabinPricingInventories { get; set; }
        //Cabin details
        public DbSet<CruiseCabinInventory> CruiseCabinInventory { get; set; }
        //DeparturePort
        public DbSet<DeparturePort> DeparturePorts { get; set; }
        //Destination
        public DbSet<Destination> Destinations { get; set; }
        //CruiseLines
        public DbSet<CruiseLine> CruiseLines { get; set; }
        //CruiseShips
        public DbSet<CruiseShip> CruiseShips { get; set; }
        //Promotion
        public DbSet<PromotionModel> Promotions { get; set; }
        //MarupDetails
        public DbSet<MarkupDetail> MarkupDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
