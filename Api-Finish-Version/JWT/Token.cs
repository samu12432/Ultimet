using Api_Finish_Version.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_REST_PROYECT.JWT
{
    public class Token
    {
        private readonly string _secret;

        public Token(IConfiguration configuration)
        {
            _secret = configuration["JwtSettings:Secret"]!;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.userEmail),
                    new Claim(ClaimTypes.Name, user.userName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10), //Validez del Token por 10 min
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
