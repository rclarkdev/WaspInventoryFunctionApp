using System.Collections.Generic;

namespace BestBuy.Health.CareCenter.WaspInventory.Models
{
    public class InventoryItem
    {
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public int ItemType { get; set; }
        public string SiteName { get; set; }
        public string LocationCode { get; set; }
        public double TotalAvailable { get; set; }
        public string AlternateItemNumber { get; set; }
    }

    public class InventorySearchResponse
    {
        public List<InventoryItem> Data { get; set; }
        public List<object> Messages { get; set; }
        public bool HasSuccessWithMoreDataRemaining { get; set; }
        public bool HasError { get; set; }
        public bool HasMessage { get; set; }
        public bool HasHttpError { get; set; }
    }

}
