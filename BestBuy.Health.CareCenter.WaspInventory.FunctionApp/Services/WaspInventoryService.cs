using BestBuy.Health.CareCenter.WaspInventory.Extensions;
using BestBuy.Health.CareCenter.WaspInventory.Models;
using BestBuy.Health.CareCenter.WaspInventory.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Services
{
    public class WaspInventoryService : IWaspInventoryService
    {
        private readonly IWaspCloudRepository _waspCloudRepository;

        public WaspInventoryService(IWaspCloudRepository waspCloudRepository)
        {
            _waspCloudRepository = waspCloudRepository;
        }

        public async Task<RemoveItemResponse> RemoveItemFromInventoryAsync(string queueItem, ILogger log)
        {
            var removeItemResponse = await _waspCloudRepository.RemoveItemFromInventoryAsync(queueItem);
           
            WaspExtensions.VerifyRemoveItemResponse(removeItemResponse, log);

            return removeItemResponse;
        }

        public async Task<IEnumerable<ItemAdjustTransactionModel>> FindLocationForInventoryRemovalAsync(RemoveInventoryMessage removeInventoryMessage, ILogger log)
        {
            var itemAdjustTransactionModels = new List<ItemAdjustTransactionModel>();

            removeInventoryMessage.Orders.ToList()
                .ForEach(o => itemAdjustTransactionModels.AddRange(o.LineItems));

            var itemNumbersForRemoval = itemAdjustTransactionModels
                .Select(m => m.ItemNumber).Distinct().ToArray();

            var inventorySearchRequest = $"{JsonConvert.SerializeObject(new InventorySearchRequest(itemNumbersForRemoval))}";

            log.LogInformation($"{Constants.ATTEMPTING_SEARCH_INVENTORY} {inventorySearchRequest}");

            var inventorySearchResponse = await _waspCloudRepository.AdvancedInventorySearchAsync(inventorySearchRequest);

            if (inventorySearchResponse?.Data != null)
            {
                log.LogInformation($"{Constants.SUCCESSFULLY_SEARCHED_INVENTORY} {JsonConvert.SerializeObject(inventorySearchResponse.Data)}");
                
                itemAdjustTransactionModels = FindLocations(removeInventoryMessage, inventorySearchResponse, log);
            }
            return itemAdjustTransactionModels;
        }

        private List<ItemAdjustTransactionModel> FindLocations (RemoveInventoryMessage removeInventoryMessage, InventorySearchResponse inventorySearchResponse, ILogger log)
        {
            var itemAdjustTransactionModels = new List<ItemAdjustTransactionModel>();

            log.LogInformation($"{Constants.ATTEMPTING_FIND_LOCATIONS} {JsonConvert.SerializeObject(inventorySearchResponse.Data)}");

            foreach (var order in removeInventoryMessage.Orders.GroupByItemNumber())
            {
                foreach (var item in order.LineItems)
                {
                    var inventoryResult = new InventoryItem();

                    var searchResponseItems = inventorySearchResponse.Data?
                        .Where(i => i.ItemNumber == item.ItemNumber && i.TotalAvailable >= item.Quantity && i.SiteName == item.SiteName);

                    if (searchResponseItems is null || !searchResponseItems.Any())
                    {
                        log.LogWarning($"Could not find a Location for ItemNumber: {item.ItemNumber}, Quantity: {item.Quantity}, Site: {item.SiteName}.");
                    } 
                    else
                    {
                        inventoryResult = searchResponseItems.MinBy(m => m.TotalAvailable);
                        item.LocationCode = inventoryResult.LocationCode;
                        inventoryResult.TotalAvailable -= item.Quantity;
                        itemAdjustTransactionModels.Add(item);
                    }
                }
            }
            return itemAdjustTransactionModels;
        }
    }
}
