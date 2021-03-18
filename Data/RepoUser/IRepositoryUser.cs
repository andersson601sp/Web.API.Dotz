using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoUser
{
    public interface IRepositoryUser : IRepository
    {
       Task<PageList<User>> GetAllUsersAsync(PageParams pageParams);        
        User[] GetAllUsers();
        User GetUserById(int userId);
    }
}