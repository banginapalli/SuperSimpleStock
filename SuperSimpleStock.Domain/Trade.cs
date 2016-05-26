using System;
using static System.String;

namespace SuperSimpleStock.Domain
{
    public class Trade
    {
        public DateTime TimeStamp { get; }

        public uint Quantity { get; }
        public TradeType Type { get; }
        public double Price { get; }
        public string Symbol { get; set; }

        public Trade(TradeType type, string symbol, uint quantity, double price)
        {
            if (IsNullOrEmpty(symbol) || IsNullOrWhiteSpace(symbol))
                throw new ArgumentException("Stock symbol can not be empty", nameof(symbol));

            if (quantity < 1)
                throw new ArgumentOutOfRangeException(nameof(quantity), "Trade quantity can not be less than 1");

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Trade price must be +ve");

            Symbol = symbol;
            Type = type;
            Quantity = quantity;
            Price = price;
            TimeStamp = DateTime.Now;
        }
    }
}
