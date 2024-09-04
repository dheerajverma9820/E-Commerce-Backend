using ECommerce_app.Entities;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Repositories.Abstract;
using ECommerce_app.Repositories.Concrete;
using ECommerce_app.Services.Abstract;

namespace ECommerce_app.Services.Concrete
{
    public class CartService: ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }

        public Task<CartItem> AddToCart(CartItem item)
        {
            return _cartRepository.AddToCart(item);
        }

        public Task<bool> DeleteCartItemAsync(int itemId)
        {
            return _cartRepository.DeleteCartItemAsync(itemId);
        }

        public Task<List<CartReponse>> GetAllDataCartItem()
        {
            return _cartRepository.GetAllDataCartItem();
        }

        public Task<List<CartReponse>> GetById(string userId)
        {
            return _cartRepository.GetById(userId); 
        }

        public Task<CartItem> UpdateCart(CartItem item)
        {
            return _cartRepository.UpdateCart(item);
        }
    }
}
