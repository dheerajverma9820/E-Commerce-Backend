using ECommerce_app.Entities;
using ECommerce_app.Models.ResponseModel;

namespace ECommerce_app.Repositories.Abstract
{
    public interface ICartRepository
    {
        Task<CartItem> AddToCart(CartItem item);
        Task<List<CartReponse>> GetAllDataCartItem();
        Task<bool> DeleteCartItemAsync(int itemId);
        Task<List<CartReponse>> GetById(string userId);
        Task<CartItem> UpdateCart(CartItem item);

    }
}
