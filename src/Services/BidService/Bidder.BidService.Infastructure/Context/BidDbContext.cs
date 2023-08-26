using Microsoft.EntityFrameworkCore;

namespace Bidder.BidService.Infastructure.Context
{
    public class BidDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "BIDDER_BID";

        public BidDbContext(DbContextOptions<BidDbContext> options) : base(options)
        {
        }

        //public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }
    }
}
