using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Data.RepoOrder;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoOrder
{
    public class RepositoryOrder : IRepository, IRepositoryOrder
    {
        private readonly DotzContext _context;

        public RepositoryOrder(DotzContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
              _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() > 0);
        }

        public Order[] GetAllOrders()
        {
             IQueryable<Order> query = _context.Orders;

             query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<Order>> GetAllOrdersAsync(PageParams pageParams)
        {
             IQueryable<Order> query = _context.Orders;

             query = query.Include(d => d.OrderItems);

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Filtro))
                query = query.Where(order =>   order.Number
                                                  .Contains(pageParams.Filtro.ToUpper())
                                                  );

            return await PageList<Order>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Order GetOrderById(int orderId)
        {
             IQueryable<Order> query = _context.Orders;

                query = query.Include(d => d.OrderItems);

            query = query.AsNoTracking()
                         .OrderBy(o => o.Id)
                         .Where(order => order.Id == orderId);

            return query.FirstOrDefault();
        }

    }
}