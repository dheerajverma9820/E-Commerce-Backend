namespace ECommerce_app.Models.ResponseModel
{
    public class CartReponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public DateTime Created_At { get; set; }
        public string imgURL { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }
    }
}
