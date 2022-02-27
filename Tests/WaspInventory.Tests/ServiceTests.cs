using BestBuy.Health.CareCenter.WaspInventory.Models;
using BestBuy.Health.CareCenter.WaspInventory.Repositories;
using BestBuy.Health.CareCenter.WaspInventory.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

namespace WaspInventory.Tests
{
    public class ServiceTests
    {
        private readonly Mock<IWaspCloudRepository> mockWaspCloudRepo = new Mock<IWaspCloudRepository>();
        private readonly Mock<ILogger<WaspInventoryService>> mockLogger = new Mock<ILogger<WaspInventoryService>>();

        [Theory]
        [MemberData(nameof(TestFactory.RemovalRequestData), MemberType = typeof(TestFactory))]
        public async Task FindLocationForInventoryRemoval_should_attach_correct_locations_to_ItemAdjustTransactionModels(string requestBody)
        {
            var testResponseData = TestFactory.InventorySearchResponseData();

            var searchResponse = JsonConvert.DeserializeObject<InventorySearchResponse>(testResponseData);

            mockWaspCloudRepo.Setup(x => x.AdvancedInventorySearchAsync(It.IsAny<string>())).ReturnsAsync(searchResponse);
            var waspInventoryService = new WaspInventoryService(mockWaspCloudRepo.Object);

            requestBody = requestBody.Replace(Environment.NewLine, "");

            var removeInventoryMessage = JsonConvert.DeserializeObject<RemoveInventoryMessage>(requestBody);

            var itemAdjustTransactionModels = await waspInventoryService.FindLocationForInventoryRemovalAsync(removeInventoryMessage, mockLogger.Object);

            foreach (var transactionModel in itemAdjustTransactionModels)
            {
                Assert.True(!string.IsNullOrEmpty(transactionModel.LocationCode));
            }
        }
    }
}
