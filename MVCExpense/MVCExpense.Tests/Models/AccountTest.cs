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
            var account = new Account(user, "4500********1232", 2021, 8);
            var usd = new ISO4217Currency("USD", 2, "United States Dollars", null);

            var exchangeRate = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);

            account.Transactions = new List<Transaction>
            {
                new Transaction(user.Id, DateTime.Now, "Test Description A", exchangeRate, 100M, exchangeRate, 0M, account.MaskedCardNumber),
                new Transaction(user.Id, DateTime.Now, "Test Description B", exchangeRate, 0M, exchangeRate, 150M, account.MaskedCardNumber),
                new Transaction(user.Id, DateTime.Now, "Test Description C", exchangeRate, 0M, exchangeRate, 100M, account.MaskedCardNumber),
            };

            user.Accounts.Add(account);

            Assert.AreEqual(user.Accounts.Count, 1);
            Assert.AreEqual(user.Accounts.First().DebitSum, 100.00M);
            Assert.AreEqual(user.Accounts.First().CreditSum, 250.00M);
            Assert.AreEqual(user.Accounts.First().Balance, -150.00M);
        }

        [TestMethod]
        // CC Validation is tested in ExtensionTests.cs
        public void TestAccountValidation()
        {
            var user = new User("ddea", null, "Doug Dea", "Management", null);

            // Expiry year and month are gibberish
            var testAccountA = new Account(user, "4500********3198", 1, 1);
            Assert.AreEqual(testAccountA.IsValid, false);

            // Expiry user is invalid
            var testAccountB = new Account(null, "4500********3198", 2021, 8);
            Assert.AreEqual(testAccountB.IsValid, false);

            // Account is valid
            var testAccountC = new Account(user, "4500********3198", 2021, 8);
            Assert.AreEqual(testAccountC.IsValid, true);
        }
    }
}
