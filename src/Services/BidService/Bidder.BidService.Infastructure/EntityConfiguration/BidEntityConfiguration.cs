

using Bidder.BidService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidder.BidService.Infastructure.EntityConfiguration
{
    public class BidEntityConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.ToTable(nameof(Bid));
            builder.HasKey(k => k.Id);
            builder.Ignore(k => k.DomainEvents);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Property(k => k.CategoryId);
            builder.Property(k => k.EndDate).IsRequired();
            builder.Property(k => k.Title).IsRequired();
            builder.Property(k => k.Description).IsRequired();
            builder.Property(k=> k.MinPrice);
            builder.Property(k=>k.IsEnded).IsRequired();
            builder.Property(k => k.HasIncreaseRest).IsRequired();
            builder.Property(k => k.MinPriceIncrease);
            builder.Property(k => k.ProductType);
        }
    }
}
