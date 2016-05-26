using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockService
{
    public interface ICalculationService
    {
        double CalculateDividendYield(string symbol, double price);
        double CalculatePERatio(string symbol, double price);
        double CalculateVolumeWeightedStockPrice(string symbol);
        double CalculateIndex();

    }
}
