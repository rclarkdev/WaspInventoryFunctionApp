using BestBuy.Health.CareCenter.WaspInventory.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Functions
{
    public class ProcessRemoveItemsQueue
    {

        private readonly HttpClient _httpClient;
        private readonly IWaspInventoryService _inventoryItemService;

        public ProcessRemoveItemsQueue(IHttpClientFactory httpClientFactory,
            IWaspInventoryService inventoryItemService)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(ProcessRemoveItemsQueue));
            _inventoryItemService = inventoryItemService;
        }

        [FunctionName("HandleRemoveItemsRequests")]
        public async Task Run(
            [QueueTrigger("RemoveItemsQueue", Connection = "AzureWebJobsStorage")] string queueItem,
            ILogger log)
        {
            try
            {
                log.LogInformation($"{Constants.ATTEMPING_TO_REMOVE_FROM_INVENTORY}: {queueItem}");

                var removeItemResponse = await _inventoryItemService.RemoveItemFromInventoryAsync(queueItem, log);

                log.LogInformation($"{Constants.QUEUE_ITEM_REMOVED}: {queueItem}");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"{Constants.PROCESS_QUEUE_ERROR}: {queueItem}");
                throw;
            }
        }
    }
}
