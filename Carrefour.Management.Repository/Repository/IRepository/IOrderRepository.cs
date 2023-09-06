using Carrefour.Management.Repository.Entities;
using System.Linq.Expressions;

namespace Carrefour.Management.Repository.Repository.IRepository
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<Order> GetLastAsync(Expression<Func<Order, bool>> filter = null, bool tracked = true);
    }
}
