using System;
using System.Linq;

namespace StockService
{
    public class CalculationService : ICalculationService
    {
        readonly IStockService _stockService;
        readonly ITradeService _tradeService;
        public CalculationService(IStockService stockService, ITradeService tradeService)
        {
            if (stockService == null) throw new ArgumentNullException(nameof(stockService));
            if (tradeService == null) throw new ArgumentNullException(nameof(tradeService));

            _stockService = stockService;
            _tradeService = tradeService;
        }
        public double CalculateDividendYield(string symbol, double price)
        {
            if (price <= 0) throw new ArgumentOutOfRangeException("price must be +ve", nameof(price));

            var stock = _stockService.Get(symbol);

            switch (stock.Type)
            {
                case SuperSimpleStock.Domain.StockType.Common:
                    return stock.LastDividend / price;
                case SuperSimpleStock.Domain.StockType.Preffered:
                    return (stock.FixedDividend.GetValueOrDefault() * stock.ParValue) / price;
                default:
                    throw new NotSupportedException("This stock type not supported");
            }
        }

        public double CalculateIndex()
        {
            var prices = _stockService.Stocks
                .Select(s => _tradeService.Trades.LastOrDefault(t => t.Symbol == s.Symbol))
                .Where(t => t != null).Select(t => t.Price).ToList();

            if (!prices.Any()) throw new InvalidOperationException("No prices were found");

            return Math.Pow(prices.Aggregate(1.0d, (a, b) => a * b), 1 / (double)prices.Count);
        }

        public double CalculatePERatio(string symbol, double price)
        {
            if (price <= 0) throw new ArgumentOutOfRangeException("price must be +ve", nameof(price));

            var stock = _stockService.Get(symbol);

            return stock.LastDividend != 0 ? price / stock.LastDividend : double.NaN;
        }

        public double CalculateVolumeWeightedStockPrice(string symbol)
        {
            var checkTimeStamp = DateTime.Now.AddMinutes(-15);
            var trades = _tradeService.Trades.Where(t => t.TimeStamp > checkTimeStamp && t.Symbol == symbol).ToList();

            if (!trades.Any()) throw new ArgumentNullException("no trades found for given symbol", nameof(symbol));

            var sigmaQuantity = 0u;
            var sigmaQuantityPrice = 0.0d;

            foreach (var item in trades)
            {
                sigmaQuantity += item.Quantity;
                sigmaQuantityPrice += item.Quantity * item.Price;
            }

            return sigmaQuantityPrice / sigmaQuantity;
        }
    }
}