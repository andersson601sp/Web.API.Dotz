using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoUserDotz
{
    public interface IRepositoryUserDotz : IRepository
    {
       Task<PageList<UserDotz>> GetAllUserDotzAsync(PageParams pageParams);        
        UserDotz[] GetAllUserDotz();
        UserDotz GetUserDotzById(int userId);
    }
}