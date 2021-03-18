using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoOrder
{
    public interface IRepositoryOrder : IRepository
    {
       Task<PageList<Order>> GetAllOrdersAsync(PageParams pageParams);        
        Order[] GetAllOrders();
        Order GetOrderById(int orderId);
    }
}