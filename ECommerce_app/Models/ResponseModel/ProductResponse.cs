namespace ECommerce_app.Models.ResponseModel
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime Created_At { get; set; }
        public string productCategoryName { get; set; }

        public string BrandName { get; set; }
        public int ProductCategoryId { get; set; } // Include ProductCategoryId
       // public ProductCategoryResponse ProductCategory { get; set; }
        public List<ImageReponse> ProductImages { get; set; }        

    }
}
