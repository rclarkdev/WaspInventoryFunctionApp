using BestBuy.Health.CareCenter.WaspInventory.Models;
using Microsoft.Extensions.Logging;
using ServiceStack.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace BestBuy.Health.CareCenter.WaspInventory.Extensions
{
    public static class WaspExtensions
    {
        public static void VerifyRemoveItemResponse(RemoveItemResponse response, ILogger log)
        {
            var resultList = response?.Data?.ResultList;

            if (resultList is null || !resultList.Any()) return;

            foreach (var result in resultList)
            {
                var message = string.IsNullOrWhiteSpace(result?.Message) ? string.Empty : $"- {result.Message}";
                var statusMessage = string.Empty;

                switch (result.HttpStatusCode)
                {
                    case (int)HttpStatusCode.OK:
                        statusMessage = $"Status: 200 OK {message}";
                        log.LogInformation(statusMessage);
                        break;
                    case (int)HttpStatusCode.NoContent:
                        statusMessage = $"Status: 204 No Content {message}";
                        log.LogInformation(statusMessage);
                        break;
                    case (int)HttpStatusCode.MultiStatus:
                        statusMessage = $"Status: 207 Warning {message}";
                        log.LogWarning(statusMessage);
                        break;
                    case (int)HttpStatusCode.MultipleChoices:
                        statusMessage = $"Status: 300 Multiple Choices or Business Logic Error {message}";
                        break;
                    case (int)HttpStatusCode.BadRequest:
                        statusMessage = $"Status: 400 Bad Request {message}";
                        break;
                    case (int)HttpStatusCode.Gone:
                        statusMessage = $"Status: 410 Gone or No Longer Available {message}";
                        break;
                    case (int)HttpStatusCode.UnprocessableEntity:
                        statusMessage = $"Status: 422 Unprocessable Entity {message}";
                        break;
                    case (int)HttpStatusCode.Forbidden:
                        statusMessage = $"Status: 403 Forbidden {message}";
                        break;
                    case (int)HttpStatusCode.NotFound:
                        statusMessage = $"Status: 404 Not Found {message}";
                        break;
                    case (int)HttpStatusCode.UpgradeRequired:
                        statusMessage = $"Status: 426 Upgrade Required {message}";
                        break;
                    case (int)HttpStatusCode.InternalServerError:
                        statusMessage = $"Status: 500 Internal Server Error {message}";
                        break;
                }

                if (result.HttpStatusCode >= 300)
                {
                    log.LogError(statusMessage);
                }
            }
        }

        public static void VerifyHttpResponseMessage(HttpResponseMessage message, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                switch (message.ReasonPhrase)
                {
                    case "invalid_token":
                        throw new HttpRequestException($"Invalid Authorization Token");
                }
            }
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, 
            Func<TSource, TKey> selector)
        {
            return source.MinBy(selector, null);
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> selector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (selector == null) throw new ArgumentNullException("selector");
            comparer ??= Comparer<TKey>.Default;

            using (var sourceIterator = source.GetEnumerator())
            {
                if (!sourceIterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contains no elements");
                }
                var min = sourceIterator.Current;
                var minKey = selector(min);
                while (sourceIterator.MoveNext())
                {
                    var candidate = sourceIterator.Current;
                    var candidateProjected = selector(candidate);
                    if (comparer.Compare(candidateProjected, minKey) < 0)
                    {
                        min = candidate;
                        minKey = candidateProjected;
                    }
                }
                return min;
            }
        }

        public static IEnumerable<LineItemsWrapper> GroupByItemNumber(this IEnumerable<LineItemsWrapper> orders)
        {
            var ordersGrouped = new List<LineItemsWrapper>();
            foreach (var order in orders)
            {
                var lineItemsWrapper = order.LineItems
                    .GroupBy(x => x.ItemNumber)
                    .Select(x => x.First()).ToList();

                lineItemsWrapper.ToList().ForEach(w =>
                    w.Quantity = order.LineItems
                    .Where(li => w.ItemNumber == li.ItemNumber)
                    .Select(x => x.Quantity).Sum());

                order.LineItems = lineItemsWrapper;
                ordersGrouped.Add(order);
            }

            return ordersGrouped;
        }
    }
}
