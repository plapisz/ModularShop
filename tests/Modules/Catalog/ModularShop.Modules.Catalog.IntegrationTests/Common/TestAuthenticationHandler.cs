using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ModularShop.Modules.Catalog.IntegrationTests.Common;

internal sealed class TestAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    public const string SchemeName = "Test";
    public const string ClaimsHeader = "Test-Claims";
    public const string UnauthenticatedHeader = "Test-Unauthenticated";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Request.Headers.ContainsKey(UnauthenticatedHeader))
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        var identity = new ClaimsIdentity(GetClaims(), SchemeName);
        var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), SchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }

    private List<Claim> GetClaims()
    {
        var policiesHeader = Request.Headers[ClaimsHeader].ToString();
        
        return !string.IsNullOrEmpty(policiesHeader)
            ? policiesHeader.Split(",").Select(p => new Claim("permissions", p)).ToList()
            : [];
    }
}