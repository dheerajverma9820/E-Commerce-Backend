using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Models;
using ECommerce_app.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using ECommerce_app.Entities;
using Microsoft.EntityFrameworkCore;
using ECommerce_app.Data;
using System.Net;

namespace ECommerce_app.Controllers
{
    [Route("category")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public ProductCategoryController(IProductCategoryService categoryService,ApplicationDbContext context)
        {
            _categoryService = categoryService;
            _context = context;
        }


        [HttpGet("all")]
        public async Task<ActionResult<BaseResponse<List<ProductCategory>>>> AllData()
        {
            try
            {
                var data = await _categoryService.GetAllAsync();
                if (data == null)
                {
                    return NotFound(BaseResponse<List<ProductCategory>>.ErrorResponse((int)HttpStatusCode.NotFound,"Product categories not found."));
                }
                return Ok(BaseResponse<List<ProductCategory>>.SuccessResponse((int)HttpStatusCode.OK, "Product categories",data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<List<ProductCategory>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error."));
            }
        }

        [HttpGet("{productCategoryId}")]
        public IActionResult GetProducts(int productCategoryId)
        {
            var products = _context.Products
                .Where(p => p.ProductCategoryId == productCategoryId)
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ProductImages = p.Images.Select(i => new ImageReponse
                    {
                        Id = i.Id,
                        imgName = i.ImgName,
                        imgURL = i.ImagePath
                    }).ToList()
                })
                .ToList();
            if (products == null)
            {
                return NotFound(new { status = 404, message = "Products not found" });
            }
            var responseData = new
            {
                status = 200,
                data = new
                {
                    productCategoryId = productCategoryId,
                    products = products
                },
                message = "Success"
            };
            return Ok(responseData);
        }
    }
}
