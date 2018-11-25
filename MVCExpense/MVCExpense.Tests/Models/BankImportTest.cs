namespace MVCExpense.Tests.Model
{
    using System;
    using System.Linq;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BankImportTest
    {

        [TestMethod]
        public void TestBankImportAddingAndRemovingTransactions()
        {
            var user = new User("provanperä", null, "Pentti Rovanperä", "Management", null);
            Assert.AreEqual(user.FullName, "Pentti Rovanperä");

            var statement = new Statement(DateTime.Now, StatusTypes.Pending);
            var bankImport = new BankImport(statement, user, "TD", StatusTypes.Pending);

            var usd           = new ISO4217Currency("USD", 2, "United States Dollars", null);
            var cad           = new ISO4217Currency("CAD", 2, "Canadian Dollars",      null);
            var exchangeRateA = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            var exchangeRateB = new ExchangeRate(DateTime.Now, 1.3M, cad, usd);

            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 22), "mileage", exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 23), "dinner", exchangeRateA, 60.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets", exchangeRateB, 250.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast", exchangeRateB, 30.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 23), "Uber", exchangeRateB, 20.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.AddTransaction(new Transaction(user.Id, new DateTime(2015, 3, 24), "Uber", exchangeRateA, 30.00M, exchangeRateA, 0.00M, "4500********9975"));
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.StatementId, statement.Id);
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.Statement, statement);
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.BankImportId, bankImport.Id);
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.BankImport, bankImport);

            Assert.AreEqual(bankImport.Transactions.Count, 7);

            bankImport.RemoveTransaction(new Transaction(user.Id, new DateTime(2015, 3, 23), "dinner", exchangeRateA, 60.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.RemoveTransaction(new Transaction(user.Id, new DateTime(2015, 3, 23), "Uber", exchangeRateB, 20.00M, exchangeRateB, 0.00M, "4500********9975"));
            bankImport.RemoveTransaction(new Transaction(user.Id, new DateTime(2015, 3, 24), "Uber", exchangeRateA, 30.00M, exchangeRateA, 0.00M, "4500********9975"));
            Assert.AreEqual(bankImport.Transactions.Count, 4);
        }

        [TestMethod]
        public void TestBankImportAddingAndRemovingInvalidTransactions()
        {

        }

        [TestMethod]
        public void TestBankImportCalculatedFields()
        {

        }

        [TestMethod]
        public void TestBankImportValidation()
        {

        }

    }
}
