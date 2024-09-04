namespace ECommerce_app.Models.RequestModel.AuthRequestModels
{
    public class UserAddressRequestModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public string State { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
