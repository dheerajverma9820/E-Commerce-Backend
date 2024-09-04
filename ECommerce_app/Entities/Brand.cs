using System.ComponentModel.DataAnnotations;

namespace ECommerce_app.Entities
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        public string BrandName { get; set; }
    }
}
