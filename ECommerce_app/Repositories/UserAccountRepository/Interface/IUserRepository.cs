using ECommerce_app.Entities;
using ECommerce_app.Models.RequestModel.AuthRequestModels;
using ECommerce_app.Models.RequestModel.UserRequestModels;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Models.ResponseModel.AuthResponseModels;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_app.Repositories.UserAccountRepository.Interface
{
    public interface IUserRepository
    {
        Task<UserResponseModel> GetUserData(string id);
        Task<IdentityResult> SignUp(UserRequestModel userRequest);
        Task<string> UserLogin(LoginRequestModel loginRequest);
        Task<UserAddressNew> UserAddress(UserAddressRequestModel addressRequestModel);
    }
}
