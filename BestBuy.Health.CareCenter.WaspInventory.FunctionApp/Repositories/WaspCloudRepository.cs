using BestBuy.Health.CareCenter.WaspInventory.Extensions;
using BestBuy.Health.CareCenter.WaspInventory.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Repositories
{
    public class WaspCloudRepository : IWaspCloudRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _inventorySearchUrl;
        private readonly string _removeItemUrl;

        public WaspCloudRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(WaspCloudRepository));
            _inventorySearchUrl = $"public-api/ic/item/advancedinventorysearch";
            _removeItemUrl = $"public-api/transactions/item/remove";
        }

        public async Task<RemoveItemResponse> RemoveItemFromInventoryAsync(string queueItem)
        {
            var itemAdjustTransactionModels = JsonConvert.DeserializeObject<List<ItemAdjustTransactionModel>>(queueItem);

            var requestContent = new StringContent($"{JsonConvert.SerializeObject(itemAdjustTransactionModels)}", Encoding.UTF8, "application/json");         

            var response = await _httpClient.PostAsync(_removeItemUrl, requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            WaspExtensions.VerifyHttpResponseMessage(response, content);

            var removeItemResponse = JsonConvert.DeserializeObject<RemoveItemResponse>(content);

            return removeItemResponse;
        }

        public async Task<InventorySearchResponse> AdvancedInventorySearchAsync(string inventorySearchRequest)
        {
            var inventoryRequestContent = new StringContent(inventorySearchRequest, Encoding.UTF8, "application/json");

            var inventorySearchMessage = await _httpClient.PostAsync(_inventorySearchUrl, inventoryRequestContent);

            var inventorySearchResponseContent = await inventorySearchMessage.Content.ReadAsStringAsync();

            var inventorySearchResponse = JsonConvert.DeserializeObject<InventorySearchResponse>(inventorySearchResponseContent);

            return inventorySearchResponse;
        }
    }
}
