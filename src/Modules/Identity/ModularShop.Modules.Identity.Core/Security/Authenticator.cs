using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ModularShop.Modules.Identity.Core.Dtos;
using ModularShop.Modules.Identity.Core.Time;
using ModularShop.Shared.Abstractions.Options;

namespace ModularShop.Modules.Identity.Core.Security;

internal sealed class Authenticator(IOptions<AuthOptions> options, IClock clock) : IAuthenticator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();
    private readonly SigningCredentials _signingCredentials = new(new SymmetricSecurityKey( 
            Encoding.UTF8.GetBytes(options.Value.SigningKey)), 
        SecurityAlgorithms.HmacSha256);

    public JsonWebTokenDto CreateToken(Guid userId, string role, IDictionary<string, IEnumerable<string>> claims)
    {
        var now = clock.Current();
        var jwtClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };
        
        var customClaims = new List<Claim>();
        foreach (var (claim, values) in claims)
        {
            customClaims.AddRange(values.Select(value => new Claim(claim, value)));
        }
        jwtClaims.AddRange(customClaims);

        var expires = now.Add(options.Value.Expiry);
        var jwt = new JwtSecurityToken(options.Value.Issuer, options.Value.Audience, jwtClaims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JsonWebTokenDto
        {
            AccessToken = token
        };
    }
}