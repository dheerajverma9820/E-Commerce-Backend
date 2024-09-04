namespace ECommerce_app.Models.ResponseModel.AuthResponseModels
{
    public class UserResponseModel
    {
        public string userId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int CartCount { get; set; }
    }
}
