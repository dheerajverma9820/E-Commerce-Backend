using ECommerce_app.Data;
using ECommerce_app.Entities;
using ECommerce_app.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_app.Repositories.Concrete
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {


        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductCategory> GetByIdAsync(int id)
        {
            return await _context.ProductCategorys.FindAsync(id);
        }

        public async Task<List<ProductCategory>> GetAllAsync()
        {
            return await _context.ProductCategorys.ToListAsync();
        }

        public async Task<ProductCategory> GetByNameAsync(string name)
        {
            return await _context.ProductCategorys.FirstOrDefaultAsync(pc => pc.CategoryName == name);
        }

        public async Task AddAsync(ProductCategory category)
        {
            await _context.ProductCategorys.AddAsync(category);
        }

        public void Remove(ProductCategory category)
        {
            _context.ProductCategorys.Remove(category);
        }

        public void RemoveRange(IEnumerable<ProductCategory> categories)
        {
            _context.ProductCategorys.RemoveRange(categories);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        } 
    }
}