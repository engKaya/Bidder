using Bidder.BidService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidder.BidService.Infastructure.EntityConfiguration
{
    public class BidRoomEntityConfiguration : IEntityTypeConfiguration<BidRoom>
    {
        public void Configure(EntityTypeBuilder<BidRoom> builder)
        {
            builder.ToTable(nameof(BidRoom));
            builder.HasKey(k => k.Id);
            builder.Ignore(k => k.DomainEvents);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
            builder.Property(k => k.BidId);
            builder.Property(k => k.BidName).IsRequired();
            builder.Property(k => k.RoomStatus).HasConversion<int>();
            builder.HasOne(k => k.Bid).WithOne(k => k.BidRoom).HasForeignKey<BidRoom>(a => a.BidId);
        }
    }
}
