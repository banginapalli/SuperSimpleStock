using SuperSimpleStock.Domain;
using System;
using System.Collections.Generic;
using static System.String;

namespace StockService
{
    public class TradeService : ITradeService
    {
        readonly IStockService _stockService;
        readonly List<Trade> _trades = new List<Trade>();
        public TradeService(IStockService stockService)
        {
            if (stockService == null) throw new ArgumentNullException(nameof(stockService));

            _stockService = stockService;
        }

        public IList<Trade> Trades
        {
            get
            {
                return _trades.AsReadOnly();
            }
        }

        public void PlaceTrade(TradeType tradeType, string symbol, uint quantity, double price)
        {
            if (!_stockService.IsVaid(symbol)) throw new ArgumentException(Format("Symbol not found, {0}", symbol), nameof(symbol));
            
            _trades.Add(new Trade(tradeType, symbol, quantity, price));

        }
    }
}
