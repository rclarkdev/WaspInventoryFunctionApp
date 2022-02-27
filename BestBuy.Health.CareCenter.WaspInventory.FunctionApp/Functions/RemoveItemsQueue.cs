using BestBuy.Health.CareCenter.WaspInventory.Models;
using BestBuy.Health.CareCenter.WaspInventory.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BestBuy.Health.CareCenter.WaspInventory.Functions
{
    public class RemoveItemsQueue
    {
        private readonly HttpClient _httpClient;
        private readonly IWaspInventoryService _inventoryItemService;

        public RemoveItemsQueue(IHttpClientFactory httpClientFactory, IWaspInventoryService inventoryItemService)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(RemoveItemsQueue));
            _inventoryItemService = inventoryItemService;
        }

        [FunctionName(nameof(CreateRemoveItemsQueue))]
        [OpenApiOperation(operationId: nameof(CreateRemoveItemsQueue), tags: new[] { "Queue" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(RemoveInventoryMessage))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string))]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateRemoveItemsQueue(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("RemoveItemsQueue", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> removeQueue,
            ILogger log)
        {
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var removeInventoryMessage = JsonConvert.DeserializeObject<RemoveInventoryMessage>(requestBody);

                log.LogInformation($"Request: {requestBody}");

                if (removeInventoryMessage.Orders.Any())
                {
                    var itemAdjustTransactionModels = await _inventoryItemService.FindLocationForInventoryRemovalAsync(removeInventoryMessage, log);

                    log.LogInformation($"{Constants.ATTEMPTING_TO_QUEUE}");

                    await removeQueue.AddAsync(JsonConvert.SerializeObject(itemAdjustTransactionModels));

                    log.LogInformation($"{Constants.SUCCESSFULLY_QUEUED}");
                }

                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);
                return new UnprocessableEntityObjectResult(ex);
            }
        }
    }
}
