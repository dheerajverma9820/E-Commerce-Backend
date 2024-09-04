using ECommerce_app.Entities.User;

namespace ECommerce_app.Models.ResponseModel.AuthResponseModels
{
    public class UserAddressResponseModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
    
    }
}
