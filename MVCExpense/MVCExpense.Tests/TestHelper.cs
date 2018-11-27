
namespace MVCExpense.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ExpenseModel;

    using MVCExpense.Tests.Models;

    public class TestHelper
    {
        public string CreateStringLongerThan(int length)
        {
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++) { stringBuilder.Append('x'); }

            return stringBuilder.ToString();
        }

        public ICollection<Transaction> BuildTestTransactions(ModelFactory factory, User user, ExchangeRate exchangeRateA, ExchangeRate exchangeRateB)
        {
            return new List<Transaction>
            {
               factory.Transaction(user.Id, new DateTime(2015, 3, 22), "mileage",                      exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 23), "dinner",                       exchangeRateA, 60.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets",           exchangeRateB, 250.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast",                    exchangeRateB, 30.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 23), "Uber",                         exchangeRateB, 20.00M, exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 24), "Uber",                         exchangeRateA, 30.00M,  exchangeRateA, 0.00M, "4500********9975"),
               factory.Transaction(user.Id, new DateTime(2015, 3, 24), "Credit Test",                  exchangeRateA, 0.00M, exchangeRateA, 100.00M, "4500********9975")
            };
        }

        public ICollection<InvalidTransaction> BuildTestInvalidTransactions(ModelFactory factory, User user)
        {
            return new List<InvalidTransaction>
            {
                factory.InvalidTransaction(null, user.Id, "2015, 3, 22", "mileage",                      "USD", "100.00", "USD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
                factory.InvalidTransaction(null, user.Id, "2015, 3, 23", "dinner",                       "USD", "60.00",  "CAD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
                factory.InvalidTransaction(null, user.Id, "2015, 3, 21", "convention tickets",           "CAD", "250.00", "CAD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
                factory.InvalidTransaction(null, user.Id, "2015, 3, 21", "Best Western stay (3 nights)", "USD", "100.00", "CAD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
                factory.InvalidTransaction(null, user.Id, "2015, 3, 22", "breakfast",                    "CAD", "30.00",  "CAD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
                factory.InvalidTransaction(null, user.Id, "2015, 3, 23", "Uber",                         "CAD", "20.00",  "CAD", "0.00", "The date provided is in an invalid format", "4500********9975", "08/21"),
            };
        }
    }
}
