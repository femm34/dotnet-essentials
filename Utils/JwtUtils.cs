using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MiApi.Utils;

public class JwtUtils
{
    
    private readonly IConfiguration _configuration;
    
    public JwtUtils(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateJwtToken(string username)
    {
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            // new Claim(ClaimTypes.Role, "Admin")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // El token expira en 30 minutos
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }
}