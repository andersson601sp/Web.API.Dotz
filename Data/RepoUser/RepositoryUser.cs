using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Data;
using Web.API.Dotz.Data.RepoUser;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoUser
{
    public class RepositoryUser : IRepository, IRepositoryUser
    {
        private readonly DotzContext _context;

        public RepositoryUser(DotzContext context)
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

        public User[] GetAllUsers()
        {
             IQueryable<User> query = _context.Users;

             query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<User>> GetAllUsersAsync(PageParams pageParams)
        {
             IQueryable<User> query = _context.Users;

             query = query.Include(d => d.UserAddress);

            query = query.AsNoTracking().OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(pageParams.Filtro))
                query = query.Where(user => user.FirstName
                                                  .ToUpper()
                                                  .Contains(pageParams.Filtro.ToUpper()) ||
                                             user.LastName
                                                  .ToUpper()
                                                  .Contains(pageParams.Filtro.ToUpper()) ||
                                             user.Username
                                                  .ToUpper()
                                                  .Contains(pageParams.Filtro.ToUpper())
                                                  );

            return await PageList<User>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public User GetUserById(int userId)
        {
            IQueryable<User> query = _context.Users;

            query = query.Include(d => d.UserAddress);

            query = query.AsNoTracking()
                         .OrderBy(u => u.Id)
                         .Where(user => user.Id == userId);

            return query.FirstOrDefault();
        }
    }
}