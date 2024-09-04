using AutoMapper;
using ECommerce_app.Entities;
using ECommerce_app.Models;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;

using ECommerce_app.Services.Concrete;
using ECommerce_app.Services.Interfaces;
using ECommerce_app.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ECommerce_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(mapper, httpContextAccessor)
        {
            _productService = productService;



        }
        [HttpGet("all-brand")]
        public async Task<ActionResult<BaseResponse<List<Brand>>>> GetAllDataBrand()
        {
            try
            {
                // Assuming GetAllBrand is a method that returns a List<Brand>
                var data = await _productService.GetAllBrand();
                if (data == null)
                {
                    return NotFound(BaseResponse<List<Brand>>.ErrorResponse((int)HttpStatusCode.NotFound, "Brands not found."));
                }
                return Ok(BaseResponse<List<Brand>>.SuccessResponse((int)HttpStatusCode.OK, "Brands retrieved successfully.", data));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<List<ProductCategory>>.ErrorResponse(StatusCodes.Status500InternalServerError, "Internal server error."));
            }
        }
        [HttpGet("{id}/stock")]
        public async Task<IActionResult> GetProductStock(int id)
        {
            try
            {
                var stock = await _productService.GetProductStockAsync(id);
                var response = new BaseResponse<int>((int)HttpStatusCode.OK, "Stock retrieved successfully.", stock);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new BaseResponse<string>((int)HttpStatusCode.InternalServerError, $"Failed to retrieve product stock. {ex.Message}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("brand-Byid")]

        public async Task<ActionResult<List<BrandResponseModel>>> GetDataByBrandNameWithCategory([FromQuery] int brandId, int categoryId)
        {
            var data = await _productService.GetBrandsAsync(brandId, categoryId);
            if (data == null)
            {
                return NotFound(BaseResponse<List<BrandResponseModel>>.ErrorResponse(
                   (int)HttpStatusCode.NotFound, "No discounted products found."));
            }
            return Ok(BaseResponse<List<BrandResponseModel>>.SuccessResponse(
              (int)HttpStatusCode.OK, "Discounted products found", data));
        }


        [HttpGet("brand-id")]
      
        public async Task<ActionResult<List<BrandResponseModel>>> GetDataByBrandName([FromQuery] int id)
        {
            var data = await _productService.GetBrand(id);
               if (data== null)
                {
                    return NotFound(BaseResponse<List<BrandResponseModel>>.ErrorResponse(
                       (int)HttpStatusCode.NotFound, "No discounted products found."));
                }
               return Ok(BaseResponse<List<BrandResponseModel>>.SuccessResponse(
                 (int)HttpStatusCode.OK, "Discounted products found",data)); 
        }

        //[HttpPatch("inventory")]
        //public async Task<IActionResult> UpdateInventory([FromBody] InventoryUpdateRequest request)
        //{
        //    try
        //    {
        //        var success = await _productService.UpdateInventoryAsync(request);

        //        if (success)
        //        {
        //            return Ok(new ResponseMessage<string>().SetSuccessResponse("Inventory updated successfully."));
        //        }
        //        else
        //        {
        //            return NotFound(new ResponseMessage<string>().SetErrorResponse($"Product with ID {request.ProductId} not found."));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage<string>().SetErrorResponse($"Failed to update inventory. {ex.Message}"));
        //    }
        //}


        [HttpPost("create")]
        public async Task<ActionResult<BaseResponse<Product>>> AddProduct([FromBody] ProductRequest productRequest)
        {
            var productCategory = await _productService.FindProductCategoryAsync(productRequest.ProductCategoryId);
            if (productCategory == null)
            {
                //  return NotFound(BaseResponse<Product><int>(int)(HttpStatusCode.NotFound, "Product category not found."));
                return NotFound(BaseResponse<Product>.ErrorResponse((int)HttpStatusCode.NotFound, "Product category not found."));
            }
            var product = new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Created_At = DateTime.Now,
                ProductCategoryId = productCategory.Id
            };
            try
            {
                await _productService.AddAsync(product);
                return CreatedAtAction(nameof(GetProductWithCategory), new { id = product.Id }, BaseResponse<Product>.SuccessResponse(StatusCodes.Status201Created, "Product created successfully.", product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<Product>.ErrorResponse(StatusCodes.Status500InternalServerError, $"Failed to add product. {ex.Message}"));
            }
        }





        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ProductResponse>>> GetProductWithCategory(int id)
        {
            var product = await _productService.GetProductWithCategoryAsync(id);
            if (product == null)
            {
                return NotFound(BaseResponse<ProductResponse>.ErrorResponse((int)HttpStatusCode.NotFound, "Product not found."));
            }
            var productResponse = _mapper.Map<ProductResponse>(product);
            return Ok(BaseResponse<ProductResponse>.SuccessResponse((int)HttpStatusCode.OK, "Product found.", productResponse));
        }


            [HttpGet("product-bypage")]
            public async Task<IActionResult> GetPaginatedProducts([FromQuery] int pageNumber, [FromQuery] int pageSize)
              {
                 try
                    {
                     var products = await _productService.GetPaginatedProductsAsync(pageNumber, pageSize);
                     return Ok(BaseResponse<List<ProductResponse>>.SuccessResponse((int)HttpStatusCode.OK, "Product found.", products));
                    }
                 catch (Exception ex)
                   {
                   return StatusCode(StatusCodes.Status500InternalServerError, BaseResponse<List<ProductResponse>>.ErrorResponse(StatusCodes.Status500InternalServerError, $"Failed to retrieve products. {ex.Message}"));
                   }
            
              }


            [HttpGet("Get-ById")]
            // [HttpGet("{id}/product")]
            public async Task<ActionResult<BaseResponse<ProductWithImageResponse>>> GetData(int id)
             {
                 var data = await _productService.GetProductByIdAsync(id);
                    if (data == null)
                     {
                       return NotFound(BaseResponse<ProductResponse>.ErrorResponse((int)HttpStatusCode.NotFound, "Product not found."));
                     }
                       return Ok(BaseResponse<ProductWithImageResponse>.SuccessResponse((int)HttpStatusCode.OK, "Product found.", data));
               }



        //////   [Authorize("role=Admin")]
        [HttpPost("upload-image")]
        public async Task<ActionResult<BaseResponse<string>>> UploadImage([FromForm] ImageRequest imageRequest, List<IFormFile> files)
        {
            if (!ModelState.IsValid || files == null || files.Count == 0)
            {
                return BadRequest(BaseResponse<string>.ErrorResponse((int)HttpStatusCode.BadRequest, "Invalid image data."));
            }
            try
            {
                await _productService.UploadImage(imageRequest, files);
                return Ok(BaseResponse<string>.SuccessResponse((int)HttpStatusCode.OK,"Image uploaded successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,BaseResponse<string>.ErrorResponse(StatusCodes.Status500InternalServerError,$"Failed to upload image. {ex.Message}"));  
            }
        }


        //   // [Authorize("role=User")]
        [HttpGet("all")]
        public async Task<ActionResult<BaseResponse<List<ProductResponse>>>> AllData([FromQuery] List<int>? CategoryIds, [FromQuery]List<int>? BrandIds, int? PageNumber, int? PageSize)
        {
            try
            {

                // Fetch data from the service

                var data = await _productService.GetAllDataAsync(CategoryIds, BrandIds, PageNumber, PageSize);

               // Check if data is null or empty and return appropriate response
                 if (data == null || !data.Any())
                {
                    return NotFound(BaseResponse<List<ProductResponse>>.ErrorResponse(
                        (int)HttpStatusCode.NotFound,
                        "Products not found."
                    ));
                }

                return Ok(BaseResponse<List<ProductResponse>>.SuccessResponse(
                    (int)HttpStatusCode.OK,
                    "Products found",
                    data
                ));
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                return StatusCode((int)HttpStatusCode.InternalServerError, BaseResponse<List<ProductResponse>>.ErrorResponse(
                    (int)HttpStatusCode.InternalServerError,
                    $"An unexpected error occurred: {ex.Message}"
                ));
            }
        }
           
        


        [HttpGet("discount")]
        public async Task<ActionResult<BaseResponse<List<ProductResponse>>>> Discount(int price)
        {
            var data = await _productService.DiscountZone(price);
            if (data == null)
            {
                return NotFound(BaseResponse<List<ProductResponse>>.ErrorResponse(
                    (int)HttpStatusCode.NotFound, "No discounted products found."));
            }
            return Ok(BaseResponse<List<ProductResponse>>.SuccessResponse(
                (int)HttpStatusCode.OK, "Discounted products found", data));
        }
    }

}










//[HttpGet("all")]
//public async Task<IActionResult> UserResponse(int pageNumber, int pageSize)
//{
//    var userSpecification = new ProductWithCategorySpecification(pageNumber, pageSize);
//    var users = _productService.FindWithSpecificationPattern(userSpecification);



//}

//// [Authorize("role=Admin")]

//[HttpPost("upload")]
//public async Task<IActionResult> UploadImage([FromForm] ImageRequest imageRequest, IFormFile file)
//{
//    if (file == null || file.Length == 0)
//    {
//        return BadRequest("No file uploaded");
//    }

//    try
//    {
//        await _imageService.AddAsync(file);
//        return Ok("Image uploaded successfully");
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to upload image: {ex.Message}");
//    }
//}

//[HttpGet("product/{id}")]
//public async Task<IActionResult> GetProductWithImage(int id)
//{
//    var productWithImage = await _imageService.GetProductWithImageAsync(id);
//    if (productWithImage == null)
//    {
//        return NotFound();
//    }

//    return Ok(productWithImage); // Assuming productWithImage is a DTO or model
//}

//[HttpGet("products")]
//public async Task<IActionResult> GetAllProductsWithImages()
//{
//    var productsWithImages = await _imageService.GetAllProductsWithImageAsync();
//    return Ok(productsWithImages); // Assuming productsWithImages is a collection of DTOs or models
//}



//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetProductById(int id)
//        {
//            var product = await _productService.GetProductWithCategoryAsync(id);

//            if (product == null)
//            {
//                return NotFound();
//            }

//            var productResponse = _mapper.Map<ProductResponse>(product);

//            return Ok(productResponse);
//        }
//    }
//}
//    [HttpPost("create")]
//    public async Task<ActionResult<Product>> CreateProduct(ProductRequest request)
//    {
//        var productCategory = await _productService.FindOrCreateProductCategoryAsync(request.CategoryName);

//        var product = new Product
//        {
//            Name = request.Name,
//            Price = request.Price,
//            ProductCategory = productCategory
//        };

//        await _productService.AddProductAsync(product);

//        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
//    }

//    [HttpGet("id")]
//    public async Task<ActionResult<Product>> GetProduct(int id)
//    {
//        var product = await _productService.GetProductByIdAsync(id);

//        if (product == null)
//        {
//            return NotFound();
//        }

//        return Ok(product);
//    }

//}
//}

