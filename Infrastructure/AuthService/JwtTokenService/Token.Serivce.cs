using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankApplication.Infrastructure.AuthService.JwtTokenService;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:TokenKey"]));
        _issuer = config["Jwt:Issuer"];
    }

    public string CreateToken(User user)
    {
        var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString() ),
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.Surname, user.Surname),
            new (ClaimTypes.Email, user.Email),
            
        };
        var token = new JwtSecurityToken(_issuer,
            _issuer,
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: cred);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public JwtSecurityToken Verify(string jwt)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(jwt, new TokenValidationParameters()
        {
            IssuerSigningKey = _key,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateLifetime = false
        }, out SecurityToken securityToken);
        return (JwtSecurityToken) securityToken;
    }
}