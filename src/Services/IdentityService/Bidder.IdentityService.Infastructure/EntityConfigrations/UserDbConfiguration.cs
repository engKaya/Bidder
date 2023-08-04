using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Infastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidder.IdentityService.Infastructure.EntityConfigrations
{
    public class UserDbConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {

            builder.ToTable("USERS", UserDbContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id); 
            builder.Ignore(k => k.DomainEvents);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Property(k => k.Name).IsRequired();
            builder.Property(k => k.Surname).IsRequired();
            builder.Property(k => k.Username).IsRequired();
            builder.Property(k => k.Password).IsRequired();
            builder.Property(k => k.Email).IsRequired();
            builder.Property(k => k.PhoneNumber).IsRequired();
            builder.Property(k => k.IdentityNumber).IsRequired();
            builder.Property(k => k.IsEmailVerified).IsRequired();
            builder.Property(k => k.EmailVerifiedAt);
            builder.Property(k => k.CreatedAt).IsRequired();
        }
    }
}
