namespace MVCExpense.Controllers.OER
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    // TODO: Replace with controller
    public class CurrencyListing : ICurrencyListing
    {
        protected IList<ISO4217Currency> Currencies;

        public CurrencyListing()
        {
            this.Currencies = new List<ISO4217Currency>();
        }

        public void AddCurrency(ISO4217Currency newRate)
        {
            if (this.Currencies.Contains(newRate)) { return; }
            this.Currencies.Add(newRate);
        }

        public ISO4217Currency GetCurrency(string currencyFromAlphabeticCode, DateTime dateSearched)
        {
            return this.Currencies.FirstOrDefault(n => n.Id == currencyFromAlphabeticCode && (n.WithdrawalDate == null || n.WithdrawalDate.Value.Ticks >= dateSearched.Ticks));
        }
    }
}