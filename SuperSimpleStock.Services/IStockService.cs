using SuperSimpleStock.Domain;
using System.Collections.Generic;

namespace StockService
{
    public interface IStockService
    {
        IList<Stock> Stocks { get; }
        Stock Get(string symbol);

        bool IsVaid(string symbol);

    }
}
