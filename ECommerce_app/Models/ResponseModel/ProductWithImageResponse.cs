namespace ECommerce_app.Models.ResponseModel
{
    public class ProductWithImageResponse
    {
        public ProductResponse ProductResponse { get; set; }
        public List<ImageReponse> ImageReponse { get; set; }
    }
}
