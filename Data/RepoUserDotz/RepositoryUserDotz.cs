using System.Linq;
using System.Threading.Tasks;
using Web.API.Dotz.Data;
using Web.API.Dotz.Data.RepoUser;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Dotz.Data.RepoUserDotz
{
    public class RepositoryUserDotz : IRepository, IRepositoryUserDotz
    {
        private readonly DotzContext _context;

        public RepositoryUserDotz(DotzContext context)
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

        public UserDotz[] GetAllUserDotz()
        {
             IQueryable<UserDotz> query = _context.UserDotz;

             query = query.AsNoTracking().OrderBy(a => a.Id);

            return query.ToArray();
        }

        public async Task<PageList<UserDotz>> GetAllUserDotzAsync(PageParams pageParams)
        {
             IQueryable<UserDotz> query = _context.UserDotz;

            query = query.AsNoTracking().OrderBy(a => a.Id);

           return await PageList<UserDotz>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public UserDotz GetUserDotzById(int userId)
        {
             IQueryable<UserDotz> query = _context.UserDotz;

            query = query.AsNoTracking()
                         .OrderBy(u => u.Id)
                         .Where(userDotz => userDotz.UserId == userId);

            return query.FirstOrDefault();
        }
    }
}