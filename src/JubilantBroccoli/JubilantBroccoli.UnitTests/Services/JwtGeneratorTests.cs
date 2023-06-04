using JubilantBroccoli.BusinessLogic.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace JubilantBroccoli.UnitTests.Services;

public class JwtGeneratorTests
{
    private readonly IConfiguration _configuration;

    public JwtGeneratorTests()
    {
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "Jwt:Issuer", "issuer" },
                { "Jwt:Audience", "audience" },
                { "Jwt:Subject", "subject" },
                { "Jwt:Key", "secret_keysecret_keysecret_keysecret_keysecret_key" }
            })
            .Build();
    }

    [Fact]
    public void CreateToken_ReturnsValidAuthenticationResponse()
    {
        // Arrange
        var user = new IdentityUser
        {
            Id = "user_id",
            UserName = "test_user",
            Email = "test@example.com"
        };

        var jwtGenerator = new JwtGenerator(_configuration);

        // Act
        var authenticationResponse = jwtGenerator.CreateToken(user);

        // Assert
        Assert.NotNull(authenticationResponse);
        Assert.NotNull(authenticationResponse.Token);
        Assert.True(authenticationResponse.Expiration > DateTime.UtcNow);
    }
}
