using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos;
using Application.Interfaces.Auth;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace Infrastructure.Service.Auth
{
    public class AuthService : IAuthService
    {
        private readonly JWTSettings _jWTSettings;
        public AuthService(IOptions<JWTSettings> options)
        {
            _jWTSettings = options.Value;
        }
        public string GenerateToken(UserDto model)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.SecretKey));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, model.Id.ToString()),
                new(ClaimTypes.MobilePhone, model.PhoneNumber),
                new(ClaimTypes.Email, model.Email)
            };
            var token = new JwtSecurityToken(_jWTSettings.Issuer, _jWTSettings.Audience, claims, null, DateTime.Now.AddMinutes(Convert.ToDouble(_jWTSettings.ExpiryInMinutes)), credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
