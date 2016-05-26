using SuperSimpleStock.Domain;
using System.Collections.Generic;

namespace StockService
{
    public interface ITradeService
    {
        IList<Trade> Trades { get; }
        void PlaceTrade(TradeType tradeType, string symbol, uint quantity, double price);
    }
}
