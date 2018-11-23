namespace MVCExpense.Controllers.OER
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    // TODO: Convert to controller
    public class StockExchange : CurrencyListing, IStockExchange
    {
        private          IList<ExchangeRate>   exchangeRates;
        private readonly IStockExchangeUpdater stockUpdater;
        private readonly ISO4217Currency       internalCurrency;

        public StockExchange(IStockExchangeUpdater updater, ISO4217Currency internalCurrency)
            : base(internalCurrency)
        {
            this.exchangeRates = new List<ExchangeRate>();
            this.stockUpdater  = updater;
            this.internalCurrency = internalCurrency;
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