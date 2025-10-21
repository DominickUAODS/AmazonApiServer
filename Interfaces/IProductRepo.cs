using AmazonApiServer.Filters;
using AmazonApiServer.Models;

namespace AmazonApiServer.Interfaces
{
    public interface IProductRepo
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> EditAsync(Product product);
        Task<Product?> DeleteAsync(Guid productId);
        Task<List<Product>> GetAllAsync(ProductsFilter filter);
        Task<Product?> GetByIdAsync(Guid id);
    }
}
