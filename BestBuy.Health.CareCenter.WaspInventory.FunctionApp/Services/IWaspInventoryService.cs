using BestBuy.Health.CareCenter.WaspInventory.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Services
{
    public interface IWaspInventoryService
    {
        Task<RemoveItemResponse> RemoveItemFromInventoryAsync(string queueItem, ILogger log);
        Task<IEnumerable<ItemAdjustTransactionModel>> FindLocationForInventoryRemovalAsync(RemoveInventoryMessage removeInventoryMessage, ILogger log);
    }
}

