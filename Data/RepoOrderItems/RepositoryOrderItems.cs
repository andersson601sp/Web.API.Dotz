using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoOrderItems
{
    public class RepositoryOrderItems : IRepository, IRepositoryOrderItems
    {
        private readonly DotzContext _context;

        public RepositoryOrderItems(DotzContext context)
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

        public OrderItems[] GetAllOrderItems()
        {
             IQueryable<OrderItems> query = _context.OrderItems;

             query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<OrderItems>> GetAllOrderItemsAsync(PageParams pageParams)
        {
             IQueryable<OrderItems> query = _context.OrderItems;

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return await PageList<OrderItems>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
    }
}