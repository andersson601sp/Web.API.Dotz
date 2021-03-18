using System.Threading.Tasks;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data.RepoProduct
{
    public interface IRepositoryProduct : IRepository
    {
       Task<PageList<Product>> GetAllProductsAsync(PageParams pageParams);        
        Product[] GetAllProducts();
        Product GetProductById(int productId);
    }
}