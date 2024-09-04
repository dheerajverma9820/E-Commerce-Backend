namespace ECommerce_app.Models.RequestModel
{
    public class ProductRequest
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Created_At { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
