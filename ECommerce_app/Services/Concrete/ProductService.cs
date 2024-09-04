using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using ECommerce_app.CommonRepository;
using ECommerce_app.Entities;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Repositories.Abstract;
using ECommerce_app.Services.Interfaces;
using ECommerce_app.Specification;

namespace ECommerce_app.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<bool> UpdateInventoryAsync(InventoryUpdateRequest inventoryUpdateRequest)
        {
            return _productRepository.UpdateInventoryAsync(inventoryUpdateRequest);
        }

        public Task<List<ProductResponse>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            return _productRepository.GetPaginatedProductsAsync(pageNumber, pageSize);
        }

        public Task<int> GetProductStockAsync(int productId)
        {
            return _productRepository.GetProductStockAsync(productId);
        }

        public async Task AddAsync(Product product)
        {
            await _productRepository.AddAsync(product);
        }

        public Task<ProductCategory> FindProductCategoryAsync(int categoryId)
        {
            return _productRepository.FindProductCategoryAsync(categoryId);
        }

        public IEnumerable<Product> FindWithSpecificationPattern(ISpecification<Product> specification)
        {
            return _productRepository.FindWithSpecificationPattern(specification);
            //throw new NotImplementedException();
        }
        public Task<List<ProductResponse>> GetAllDataAsync(List<int> categoryIds,List<int> brandIds,int? pageNumber,int? pageSize)
        {
            // Call the repository method with parameters in the correct order
            return _productRepository.GetAllDataAsync(
                categoryIds,
                brandIds,
                pageNumber,
                pageSize
            );
        }

        public Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return _productRepository.GetAllProductsWithCategoriesAsync();
        }

        public Task<ProductWithImageResponse> GetProductByIdAsync(int id)
        {
            return _productRepository.GetProductByIdAsync(id);
        }

        public Task<ProductResponse> GetProductWithCategoryAsync(int id)
        {
            return _productRepository.GetProductWithCategoryAsync(id);
        }

        public Task UploadImage(ImageRequest imageRequest, List<IFormFile> file)
        {
            return _productRepository.UploadImage(imageRequest, file);
        }

        public Task<List<ProductResponse>> DiscountZone(int price)
        {
            return _productRepository.DiscountZone(price);
        }

        public  Task<List<BrandResponseModel>> GetBrand(int id)
        {
            return _productRepository.GetBrand(id);
        }

        public Task<List<Brand>> GetAllBrand()
        {
            return _productRepository.GetAllBrand();
        }

        public Task<List<BrandResponseModel>> GetBrandsAsync(int brandId, int categoryId)
        {
            return _productRepository.GetBrandsAsync(brandId, categoryId);
        }
    }
}
//public async Task AddProductAsync(Product product)
//{
//    await _unitOfWork.Products.AddAsync(product);
//    await _unitOfWork.CompleteAsync();
//}

//public async Task<Product> GetProductByIdAsync(int id)
//{
//    return await _unitOfWork.Products.GetProductWithCategoriesAsync(id);
//}



//public async Task<IEnumerable<Product>> GetProductsAsync()
//{
//    return await _unitOfWork.Products.GetProductsWithCategoriesAsync();
//}

//public async Task<Product> GetProductByIdAsync(int id)
//{
//    return await _unitOfWork.Products.GetProductWithCategoriesAsync(id);
//}

//public async Task AddProductAsync(Product product)
//{
//    await _unitOfWork.Products.AddAsync(product);
//    await _unitOfWork.CompleteAsync();
//}

//public async Task DeleteProductAsync(int id)
//{
//    var product = await _unitOfWork.Products.GetAsync(id);
//    if (product != null)
//    {
//        _unitOfWork.Products.Remove(product);
//        await _unitOfWork.CompleteAsync();
//    }
//}


