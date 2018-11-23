namespace MVCExpense.Tests.Model
{
    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Controllers.OER;

    [TestClass]
    public class ExchangeRateTests
    {
        [TestMethod]
        public void TestAddExchangeRates()
        {
            var stockExchange = new StockExchange(new StockExchangeUpdater(), new ISO4217Currency("USD", 2, "Dollar", null));
            var newRate = stockExchange.ExchangeRate("CHF",            2.0M);
            Assert.AreEqual(newRate, stockExchange.ExchangeRate("CHF", 2.0M));
        }

        [TestMethod]
        public void TestGetExchangeRate()
        {
            var stockExchange = new StockExchange(new StockExchangeUpdater(), new ISO4217Currency("USD", 2, "Dollar", null));
            stockExchange.AddCurrency(new ISO4217Currency("CHF", 2, "Franc", null));
            stockExchange.AddExchangeRate(stockExchange.ExchangeRate("CHF", 2.0M));
            stockExchange.AddExchangeRate(stockExchange.ExchangeRate("CHF", 2.1M));
            var currentRate = stockExchange.GetExchangeRate("CHF");
            Assert.AreEqual(currentRate.ConversionRate, 2.1M);
        }

        [TestMethod]
        public void TestExchangeRatesEquality()
        {
            var stockExchange = new StockExchange(new StockExchangeUpdater(), new ISO4217Currency("USD", 2, "Dollar", null));
            stockExchange.AddCurrency(new ISO4217Currency("CHF", 2, "Franc", null));
            var newRateA = stockExchange.ExchangeRate("CHF", 2.0M);
            var newRateB = stockExchange.ExchangeRate("CHF", 2.0M);
            Assert.IsTrue(newRateA.Equals(newRateB));
            Assert.IsFalse(newRateA.Equals(null));
        }
    }
}
