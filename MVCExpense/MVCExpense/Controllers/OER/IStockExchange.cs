namespace MVCExpense.Controllers.OER
{
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    public interface IStockExchange
    {

        void AddExchangeRate(ExchangeRate newRate);

        ExchangeRate GetExchangeRate(string currencyFrom);

        void UpdateExchangeRates();
    }
}