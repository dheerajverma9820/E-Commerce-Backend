using ECommerce_app.CommonRepository;
using ECommerce_app.Entities;
using ECommerce_app.Generic_Interface;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Specification;
using System.Collections.Generic;

namespace ECommerce_app.Repositories.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<BrandResponseModel>> GetBrand(int id);
        Task<List<BrandResponseModel>> GetBrandsAsync(int brandId, int categoryId);

        Task<string> UploadImage(ImageRequest imageRequest, List<IFormFile> file);
        Task<ProductCategory> FindProductCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync();
        Task<ProductResponse> GetProductWithCategoryAsync(int id);
        Task<ProductWithImageResponse> GetProductByIdAsync(int id);
        Task<List<ProductResponse>> GetAllDataAsync(List<int> categoryIds, List<int> brandIds, int? pageNumber, int? pageSize);
        IEnumerable<Product> FindWithSpecificationPattern(ISpecification<Product> specification);
        Task<int> GetProductStockAsync(int productId);
        Task<bool> UpdateInventoryAsync(InventoryUpdateRequest inventoryUpdateRequest);
        Task<List<ProductResponse>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<List<ProductResponse>> DiscountZone(int price);
        Task<List<Brand>> GetAllBrand();
    }
    
}





// Task<List<ProductResponse>> GetPaginatedProductsAsync(ProductWithCategorySpecification specification);


//  Task<ProductCategory> GetByIdProductWithCategoryAsync(int ProductCategoryId);



