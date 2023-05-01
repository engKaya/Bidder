using Bidder.UserService.Domain.Models;
using Bidder.UserService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidder.UserService.Infastructure.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USERS", UserDbContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id);
            builder.Ignore(k=>k.DomainEvents);
            builder.Property(k=>k.Id).ValueGeneratedOnAdd();
            builder.Property(k => k.Username).IsRequired();
            builder.Property(k => k.Password).IsRequired();
        }
    }
}
