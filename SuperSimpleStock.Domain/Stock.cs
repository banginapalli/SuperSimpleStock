using System;
using static System.String;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSimpleStock.Domain
{
    public class Stock
    {
        public string Symbol { get; }
        public double LastDividend { get; }
        public double? FixedDividend { get; }
        public double ParValue { get; }
        public StockType Type { get; }

        public Stock(string symbol, double lastDividend, double parValue, StockType stockType=StockType.Common, double? fixedDividend=null)
        {
            if (IsNullOrEmpty(symbol) || IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Symbol can't be empty", nameof(symbol));

            if (lastDividend < 0)
                throw new ArgumentOutOfRangeException(nameof(lastDividend), "last dividend value can not be negative");

            if (fixedDividend.HasValue && fixedDividend.Value < 0)
                throw new ArgumentOutOfRangeException(nameof(fixedDividend), "fixed dividend value can not be negative");

            if (parValue < 0)
                throw new ArgumentOutOfRangeException(nameof(parValue), "par value can not be negative");

            Symbol = symbol;
            LastDividend = lastDividend;
            FixedDividend = fixedDividend;
            ParValue = parValue;
            Type = stockType;
        }
    }
}
