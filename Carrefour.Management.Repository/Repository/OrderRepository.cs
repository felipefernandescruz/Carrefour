using Carrefour.Management.Repository.Context;
using Carrefour.Management.Repository.Entities;
using Carrefour.Management.Repository.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Carrefour.Management.Repository.Repository
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<Order> dbSet;

        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
            this.dbSet = _db.Set<Order>();
        }

        public async Task<Order> GetLastAsync(Expression<Func<Order, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Order> query = dbSet;
            if (!tracked)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.OrderBy(item=> item.CreatedAt).Where(filter);

            return await query.LastOrDefaultAsync();
        }
    }
}
