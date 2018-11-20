namespace MVCExpense.Tests.Model
{
    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Controllers.OER;

    [TestClass]
    public class ISO4217CurrencyFormattingTest
    {
        [TestMethod]
        public void TestDecimalCultureFormatting()
        {
            const decimal Amount = 100;
            Assert.AreEqual(Amount.FormatMoney("AUD"), "$100.00");
            Assert.AreEqual(Amount.FormatMoney("GBP"), "£100.00");
            Assert.AreEqual(Amount.FormatMoney("EUR"), "100,00 €");
            Assert.AreEqual(Amount.FormatMoney("VND"), "100,00 ₫");
            Assert.AreEqual(Amount.FormatMoney("INR"), "₹ 100.00");
        }

        [TestMethod]
        public void TestCurrencyCultureFormatting()
        {
            var stockExchange = new StockExchange(new StockExchangeUpdater(), "USD");
            /*var bank          = new Bank(stockExchange, new MockUpCurrencyListing(), "USD");
            Assert.AreEqual(bank.Dollar(100M).ToString(), "$100.00");
            Assert.AreEqual(bank.Pound(100M).ToString(),  "£100.00");
            Assert.AreEqual(bank.Euro(100M).ToString(),   "100,00 €");
            Assert.AreEqual(bank.Dong(100M).ToString(),   "100,00 ₫");
            Assert.AreEqual(bank.Rial(100M).ToString(),   "100/00ريال");
            */
        }
    }
}
