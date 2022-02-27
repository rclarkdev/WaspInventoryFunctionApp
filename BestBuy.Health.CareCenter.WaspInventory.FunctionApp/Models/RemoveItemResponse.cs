using System.Collections.Generic;

namespace BestBuy.Health.CareCenter.WaspInventory.Models
{
    public class ResultList
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public int HttpStatusCode { get; set; }
        public string FieldName { get; set; }
    }

    public class Data
    {
        public List<ResultList> ResultList { get; set; }
        public int SuccessfullResults { get; set; }
        public int TotalResults { get; set; }
    }

    public class RemoveItemResponse
    {
        public Data Data { get; set; }
        public List<object> Messages { get; set; }
        public bool HasSuccessWithMoreDataRemaining { get; set; }
        public bool HasError { get; set; }
        public bool HasMessage { get; set; }
        public bool HasHttpError { get; set; }
    }
}
