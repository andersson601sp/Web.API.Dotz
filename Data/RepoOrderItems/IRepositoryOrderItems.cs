using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoOrderItems
{
    public interface IRepositoryOrderItems : IRepository
    {
       Task<PageList<OrderItems>> GetAllOrderItemsAsync(PageParams pageParams);        
        OrderItems[] GetAllOrderItems();
    }
}