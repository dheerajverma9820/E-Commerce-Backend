using ECommerce_app.Entities;

namespace ECommerce_app.Services.Abstract
{
    public interface IProductCategoryService
    {
        Task<List<ProductCategory>> GetAllAsync();
    }
}
