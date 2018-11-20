namespace MVCExpense.Controllers.OER
{
    using OpenExchangeRates;

    public interface IStockExchangeUpdater
    {
        ExchangeRateData GetLatestRates();
    }
}
