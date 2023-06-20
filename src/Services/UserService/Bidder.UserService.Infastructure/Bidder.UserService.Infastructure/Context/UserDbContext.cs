using Bidder.UserService.Domain.Abstract.UnitOfWork;
using Bidder.UserService.Domain.Models;
using Bidder.UserService.Infastructure.Configurations;
using Bidder.UserService.Infastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bidder.UserService.Infastructure.Context
{
    public class UserDbContext : DbContext, IUnitOfWork
    {

        public const string DEFAULT_SCHEMA = "BIDDER_USER";

        private readonly IMediator mediator;

        public UserDbContext(DbContextOptions<UserDbContext> options, IMediator mediator) : base(options) 
        {
            this.mediator = mediator;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellation = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellation)
        {
            await mediator.DispatchDomainEvents(this);
            await base.SaveChangesAsync(cancellation);
            return true;
        }
    }
}
