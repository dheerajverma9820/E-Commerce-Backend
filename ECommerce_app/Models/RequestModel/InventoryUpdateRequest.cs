namespace ECommerce_app.Models.RequestModel
{
    public class InventoryUpdateRequest
    {
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
    }
}
