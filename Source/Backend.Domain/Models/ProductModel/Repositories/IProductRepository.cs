using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Models.ProductModel.Repositories
{
    public interface IProductRepository
    {
        Task<bool> ExistsById(string id);
        Task<bool> ExistsBySku(string sku);
        Task<Product> GetById(string id);
        Task<Product> GetBySku(string sku);
        Task<List<Product>> GetAll();
        Task<Product> Add(Product product);
    }
}
