using ECommerce_app.Entities;
using ECommerce_app.Repositories.Abstract;
using ECommerce_app.Services.Abstract;

namespace ECommerce_app.Services.Concrete
{
    public class ProductCategoryService: IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository) 
        {
            _productCategoryRepository = productCategoryRepository;
        }
        
        public Task<List<ProductCategory>> GetAllAsync()
        {
            return _productCategoryRepository.GetAllAsync();
        }

    }
}
