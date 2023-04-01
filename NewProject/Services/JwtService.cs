using Microsoft.IdentityModel.Tokens;
using NewProject.Database;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NewProject.Services
{
    public class JwtService
    {
        private readonly string _secret = "q#QFT$ekJ$jrhg7b9?Ei45kHnKLgzPz6eMa9er3S";
        private DataContext _dbContext;

        public JwtService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Generate(string userId)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(userId, null, null, null, DateTime.Today.AddDays(7));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}