using BestBuy.Health.CareCenter.WaspInventory.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Repositories
{
    public interface IWaspCloudRepository
    {
        Task<InventorySearchResponse> AdvancedInventorySearchAsync(string inventorySearchRequest);
        Task<RemoveItemResponse> RemoveItemFromInventoryAsync(string queueItem);
    }
}
