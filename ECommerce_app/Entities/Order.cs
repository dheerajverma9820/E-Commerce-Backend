using ECommerce_app.Entities.User;

namespace ECommerce_app.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
