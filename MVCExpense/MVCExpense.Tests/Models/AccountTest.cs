namespace MVCExpense.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestAccountCalculatedFields()
        {
            var user = new User("ddea", null, "Doug Dea", "Management", null);
            var account = new Account(user, "4500********1232", new DateTime(2021, 8, 15));
            var usd = new ISO4217Currency("USD", 2, "United States Dollars", null);

            var exchangeRate = new ExchangeRate(100, DateTime.Now, 1.0M, usd, usd);
            account.Transactions = new List<Transaction>
            {
                new Transaction(101, user.Id, DateTime.Now,
                    "Test Description", new Money(100, exchangeRate, DateTime.Now, 100.00M, 100.00M),
                    new Money(0, exchangeRate, DateTime.Now, 0M, 0M), account.MaskedCardNumber),
                new Transaction(101, user.Id, DateTime.Now,
                    "Test Description", new Money(0, exchangeRate, DateTime.Now, 0M, 0M), new Money(101, exchangeRate, DateTime.Now, 150.00M, 150.00M),
                    account.MaskedCardNumber),
                new Transaction(102, user.Id, DateTime.Now,
                    "Test Description", new Money(0, exchangeRate, DateTime.Now, 0M, 0M), new Money(102, exchangeRate, DateTime.Now, 100.00M, 100.00M),
                    account.MaskedCardNumber),
            };

            user.Accounts.Add(account);

            Assert.AreEqual(user.Accounts.Count, 1);
            Assert.AreEqual(user.Accounts.First().DebitSum, 100.00M);
            Assert.AreEqual(user.Accounts.First().CreditSum, 250.00M);
            Assert.AreEqual(user.Accounts.First().Balance, -150.00M);
        }

        [TestMethod]
        public void TestAccountTrackingUpdates()
        {

        }

        [TestMethod]
        public void TestAccountValidation()
        {

        }

        [TestMethod]
        public void TestCCValidationWithMasking()
        {

        }
    }
}
