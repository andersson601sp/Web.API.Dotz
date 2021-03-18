using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoUserAddress
{
    public interface IRepositoryUserAddress : IRepository
    {
       Task<PageList<UserAddress>> GetAllUsersAddressAsync(PageParams pageParams);        
        UserAddress[] GetAllUsersAddress();
        UserAddress GetUserById(int Id);
    }
}