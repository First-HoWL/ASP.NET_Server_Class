using ASP.NET_Server_Class.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.NET_Server_Class.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UsersDbContext _context;


        public TokenService(IConfiguration configuration, UsersDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public List<RefreshToken> GetAll() => _context.RefreshTokens.ToList();

        public (string AccessToken, string RefreshToken) GenerateTokens(User user)
        {
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();
            SaveRefreshToken(user, refreshToken);
            return (accessToken, refreshToken);
        }
        public RefreshToken? GetRefreshToken(string RefreshToken)
        {
            return _context.RefreshTokens.Where(i => i.Token == RefreshToken).FirstOrDefault();
        }

        public void SaveRefreshToken(User user, string RefreshToken)
        {
            var token = _context.RefreshTokens.FirstOrDefault(i => i.UserId == user.Id);

            if (token != null)
            {
                _context.RefreshTokens.Remove(token);
            }
                
            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = user.Id,
                Token = RefreshToken,
                ExpirationDate = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["TokenLifetime:RefreshToken"]))
            });
            _context.SaveChanges();
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"])
                );

            var credantials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256
                );

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["TokenLifetime:AccessToken"])),
                signingCredentials: credantials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string GenerateRefreshToken() {
            return Guid.NewGuid().ToString();
        }
    }
}
