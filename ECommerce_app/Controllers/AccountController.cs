using AutoMapper;
using ECommerce_app.Data;
using ECommerce_app.Models;
using ECommerce_app.Models.RequestModel.AuthRequestModels;
using ECommerce_app.Models.RequestModel.UserRequestModels;
using ECommerce_app.Models.ResponseModel.AuthResponseModels;
using ECommerce_app.Services.UserAccountService.Implementation;
using ECommerce_app.Services.UserAccountService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Reflection;
using System.Security.Claims;

namespace ECommerce_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IMapper mapper , IHttpContextAccessor httpContextAccessor)
            : base(mapper, httpContextAccessor)
        {
            _userService = userService;
        }

     
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return BadRequest("User ID not found");
            }
            var userData = await _userService.GetUserData(UserId);
            if (userData == null)
            {
                return NotFound("User data not found.");
            }
            return Ok(userData);
        }


        [HttpPost("Sign-Up")]
        public async Task<IActionResult> SignUp([FromBody] UserRequestModel userRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.SignUp(userRequest);
                if (result.Succeeded)
                {
                    return Ok("User created successfully.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestModel loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.UserLogin(loginRequest);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid login credentials");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("address")]
        public async Task<IActionResult> UserAddress([FromBody] UserAddressRequestModel addressRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.UserAddress(addressRequestModel);
                if (result == null)
                {
                    return Ok("User  user address is not created successfully.");
                }
                else
                {

                    return Ok("User address created successfully.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        //[HttpGet("me")]
        //[Authorize]
        //public async Task<IActionResult> GetUserData()
        //{
        //    var userId = httpContext.User.FindFirstValue(ClaimTypes.Email);
        //    var userData = await _userService.GetUserData(HttpContext);
        //    if (userData == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(userData);
        //}







        //[Authorize]
        //[HttpGet("me")]
        //public async Task<IActionResult> UserVerify()
        //{
        //    try
        //    {
        //        var userId = User.Claims.ToList()[2]?.Value;
        //        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        //        var nameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var firstName = User.FindFirst("first_name")?.Value;
        //        var lastName = User.FindFirst("last_name")?.Value;
        //        var gender = User.FindFirst(ClaimTypes.Gender)?.Value;

        //        if (string.IsNullOrEmpty(nameIdentifier))
        //        {
        //            return Unauthorized(new { message = "User not valid." });
        //        }

        //        var response = new
        //        {
        //            UserId = userId,
        //            Email = email,
        //            Gender = gender,
        //            FirstName = firstName,
        //            LastName = lastName,
        //        };

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception using a logging framework like Serilog or NLog
        //        // For example: _logger.LogError(ex, "Exception in UserVerify method");
        //        return StatusCode(500, new { message = "Internal Server Error" });
        //    }
    }
}



               //        var user = await _context.Users
               //        .FirstOrDefaultAsync(u => u.Id == userId);
               //    var userResponse = new UserResponseModel
               //    {
               //        Email = user.Email,
               //        Gender = user.Gender,
               //        Name = user.UserName
               //    };

//    return Ok(userResponse);
//}
//catch (Exception ex)
//{
//    return StatusCode(500, $"Internal server error: {ex.Message}");
//}



//[HttpGet("me")]
//public async Task<IActionResult> profile()
//{
//    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//    var data = User.FindFirstValue(ClaimTypes.Gender);
//}






