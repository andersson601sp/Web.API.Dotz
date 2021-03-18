using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Data;
using Web.API.Dotz.Data.RepoUser;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoUserAddress
{
    public class RepositoryUserAddress : IRepository, IRepositoryUserAddress
    {
        private readonly DotzContext _context;

        public RepositoryUserAddress(DotzContext context)
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

        public UserAddress[] GetAllUsersAddress()
        {
            IQueryable<UserAddress> query = _context.UserAddress;

            query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<UserAddress>> GetAllUsersAddressAsync(PageParams pageParams)
        {
            IQueryable<UserAddress> query = _context.UserAddress;

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Filtro))
                query = query.Where(userAddress =>  userAddress.Street
                                                      .ToUpper()
                                                      .Contains(pageParams.Filtro.ToUpper()) ||
                                                    userAddress.District
                                                      .ToUpper()
                                                      .Contains(pageParams.Filtro.ToUpper()) ||
                                                    userAddress.City
                                                      .ToUpper()
                                                      .Contains(pageParams.Filtro.ToUpper()) ||
                                                    userAddress.State
                                                      .ToUpper()
                                                      .Contains(pageParams.Filtro.ToUpper()) ||
                                                    userAddress.ZipCode
                                                      .ToUpper()
                                                      .Contains(pageParams.Filtro.ToUpper())
                                                  );

            return await PageList<UserAddress>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public UserAddress GetUserById(int Id)
        {
            IQueryable<UserAddress> query = _context.UserAddress;

            query = query.AsNoTracking()
                         .OrderBy(u => u.Id)
                         .Where(userAddress => userAddress.Id == Id);

            return query.FirstOrDefault();
        }
    }
}