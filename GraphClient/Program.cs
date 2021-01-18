
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

public class Program
{
    private const string _clientId = "f94ad9bc-518c-40ba-8a65-66e7e5c056a5";
    private const string _tenantId = "9950e883-d26b-4f0f-8e51-1d471ed37417";

    public static async Task Main(string[] args)
    {
        IPublicClientApplication app;

        app = PublicClientApplicationBuilder
            .Create(_clientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
            .WithRedirectUri("http://localhost")
            .Build();

        List<string> scopes = new List<string>
    {
        "user.read"
    };

        //AuthenticationResult result;

        /*result = await app
            .AcquireTokenInteractive(scopes)
            .ExecuteAsync();*/

        //Console.WriteLine($"Token:\t{result.AccessToken}");

        DeviceCodeProvider provider = new DeviceCodeProvider(app, scopes);

        GraphServiceClient client = new GraphServiceClient(provider);

        User myProfile = await client.Me
        .Request()
        .GetAsync();

        Console.WriteLine($"Name:\t{myProfile.DisplayName}");
        Console.WriteLine($"AAD Id:\t{myProfile.Id}");

    }
}