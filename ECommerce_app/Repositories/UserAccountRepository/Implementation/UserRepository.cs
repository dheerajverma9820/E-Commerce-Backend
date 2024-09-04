using ECommerce_app.Data;
using ECommerce_app.Entities;
using ECommerce_app.Entities.User;
using ECommerce_app.Models.RequestModel.AuthRequestModels;
using ECommerce_app.Models.RequestModel.UserRequestModels;
using ECommerce_app.Models.ResponseModel.AuthResponseModels;
using ECommerce_app.Repositories.UserAccountRepository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Claims;

namespace ECommerce_app.Repositories.UserAccountRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly JwtToken _tokenUtil;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtToken tokenUtil , ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenUtil = tokenUtil;
            _context = context;
        }

        public async Task<UserAddressNew> UserAddress(UserAddressRequestModel addressRequestModel)
        {
            try
            {
                var userAddress = new UserAddressNew
                {
                    FirstName = addressRequestModel.FirstName,
                    LastName = addressRequestModel.LastName,
                    Address = addressRequestModel.Address,
                    City = addressRequestModel.City,
                    PinCode = addressRequestModel.PinCode,
                    State = addressRequestModel.State,
                    PhoneNumber = addressRequestModel.PhoneNumber,
                    Country = addressRequestModel.Country,
                    ApplicationUserId = addressRequestModel.ApplicationUserId
                };
                _context.userAddressNew.Add(userAddress);
                await _context.SaveChangesAsync();
                return userAddress;
            } 
            catch (Exception ex)
            {

                throw;
            }     
        }
    

        public async Task<IdentityResult>SignUp(UserRequestModel userRequest)

        {
            var user = new ApplicationUser
            {
                DisplayName = userRequest.DisplayName,
               // LastName = userRequest.LastName,
                UserName = userRequest.Email,
                Email = userRequest.Email,
                Gender = userRequest.Gender,
                SecurityStamp = Guid.NewGuid().ToString()
            };
           var result = await _userManager.CreateAsync(user, userRequest.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");               
            }
             return result;          
        }


        public async Task<string> UserLogin(LoginRequestModel loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                return ("User not found");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, true);
            if (result.Succeeded)
            {
                var token = _tokenUtil.GenerateJwtToken(user);
                return (token);
            }
            else
            {
                return ("Invalid credentials");
            }
        }



        public async Task<UserResponseModel?> GetUserData(string id)
        {          
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }
            var cartCount = await _context.CartItems
                .CountAsync(c => c.ApplicationUserId == id);
            var userResponse = new UserResponseModel
            {
                userId = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Gender = user.Gender,
                CartCount = cartCount
            };
            return userResponse;
        }
    }
}