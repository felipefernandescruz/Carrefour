using Carrefour.Management.Repository.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Carrefour.Management.Repository.Enum;

namespace Carrefour.Management.Repository.Map
{
    public class OrderTypeMap : IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder)
        {
            builder.HasData(
              new OrderType()
              {
                  Id = (int)OrderTypeEnum.Debt,
                  Name = OrderTypeEnum.Debt.ToString()
              },
              new OrderType()
              {
                  Id = (int)OrderTypeEnum.Credit,
                  Name = OrderTypeEnum.Credit.ToString()
              },
              new OrderType()
              {
                  Id = (int)OrderTypeEnum.Balance,
                  Name = OrderTypeEnum.Balance.ToString()
              });
        }
    }
}
