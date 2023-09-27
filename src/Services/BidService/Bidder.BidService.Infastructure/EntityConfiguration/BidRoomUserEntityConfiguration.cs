using Bidder.BidService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bidder.BidService.Infastructure.EntityConfiguration
{
    public class BidRoomUserEntityConfiguration : IEntityTypeConfiguration<BidRoomUser>
    {
        public void Configure(EntityTypeBuilder<BidRoomUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BidRoomId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.JoinedDate).IsRequired();
            builder.Property(x => x.ExitDate).IsRequired(false);
            builder.HasOne(x => x.BidRoom).WithMany(x => x.BidRoomUsers).HasForeignKey(x => x.BidRoomId);
        }
    }
}
