using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Glossary.IntegrationTests
{
  public class IntegrationTests : IClassFixture<CustomWebApplicationFactory<Glossary.Startup>>
  {
    private readonly HttpClient httpClient;
    public IntegrationTests(CustomWebApplicationFactory<Glossary.Startup> factory)
    {
      httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetGlossaryList()
    {
      // Act
      var response = await httpClient.GetAsync("api/glossary");

      // Assert
      response.EnsureSuccessStatusCode();
      var stringResponse = await response.Content.ReadAsStringAsync();
      var terms = JsonSerializer.Deserialize<List<GlossaryItem>>(stringResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

      Assert.Equal(3, terms.Count);
      Assert.Contains(terms, t => t.Term == "Access Token");
      Assert.Contains(terms, t => t.Term == "JWT");
      Assert.Contains(terms, t => t.Term == "OpenID");
    }

    [Fact]
    public async Task AddTermWithoutAuthorization()
    {
      // Arrange
      var request = new HttpRequestMessage(HttpMethod.Post, "api/glossary");

      request.Content = new StringContent(JsonSerializer.Serialize(new
      {
        term = "MFA",
        definition = "An authentication process that considers multiple factors."
      }), Encoding.UTF8, "application/json");

      // Act
      var response = await httpClient.SendAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task ChangeTermWithInvalidAuthorization()
    {
      // Arrange
      var request = new HttpRequestMessage(HttpMethod.Post, "api/glossary");

      request.Content = new StringContent(JsonSerializer.Serialize(new
      {
        term = "MFA",
        definition = "An authentication process that considers multiple factors."
      }), Encoding.UTF8, "application/json");
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "This is an invalid token");

      // Act
      var response = await httpClient.SendAsync(request);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task AddTermWithAuthorization()
    {
      // Arrange
      var request = new HttpRequestMessage(HttpMethod.Post, "api/glossary");

      request.Content = new StringContent(JsonSerializer.Serialize(new
      {
        term = "MFA",
        definition = "An authentication process that considers multiple factors."
      }), Encoding.UTF8, "application/json");

      var accessToken = FakeJwtTokensManager.GenerateJwtToken();
      request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

      // Act
      HttpResponseMessage response = new HttpResponseMessage();
      try
      {
        response = await httpClient.SendAsync(request);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }


      // Assert
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
      Assert.Equal("/api/glossary/MFA", response.Headers.GetValues("Location").FirstOrDefault());
    }
  }
}
