using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Glossary.IntegrationTests
{
  public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureTestServices(services =>
      {
        services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
              {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
              IssuerSigningKey = FakeJwtTokensManager.SecurityKey,
              ValidIssuer = FakeJwtTokensManager.Issuer,
              ValidAudience = FakeJwtTokensManager.Audience
            };
          });
      });
    }
  }
}