namespace MVCExpense.Controllers.OER
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    // TODO: Convert to controller
    public class StockExchange : IStockExchange
    {
        private          IList<ExchangeRate>   exchangeRates;
        private          ICurrencyListing       currencies;
        private readonly IStockExchangeUpdater stockUpdater;
        private readonly ISO4217Currency       internalCurrency;

        public StockExchange(IStockExchangeUpdater updater, ICurrencyListing currencyListing)
        {
            this.exchangeRates = new List<ExchangeRate>();
            this.stockUpdater  = updater;
            this.internalCurrency = currencyListing.InternalCurrency;
            this.currencies = currencyListing;
        }

        public void AddExchangeRate(ExchangeRate newRate)
        {
            if (this.exchangeRates.Contains(newRate)) { return; }
            this.exchangeRates.Add(newRate);
        }

        public ExchangeRate GetExchangeRate(string currencyFrom)
        {
            return this.exchangeRates.LastOrDefault(n => n.CurrencyFrom?.Id == currencyFrom);
        }

        public ExchangeRate ExchangeRate(string currencyFrom, decimal rate)
        {
            return new ExchangeRate(
                DateTime.Now,
                rate,
                this.currencies.GetCurrency(currencyFrom, DateTime.Now),
                this.internalCurrency
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