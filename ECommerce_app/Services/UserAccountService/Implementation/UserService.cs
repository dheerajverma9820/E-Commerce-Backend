using ECommerce_app.Entities;
using ECommerce_app.Models;
using ECommerce_app.Models.RequestModel.AuthRequestModels;
using ECommerce_app.Models.RequestModel.UserRequestModels;
using ECommerce_app.Models.ResponseModel.AuthResponseModels;
using ECommerce_app.Repositories.UserAccountRepository.Implementation;
using ECommerce_app.Repositories.UserAccountRepository.Interface;
using ECommerce_app.Services.UserAccountService.Interface;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_app.Services.UserAccountService.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public Task<UserResponseModel> GetUserData(string id)
        {
            return _userRepository.GetUserData(id);   
        }
        public Task<IdentityResult> SignUp(UserRequestModel userRequest)
        {
            return _userRepository.SignUp(userRequest);
        }

        public Task<UserAddressNew> UserAddress(UserAddressRequestModel addressRequestModel)
        {
            return _userRepository.UserAddress(addressRequestModel);
        }

        public Task<string> UserLogin(LoginRequestModel loginRequest)
        {
           return _userRepository.UserLogin(loginRequest);
        }
    }
}
