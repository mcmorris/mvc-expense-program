namespace MVCExpense.Controllers.OER
{
    using OpenExchangeRates;

    public class StockExchangeUpdater : IStockExchangeUpdater
    {
        public ExchangeRateData GetLatestRates()
        {
            return OpenExchangeRates.Client.Get();
        }
    }
}