using ECommerce_app.Entities;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Services.Abstract;
using ECommerce_app.Services.Concrete;
using Microsoft.AspNetCore.Mvc;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;

namespace ECommerce_app.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<BaseResponse<List<CartReponse>>>> AllData()
        {
            var data = await _cartService.GetAllDataCartItem();
            if (data == null || data.Count == 0)
            {
                return NotFound(new BaseResponse<List<CartReponse>>((int)HttpStatusCode.NotFound, "Products not found.", null));
            }
            return Ok(new BaseResponse<List<CartReponse>>((int)HttpStatusCode.OK,"Data retrieved successfully.",data));
        }


        [HttpPost("add")]
        public async Task<ActionResult<BaseResponse<CartItem>>> AddCart([FromBody] CartRequestModel cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest(new BaseResponse<CartItem>(400, "Cart item is not provided.", null));
            }
            var item = new CartItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Created_At = cartItem.Created_At,
                ApplicationUserId = cartItem.UserId,
                // Map other properties if needed
            };
            var addedItem = await _cartService.AddToCart(item);
            if (addedItem != null)
            {
                var response = new BaseResponse<CartItem>(200, "Item added to cart successfully.", addedItem);
                return Ok(response);
            }
            else
            {
                return BadRequest(new BaseResponse<CartItem>(400, "Failed to add item to cart.", null));
            }

        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCartItem(int id)
        {
            var result = await _cartService.DeleteCartItemAsync(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound("errorResponse");
            }
        }


        
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var data = await _cartService.GetById(userId);
            if (data == null || !data.Any())
            {
                var notFoundResponse = new BaseResponse<List<CartReponse>>(404, "No cart items found for this user.", null);
                return StatusCode(404, notFoundResponse);
            }
            var successResponse = new BaseResponse<List<CartReponse>>(200, "Cart items retrieved successfully.", data);
            return StatusCode(200, successResponse);
        }


        [HttpPut("update")]
        public async Task<CartItem> UpdateCart(CartRequestModel cartRequest)
        {
            var item = new CartItem
            {
                ProductId = cartRequest.ProductId,
                Quantity = cartRequest.Quantity,
                Created_At = cartRequest.Created_At,
                ApplicationUserId = cartRequest.UserId
            };
            return await _cartService.UpdateCart(item);
        }
    }
}








//[HttpPut("update")]
//public async Task<ActionResult<BaseResponse<CartItem>>> UpdateCart([FromBody] CartRequestModel cartRequest)
//{
//    if (cartRequest == null)
//    {
//        return BadRequest(new BaseResponse<CartItem>().SetErrorResponse("Cart item data is not provided."));
//    }

//    var item = new CartItem
//    {
//        ProductId = cartRequest.ProductId,
//        Quantity = cartRequest.Quantity,
//        Created_At = cartRequest.Created_At,
//        ApplicationUserId = cartRequest.UserId
//    };

//    var updatedItem = await _cartService.UpdateCart(item);

//    if (updatedItem != null)
//    {
//        return Ok(new BaseResponse<CartItem>().SetSuccessResponse(updatedItem));
//    }
//    else
//    {
//        return BadRequest(new BaseResponse<CartItem>().SetErrorResponse("Failed to update item in cart."));
//    }

//}








