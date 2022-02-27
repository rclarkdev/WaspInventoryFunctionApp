using BestBuy.Health.CareCenter.WaspInventory.Functions;
using BestBuy.Health.CareCenter.WaspInventory.Repositories;
using BestBuy.Health.CareCenter.WaspInventory.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

[assembly: FunctionsStartup(typeof(BestBuy.Health.CareCenter.WaspInventory.Startup))]

namespace BestBuy.Health.CareCenter.WaspInventory
{
    public class Startup : FunctionsStartup
    {
        private string authHeader = Environment.GetEnvironmentVariable("WaspAuthenticationHeader");
        private string waspBaseUrl = Environment.GetEnvironmentVariable("WaspInventoryBaseUrl");

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var authorization = new AuthenticationHeaderValue("Bearer", authHeader);

            builder.Services.AddHttpClient(nameof(ProcessRemoveItemsQueue), httpClient =>
            {
                httpClient.BaseAddress = new Uri(waspBaseUrl);
                httpClient.DefaultRequestHeaders.Authorization = authorization;
            });

            builder.Services.AddHttpClient(nameof(RemoveItemsQueue), httpClient =>
            {
                httpClient.BaseAddress = new Uri(waspBaseUrl);
                httpClient.DefaultRequestHeaders.Authorization = authorization;
            });

            builder.Services.AddHttpClient(nameof(WaspCloudRepository), httpClient =>
            {
                httpClient.BaseAddress = new Uri(waspBaseUrl);
                httpClient.DefaultRequestHeaders.Authorization = authorization;
            });

            builder.Services.AddTransient<IWaspInventoryService, WaspInventoryService>();
            builder.Services.AddTransient<IWaspCloudRepository, WaspCloudRepository>();
        }
    }
}
