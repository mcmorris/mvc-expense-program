namespace MVCExpense.Tests.Mockup
{
    namespace TddBankingTests
    {
        using System;
        using System.Collections.Generic;

        using ExpenseModel;

        using MVCExpense.Controllers.OER;

        public class MockUpCurrencyListing : CurrencyListing
        {
            public MockUpCurrencyListing() : base(new ISO4217Currency("USD", 2, "United States Dollar", null, DateTime.Now, "TestAccount", null, null, null, true))
            {
                this.Currencies = new List<ISO4217Currency>
                {
                    new ISO4217Currency("ADP", 0, "Andorran Peseta", DateTime.Parse("2003-07-01 12:00:00"), DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("AED", 2, "United Arab Emirates Dirham", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("AFA", 0, "Afghan Afghani", DateTime.Parse("2003-01-01 12:00:00"), DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("AFN", 2, "Afghan Afghani", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("ALK", 0, "Afghan Old Lek", DateTime.Parse("1989-12-01 12:00:00"), DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("ALL", 2, "Albanian Lek", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("CAD", 2, "Canadian Dollar", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("CHF", 2, "Swiss Franc", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("EUR", 2, "Euro", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("FIM", 0, "Ãland Islands Markka", DateTime.Parse("2002-03-01 12:00:00"), DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("GBP", 2, "Pound Sterling", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("ILS", 2, "Israeli New Shekel", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("IMP", 2, "Isle of Man pound (also Manx pound)", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("INR", 2, "Indian Rupee", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("TVD", 2, "Tuvalu Dollar", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("TWD", 2, "New Taiwan Dollar", null, DateTime.Now, "TestAccount", null, null, null, true),
                    //new ISO4217Currency("USD", 2, "United States Dollar", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("VND", 0, "Vietnamese Dồng", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("XBT", 8, "Bitcoin", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("XXX", 0, "No ISO4217Currency", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("YER", 2, "Yemeni Rial", null, DateTime.Now, "TestAccount", null, null, null, true),
                    new ISO4217Currency("ZAR", 2, "South African Rand", null, DateTime.Now, "TestAccount", null, null, null, true),
                };
            }
        }
    }
}
