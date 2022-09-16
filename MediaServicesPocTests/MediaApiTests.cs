using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.Management.Media;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.Rest;
using Xunit;

namespace MediaServicesPocTests;

public class MediaApiTests
{
    private readonly ConfigWrapper config;
    public MediaApiTests()
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

        config = new ConfigWrapper(builder.Build());
    }
    
    public const string TokenType = "Bearer";
    
    public static async Task<IAzureMediaServicesClient> CreateMediaServicesClientAsync(ConfigWrapper config)
    {
        var scopes = new[] { config.ArmAadAudience + "/.default" };

        var app = ConfidentialClientApplicationBuilder.Create(config.AadClientId)
            .WithClientSecret(config.AadSecret)
            .WithAuthority(AzureCloudInstance.AzurePublic, config.AadTenantId)
            .Build();

        var authResult = await app.AcquireTokenForClient(scopes)
            .ExecuteAsync()
            .ConfigureAwait(false);

        var credentials = new TokenCredentials(authResult.AccessToken, TokenType);

        return new AzureMediaServicesClient(config.ArmEndpoint, credentials)
        {
            SubscriptionId = config.SubscriptionId,
        };
    }

    [Fact]
    public async Task ItReturnAssetsList()
    {
        var client = await CreateMediaServicesClientAsync(config);
        var assets = await client.Assets.ListAsync(config.ResourceGroup, config.AccountName);

        assets.Count().Should().NotBe(0);

    }
    
}