using NUnit.Framework;
using SuperSimpleStock.Domain;
using System;

namespace StockService.Tests
{
    public class StockServiceTests
    {
        private IStockService _stockService;
        private ITradeService _tradeService;
        private ICalculationService _claculationService;

        [SetUp]
        public void Setup()
        {
            _stockService = new StockService();
            _tradeService = new TradeService(_stockService);
            _claculationService = new CalculationService(_stockService, _tradeService);
        }

        [TearDown]
        public void TearDown()
        {
            _stockService = null;
            _tradeService = null;
            _claculationService = null;
        }

        [Test]
        [TestCase("TEA", 10d, 0.0d)]
        [TestCase("POP", 8.0d, 1.0d)]
        [TestCase("ALE", 23.0d, 1.0d)]
        [TestCase("GIN", 8.0d, 25.0d)]
        [TestCase("JOE", 13.0d, 1.0d)]
        public void CalculateDividendYieldTests(string symbol, double price, double expected)
        {
            var result = _claculationService.CalculateDividendYield(symbol, price);

            Assert.AreEqual(expected, result);
        }

        [TestCase("TEA", 0.0d, 0.0d)]
        public void CalculateDividendYieldThrowExceptionTest(string symbol, double price, double expected)
        {
            var e = Assert.Throws<ArgumentOutOfRangeException>(() => _claculationService.CalculateDividendYield(symbol, price));

            Assert.That(e.ParamName, Is.EqualTo("price must be +ve"));
        }

        [Test]
        [TestCase("TEA", 10d, double.NaN)]
        [TestCase("POP", 8.0d, 1.0d)]
        [TestCase("ALE", 23.0d, 1.0d)]
        [TestCase("GIN", 8.0d, 1.0d)]
        [TestCase("JOE", 13.0d, 1.0d)]
        public void CalculatePERatioTest(string symbol, double price, double expected)
        {
            var result = _claculationService.CalculatePERatio(symbol, price);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CalculateIndexThrowExceptionTest()
        {
            var e = Assert.Throws<InvalidOperationException>(() => _claculationService.CalculateIndex());

            Assert.That(e.Message, Is.EqualTo("No prices were found"));
        }

        [Test]
        public void CalculateIndexTest()
        {
            _tradeService.PlaceTrade(TradeType.Buy, "TEA", 10, 1.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "POP", 1000, 2.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "ALE", 120, 4.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "GIN", 1, 2.0d);

            var result = _claculationService.CalculateIndex();

            Assert.AreEqual(2.0d, result);
        }

        [Test]
        public void CalculateVolumeWeightedStockPriceTest()
        {
            _tradeService.PlaceTrade(TradeType.Buy, "TEA", 10, 1.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "TEA", 10, 1.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "TEA", 10, 1.0d);
            _tradeService.PlaceTrade(TradeType.Buy, "TEA", 10, 1.0d);

            var result = _claculationService.CalculateVolumeWeightedStockPrice("TEA");

            Assert.AreEqual(1.0d, result);

        }
    }
}
