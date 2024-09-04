namespace ECommerce_app.Entities
{
    public class Image
    {
        public int Id { get; set; }        
        public string ImagePath { get; set; }
        public int ProductId { get; set; }
        public string ImgName { get; set; }
        public Product Product { get; set; }
    }
}
