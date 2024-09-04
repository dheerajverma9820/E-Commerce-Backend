using ECommerce_app.Data;
using ECommerce_app.Entities;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace ECommerce_app.Repositories.Concrete
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;

        public CartRepository(ApplicationDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public async Task<CartItem> AddToCart(CartItem item)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == item.ProductId && ci.ApplicationUserId == item.ApplicationUserId);
            CartItem newItem = null;
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                _context.CartItems.Update(existingItem);
                newItem = existingItem;
            }
            else
            {
                newItem = new CartItem
                {
                    Created_At = item.Created_At,
                    ApplicationUserId = item.ApplicationUserId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                await _context.CartItems.AddAsync(newItem);
            }
            await _context.SaveChangesAsync();
            return newItem;
        }
        //public async Task<CartItem> UpdateCart(CartItem item)
        //{
        //    var existingItem = await _context.CartItems
        //        .FirstOrDefaultAsync(ci => ci.ProductId == item.ProductId && ci.ApplicationUserId == item.ApplicationUserId);

        //    if (existingItem != null)
        //    {
        //       // existingItem.Quantity += item.Quantity;
        //        _context.CartItems.Update(existingItem);
        //        await _context.SaveChangesAsync();
        //        return existingItem;
        //    }
        //    return null; // Return null if the item was not found
        //}
        public async Task<CartItem> UpdateCart(CartItem item)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == item.ProductId && ci.ApplicationUserId == item.ApplicationUserId);
            if (existingItem != null)
            {
                existingItem.Quantity = item.Quantity;
                existingItem.Created_At = item.Created_At; // Assuming you want to update Created_At too
                _context.CartItems.Update(existingItem);
                await _context.SaveChangesAsync();
                return existingItem;
            }
            return null;
        }


        public async Task<bool> DeleteCartItemAsync(int itemId)
        {
            var data = await _context.CartItems.FindAsync(itemId);
            if (data == null)
            {
                return false; // Item not found
            }

            _context.CartItems.Remove(data);
            await _context.SaveChangesAsync(); // Await the asynchronous operation to save changes
            return true; // De
        }

        public async Task<List<CartReponse>> GetAllDataCartItem()
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product).Include(m=>m.Product.Images)
                .ToListAsync();
            var cartResponses = cartItems.Select(ci => new CartReponse
            {
                Id = ci.Id,
                Name = ci.Product.Name,
                imgURL = ci.Product.Images.FirstOrDefault().ImagePath,
                Price = ci.Product.Price,
                // Created_At = ci.CreatedAt, 
                Quantity = ci.Quantity
            }) .ToList();
            return cartResponses;
        }


        public async Task<List<CartReponse>> GetById(string userId)
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Product)
                .ThenInclude(p => p.Images)
                .Where(ci => ci.ApplicationUserId == userId.ToString())
                .ToListAsync();
            var cartResponses = cartItems.Select(ci => new CartReponse
            {
                Id = ci.Id,
                Name = ci.Product.Name,
                ProductId =ci.ProductId,
                imgURL = ci.Product.Images.FirstOrDefault().ImagePath,
                Price = ci.Product.Price,
                Created_At = ci.Created_At,
                Quantity = ci.Quantity,
                UserId = ci.ApplicationUserId
            }).ToList();
            return cartResponses;
        } 
    }
}
