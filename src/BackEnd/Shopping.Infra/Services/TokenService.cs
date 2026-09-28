using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Shopping.Application.Configurations;
using Shopping.Application.Interfaces.Services;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Infra.Services;

internal sealed class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenService(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    public string GenerateAccessToken(User user, CancellationToken cancellationToken = default)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(GetClaimsOfUser(user)),
            Expires = DateTime.UtcNow.AddHours(JwtConfiguration.ExpiredTokenInHours)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    private IEnumerable<Claim> GetClaimsOfUser(User user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email.Address)
        };
        foreach (var role in user.Roles)
            claims.Add(new Claim(ClaimTypes.Role, role.Title));
        
        return claims;
    }
    public string GenerateRefreshToken()
    {
        var bytes = new byte[128];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}