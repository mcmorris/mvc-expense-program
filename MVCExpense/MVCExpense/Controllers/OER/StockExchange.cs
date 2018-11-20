namespace MVCExpense.Controllers.OER
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    // TODO: Replace with controller
    public class StockExchange : CurrencyListing, IStockExchange
    {
        private          IList<ExchangeRate>   exchangeRates;
        private readonly IStockExchangeUpdater stockUpdater;
        private readonly ISO4217Currency       internalCurrency;

        public StockExchange(IStockExchangeUpdater updater, string internalCurrencyCode)
        {
            this.exchangeRates = new List<ExchangeRate>();
            this.stockUpdater  = updater;
            this.internalCurrency = this.GetCurrency(internalCurrencyCode, DateTime.Now);
        }

        public void AddExchangeRate(ExchangeRate newRate)
        {
            if (this.exchangeRates.Contains(newRate)) { return; }
            this.exchangeRates.Add(newRate);
        }

        public ExchangeRate GetExchangeRate(string currencyFrom)
        {
            return this.exchangeRates.FirstOrDefault(n => n.CurrencyFrom?.Id == currencyFrom);
        }

        public ExchangeRate ExchangeRate(string currencyFrom, decimal rate)
        {
            return new ExchangeRate(
                this.GetHashCode(),
                DateTime.Now,
                rate,
                this.GetCurrency(currencyFrom, DateTime.Now),
                this.internalCurrency,
                true
            );
        }

        public void UpdateExchangeRates()
        {
            var results = this.stockUpdater.GetLatestRates();
            foreach (var rate in results.Rates)
            {
                this.AddExchangeRate(this.ExchangeRate(rate.Key, rate.Value));
            }
        }
    }
}