namespace ECommerce_app.Models.RequestModel
{
    public class CartRequestModel
    {     
        public int ProductId { get; set; }
        public DateTime Created_At { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
