using SuperSimpleStock.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockService
{
    public class StockService : IStockService
    {
        readonly List<Stock> _stocks;
        public IList<Stock> Stocks
        {
            get
            {
                return _stocks.AsReadOnly();
            }
        }

        public StockService()
        {
            _stocks = new List<Stock>
            {
                new Stock("TEA",0,100),
                new Stock("POP",8,100),
                new Stock("ALE",23,60),
                new Stock("GIN",8,100,StockType.Preffered,2),
                new Stock("JOE",13,250)

            };
        }

        public Stock Get(string symbol)
        {
            var stock = Stocks.FirstOrDefault(s => s.Symbol == symbol);

            if (stock == null) throw new ArgumentException(string.Format("Symbol not found, {0}", symbol), nameof(symbol));

            return stock;
        }

        public bool IsVaid(string symbol)
        {
            return Stocks.Any(s => s.Symbol == symbol);
        }
    }
}
