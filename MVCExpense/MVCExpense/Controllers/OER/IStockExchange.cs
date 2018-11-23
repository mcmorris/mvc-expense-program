namespace MVCExpense.Controllers.OER
{
    using ExpenseModel;

    public interface IStockExchange
    {
        void AddExchangeRate(ExchangeRate newRate);

        ExchangeRate GetExchangeRate(string currencyFrom);

        void UpdateExchangeRates();
    }
}