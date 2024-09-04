using ECommerce_app.Entities;

namespace ECommerce_app.Repositories.Abstract
{
    public interface IProductCategoryRepository
    {
        Task<ProductCategory> GetByIdAsync(int id);
        Task<List<ProductCategory>> GetAllAsync();
        Task AddAsync(ProductCategory category);
        Task<ProductCategory> GetByNameAsync(string name);
    }
}





// Task<ProductCategory> GetByIdProductWithCategoryAsync(int ProductCategoryId);

//  void Remove(ProductCategory category);
//  void RemoveRange(IEnumerable<ProductCategory> categories);

