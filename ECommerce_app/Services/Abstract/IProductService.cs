using ECommerce_app.CommonRepository;
using ECommerce_app.Entities;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Specification;
using System.Collections.Generic;

namespace ECommerce_app.Services.Interfaces
{
    public interface IProductService
    {
        Task AddAsync(Product product);
        Task<ProductCategory> FindProductCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync();
        Task<ProductResponse> GetProductWithCategoryAsync(int id);
        Task UploadImage(ImageRequest imageRequest, List<IFormFile> file);
        Task<ProductWithImageResponse> GetProductByIdAsync(int id);
        Task<List<ProductResponse>> GetAllDataAsync(List<int> categoryIds, List<int> brandIds, int? pageNumber, int? pageSize);
        IEnumerable<Product> FindWithSpecificationPattern(ISpecification<Product> specification);
        Task<List<ProductResponse>> GetPaginatedProductsAsync(int pageNumber, int pageSize);
        Task<int> GetProductStockAsync(int productId);
        Task<bool> UpdateInventoryAsync(InventoryUpdateRequest inventoryUpdateRequest);
        Task<List<ProductResponse>> DiscountZone(int price);
        Task<List<BrandResponseModel>> GetBrand(int id);
        Task<List<Brand>> GetAllBrand();

        Task<List<BrandResponseModel>> GetBrandsAsync(int brandId, int categoryId);






    }
}
