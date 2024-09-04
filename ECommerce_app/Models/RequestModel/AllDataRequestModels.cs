namespace ECommerce_app.Models.RequestModel
{
    public class AllDataRequestModels
    {
        public List<int> ? CategoryIds { get; set; }
        public List<int>?  BrandIds { get; set; }
        public int ? PageNumber { get; set; }
        public int ? PageSize { get; set; }

    }
}
