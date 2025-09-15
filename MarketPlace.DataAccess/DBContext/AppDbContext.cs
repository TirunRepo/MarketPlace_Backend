using MarketPlace.DataAccess.Entities;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
    }
}
