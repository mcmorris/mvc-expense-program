namespace MVCExpense.Controllers.OER
{
    using System;

    using ExpenseModel;

    public interface ICurrencyListing
    {
        void AddCurrency(ISO4217Currency newRate);

        ISO4217Currency GetCurrency(string currencyFromAlphabeticCode, DateTime dateSearched);

        ISO4217Currency InternalCurrency { get; }
    }

}