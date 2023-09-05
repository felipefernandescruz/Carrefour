using Carrefour.Management.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Carrefour.Management.Repository.Map
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.OrderType)
                .WithMany()
                .HasForeignKey(x => x.OrderTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
