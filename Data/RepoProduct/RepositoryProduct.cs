using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Data.RepoProduct;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoProduct
{
    public class RepositoryProduct : IRepository, IRepositoryProduct
    {
        private readonly DotzContext _context;

        public RepositoryProduct(DotzContext context)
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

        public Product[] GetAllProducts()
        {
             IQueryable<Product> query = _context.Products;

             query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<Product>> GetAllProductsAsync(PageParams pageParams)
        {
             IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Filtro))
                query = query.Where(product => product.Description
                                                  .ToUpper()
                                                  .Contains(pageParams.Filtro.ToUpper())
                                                  );

            return await PageList<Product>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Product GetProductById(int productId)
        {
             IQueryable<Product> query = _context.Products;

            query = query.AsNoTracking()
                         .OrderBy(p => p.Id)
                         .Where(product => product.Id == productId);

            return query.FirstOrDefault();
        }
    }
}