using System;
using System.Collections.Generic;

namespace BestBuy.Health.CareCenter.WaspInventory.Models
{
    public class ItemAdjustTransactionModel
    {
        public string ItemNumber { get; set; }

        public DateTime DateRemoved { get; set; }

        public string SiteName { get; set; }
        public string LocationCode { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCode { get; set; }
    }
    public class RemoveInventoryMessage
    {
        public IEnumerable<LineItemsWrapper> Orders { get; set; }
    }

    public class LineItemsWrapper
    {
        public IEnumerable<ItemAdjustTransactionModel> LineItems { get; set; }
    }

}
