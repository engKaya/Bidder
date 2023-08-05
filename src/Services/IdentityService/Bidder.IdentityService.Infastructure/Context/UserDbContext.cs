using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Infastructure.EntityConfigrations;
using Microsoft.EntityFrameworkCore;

namespace Bidder.IdentityService.Infastructure.Context
{
    public class UserDbContext : DbContext 
    {
        public const string DEFAULT_SCHEMA = "BIDDER_USER";
         
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserDbConfiguration());
        }  
    }
}
