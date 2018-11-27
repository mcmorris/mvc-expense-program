namespace MVCExpense.Tests.Mockup
{
    namespace TddBankingTests
    {
        using System.Collections.Generic;

        using ExpenseModel;

        using MVCExpense.Controllers.OER;
        using MVCExpense.Tests.Models;

        public class MockUpCurrencyListing : CurrencyListing
        {
            private ModelFactory model;

            public MockUpCurrencyListing(ModelFactory model) : base(model.USD)
            {
                this.Currencies = new List<ISO4217Currency>
                {
                    model.ADP,
                    model.AED,
                    model.AFA,
                    model.AFN,
                    model.ALK,
                    model.ALL,
                    model.CAD,
                    model.CHF,
                    model.EUR,
                    model.FIM,
                    model.GBP,
                    model.ILS,
                    model.IMP,
                    model.INR,
                    model.TVD,
                    model.TWD,
                    model.USD,
                    model.VND,
                    model.XBT,
                    model.XXX,
                    model.YER,
                    model.ZAR
                };
            }
        }
    }
}
