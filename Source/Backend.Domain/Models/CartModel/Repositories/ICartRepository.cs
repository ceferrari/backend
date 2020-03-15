using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Domain.Models.CartModel.Repositories
{
    public interface ICartRepository
    {
        Task<bool> ExistsById(string id);
        Task<bool> ExistsByCustomerId(string id);
        Task<Cart> GetById(string id);
        Task<Cart> GetByCustomerId(string customerId);
        Task<List<Cart>> GetAll();
        Task<Cart> Add(Cart cart);
        Task<Cart> Update(Cart cart);
        Task<bool> Delete(string id);
        Task<bool> DeleteItem(string id, string itemId);
        Task<bool> Checkout(string id);
    }
}
