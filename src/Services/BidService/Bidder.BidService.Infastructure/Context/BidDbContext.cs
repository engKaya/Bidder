using Bidder.BidService.Domain.Entities;
using Bidder.BidService.Infastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Bidder.BidService.Infastructure.Context
{
    public class BidDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "BIDDER_BID";

        public BidDbContext(DbContextOptions<BidDbContext> options) : base(options)
        {
        }

        public DbSet<Bid> Bids{ get; set; } 
        public DbSet<BidRoom> BidRooms { get; set; } 
        public DbSet<BidRoomUser> BidRoomUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BidEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BidRoomEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BidRoomUserEntityConfiguration()); 
            base.OnModelCreating(modelBuilder); 
        }
    }
}
