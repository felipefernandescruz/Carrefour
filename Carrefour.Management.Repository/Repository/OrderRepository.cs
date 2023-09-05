using Carrefour.Management.Repository.Context;
using Carrefour.Management.Repository.Entities;
using Carrefour.Management.Repository.Repository.IRepository;

namespace Carrefour.Management.Repository.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
