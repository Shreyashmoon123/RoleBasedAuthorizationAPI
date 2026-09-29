using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RoleBasedAuthorizationAPI.Models;
namespace RoleBasedAuthorizationAPI.Services;

public class TokenServices
{
    private readonly IConfiguration _configuration;

    public TokenServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(ApplicationUser user, string role)
    {
        var claim = new List<Claim>
        {
          new Claim(ClaimTypes.NameIdentifier,user.Id),
          new Claim(ClaimTypes.Email, user.Email),
          new Claim(ClaimTypes.Role,role )
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claim,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
