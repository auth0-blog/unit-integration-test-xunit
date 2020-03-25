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
              IssuerSigningKey = FakeJwtManager.SecurityKey,
              ValidIssuer = FakeJwtManager.Issuer,
              ValidAudience = FakeJwtManager.Audience
            };
          });
      });
    }
  }
}