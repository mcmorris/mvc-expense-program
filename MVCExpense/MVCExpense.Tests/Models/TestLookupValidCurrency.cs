namespace MVCExpense.Tests.Model
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Tests.Mockup.TddBankingTests;

    [TestClass]
    public class TestCurrencyLookup
    {
        [TestMethod]
        public void TestLookupValidCurrency()
        {
            var currencies   = new MockUpCurrencyListing();
            var usdCurrencyA = currencies.GetCurrency("USD", DateTime.Now);
            Assert.AreEqual(usdCurrencyA.Name, "United States Dollar");
        }

        [TestMethod]
        public void TestLookupInvalidCurrency()
        {
            var currencies = new MockUpCurrencyListing();
            Assert.AreEqual(currencies.GetCurrency("ZZZ", DateTime.Now), null);
        }

        [TestMethod]
        public void TestLookupValidOldCurrencyAtValidTime()
        {
            var currencies  = new MockUpCurrencyListing();
            var oldCurrency = currencies.GetCurrency("ADP", DateTime.Parse("2003-06-01 12:00:00"));
            Assert.AreEqual(oldCurrency.Name,           "Andorran Peseta");
        }

        [TestMethod]
        public void TestLookupValidOldCurrencyAtInvalidTime()
        {
            var currencies = new MockUpCurrencyListing();
            Assert.AreEqual(currencies.GetCurrency("ADP", DateTime.Parse("2003-08-01 12:00:00")), null);
        }

        [TestMethod]
        public void TestLookupValidCurrencyWithNullNumericCode()
        {
            var currencies = new MockUpCurrencyListing();
            var currency   = currencies.GetCurrency("TVD", DateTime.Now);
            Assert.AreEqual(currency.Name,          "Tuvalu Dollar");
        }

        [TestMethod]
        public void TestLookupValidISO4217CurrencyWithNegligibleMinorUnits()
        {
            var currencies = new MockUpCurrencyListing();
            var currency   = currencies.GetCurrency("VND", DateTime.Now);
            Assert.AreEqual(currency.Name,          "Vietnamese Dồng");
        }
    }
}