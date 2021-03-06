﻿namespace MVCExpense.Tests.Model
{
    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Controllers.OER;
    using MVCExpense.Tests.Mockup.TddBankingTests;

    [TestClass]
    public class ISO4217CurrencyFormattingTest : TestBase
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
            var stockExchange = new StockExchange(new StockExchangeUpdater(), new MockUpCurrencyListing(this.Model));
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
