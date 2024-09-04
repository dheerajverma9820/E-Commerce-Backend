using System.ComponentModel.DataAnnotations;

namespace ECommerce_app.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Created_At { get; set; }
        public int ProductCategoryId { get; set; }

        public int BrandId { get; set; }
        public IEnumerable<Image> Images { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
        public Brand Brand { get; set; }

        public Product()
        {
            Inventories = new List<Inventory>();
        }
    }
}
