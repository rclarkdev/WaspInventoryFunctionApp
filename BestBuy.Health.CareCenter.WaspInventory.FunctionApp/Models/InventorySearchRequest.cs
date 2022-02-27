using System;
using System.Collections.Generic;
using System.Text;

namespace BestBuy.Health.CareCenter.WaspInventory.Models
{
    public class InventorySearchRequest
    {
        public InventorySearchRequest(string[] itemNumbersForRemoval)
        {
            PageSize = int.MaxValue;
            Filter = new ParentFilter
            {
                Filters = new List<Filter>
                {
                    new Filter
                    {
                        Field = "ItemNumber",
                        Operator = "string.IsInList",
                        Value = itemNumbersForRemoval
                    }
                }
            };
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public ParentFilter Filter { get; set; }
    }

    public class Filter
    {
        public string Operator { get; set; }
        public string[] Value { get; set; }
        public string Field { get; set; }
    }

    public class ParentFilter
    {
        public List<Filter> Filters { get; set; }
    }
}
