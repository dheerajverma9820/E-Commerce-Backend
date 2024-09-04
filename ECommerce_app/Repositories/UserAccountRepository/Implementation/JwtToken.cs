using ECommerce_app.Entities.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce_app.Repositories.UserAccountRepository.Implementation
{
    public class JwtToken
    {
        private readonly IConfiguration _configuration;

        public JwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var ss = _configuration["Jwt:SecretKey"];
            var claims = new[]
            {      
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("DisplayName", user.DisplayName),
               // new Claim("last_name", user.LastName),
                new Claim(ClaimTypes.Email, user.Email), // Assuming FirstName is a property of ApplicationUser
                new Claim(ClaimTypes.Gender, user.Gender),
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
