using System;
using Microsoft.Extensions.Configuration;

namespace MediaServicesPocTests;

public class ConfigWrapper
{
    private readonly IConfiguration config;

    public ConfigWrapper(IConfiguration config)
    {
        this.config = config;
    }

    public string SubscriptionId => config["SubscriptionId"];

    public string ResourceGroup => config["ResourceGroup"];

    public string AccountName => config["AccountName"];

    public string AadTenantId => config["AadTenantId"];

    public string AadClientId => config["AadClientId"];

    public string AadSecret => config["AadSecret"];

    public Uri ArmAadAudience => new Uri(config["ArmAadAudience"]);

    public Uri AadEndpoint => new Uri(config["AadEndpoint"]);

    public Uri ArmEndpoint => new Uri(config["ArmEndpoint"]);

    public string EventHubConnectionString => config["EventHubConnectionString"];

    public string EventHubName => config["EventHubName"];

    public string StorageContainerName => config["StorageContainerName"];

    public string StorageAccountName => config["StorageAccountName"];

    public string StorageAccountKey => config["StorageAccountKey"];

    public string SymmetricKey => config["SymmetricKey"];

    public string AskHex => config["AskHex"];

    public string FairPlayPfxPath => config["FairPlayPfxPath"];

    public string FairPlayPfxPassword => config["FairPlayPfxPassword"];
}