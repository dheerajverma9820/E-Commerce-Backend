using ECommerce_app.Entities;

namespace ECommerce_app.Specification
{
    public class ProductWithCategorySpecification : BaseSpecification<Product>
    {
        public ProductWithCategorySpecification(int pageNumber, int pageSize)
            : base()
        {
            AddInclude(p => p.ProductCategory);
            ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
    }
}
