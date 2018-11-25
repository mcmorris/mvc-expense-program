namespace MVCExpense.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
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

            var transactions = new List<Transaction> { 
                new Transaction(user.Id, new DateTime(2015, 3, 22), "mileage",                      exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 23), "dinner",                       exchangeRateA, 60.00M,  exchangeRateB, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets",           exchangeRateB, 250.00M, exchangeRateB, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateB, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast",                    exchangeRateB, 30.00M,  exchangeRateB, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 23), "Uber",                         exchangeRateB, 20.00M,  exchangeRateB, 0.00M, "4500********9975"),
                new Transaction(user.Id, new DateTime(2015, 3, 24), "Uber",                         exchangeRateA, 30.00M,  exchangeRateA, 0.00M, "4500********9975"),
            };

            bankImport.AddTransactions(transactions);
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.Statement, statement);
            Assert.AreEqual(bankImport.Transactions.FirstOrDefault()?.BankImport, bankImport);

            Assert.AreEqual(bankImport.Transactions.Count, 7);

            bankImport.RemoveTransaction(transactions[0]);
            bankImport.RemoveTransaction(transactions[5]);
            bankImport.RemoveTransaction(transactions[6]);
            Assert.AreEqual(bankImport.Transactions.Count, 4);
        }

        [TestMethod]
        public void TestBankImportAddingAndRemovingInvalidTransactions()
        {
            var user = new User("shimozukushichi", null, "Shimozukushichi", "HR", null);
            Assert.AreEqual(user.FullName, "Shimozukushichi");

            var statement = new Statement(DateTime.Now, StatusTypes.Pending);
            var bankImport = new BankImport(statement, user, "TD", StatusTypes.Pending);

            var usd = new ISO4217Currency("USD", 2, "United States Dollars", null);
            var cad = new ISO4217Currency("CAD", 2, "Canadian Dollars", null);
            var exchangeRateA = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            var exchangeRateB = new ExchangeRate(DateTime.Now, 1.3M, cad, usd);

            var transactions = new List<InvalidTransaction> {
                new InvalidTransaction(null, user.Id, "2015, 3, 22", "mileage", "USD", "100.00", "USD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 23", "dinner", "USD", "60.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 21", "convention tickets", "CAD", "250.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 21", "Best Western stay (3 nights)", "USD", "100.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 22", "breakfast", "CAD", "30.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 23", "Uber", "CAD", "20.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 24", "Uber", "USD", "30.00", "USD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
            };

            bankImport.AddInvalidTransactions(transactions);
            Assert.AreEqual(bankImport.InvalidTransactions.FirstOrDefault()?.BankImport, bankImport);

            Assert.AreEqual(bankImport.InvalidTransactions.Count, 7);

            bankImport.RemoveInvalidTransaction(transactions[0]);
            bankImport.RemoveInvalidTransaction(transactions[5]);
            bankImport.RemoveInvalidTransaction(transactions[6]);
            Assert.AreEqual(bankImport.InvalidTransactions.Count, 4);
        }

        [TestMethod]
        public void TestBankImportCalculatedFields()
        {
            var user = new User("kmozes", null, "Kardos Mozes", "HR", null);

            var statement  = new Statement(DateTime.Now, StatusTypes.Pending);
            var bankImport = new BankImport(statement, user, "CIBC", StatusTypes.Pending);

            var usd           = new ISO4217Currency("USD", 2, "United States Dollars", null);
            var cad           = new ISO4217Currency("CAD", 2, "Canadian Dollars",      null);
            var exchangeRateA = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            var exchangeRateB = new ExchangeRate(DateTime.Now, 1.3M, cad, usd);

            var transactions = new List<Transaction> {
               new Transaction(user.Id, new DateTime(2015, 3, 22), "mileage",                      exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 23), "dinner",                       exchangeRateA, 60.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets",           exchangeRateB, 250.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast",                    exchangeRateB, 30.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 23), "Uber",                         exchangeRateB, 20.00M, exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 24), "Uber",                         exchangeRateA, 30.00M,  exchangeRateA, 0.00M, "4500********9975"),
               new Transaction(user.Id, new DateTime(2015, 3, 24), "Credit Test",                  exchangeRateA, 0.00M, exchangeRateA, 100.00M, "4500********9975")

            };

            var invalidTransactions = new List<InvalidTransaction> {
                new InvalidTransaction(null, user.Id, "2015, 3, 22", "mileage",                      "USD", "100.00", "USD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 23", "dinner",                       "USD", "60.00",  "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 21", "convention tickets",           "CAD", "250.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 21", "Best Western stay (3 nights)", "USD", "100.00", "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 22", "breakfast",                    "CAD", "30.00",  "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
                new InvalidTransaction(null, user.Id, "2015, 3, 23", "Uber",                         "CAD", "20.00",  "CAD", "0.00", new ValidationResult("The date provided is in an invalid format"), "4500********9975", "08/21"),
            };

            bankImport.AddTransactions(transactions);
            bankImport.AddInvalidTransactions(invalidTransactions);

            
            Assert.AreEqual(bankImport.CoversFrom, new DateTime(2015, 3, 21));
            Assert.AreEqual(bankImport.CoversUntil, new DateTime(2015, 3, 24));
            Assert.AreEqual(bankImport.SuccessfulTransactionsCount, 8);
            Assert.AreEqual(bankImport.TransactionErrorsCount, 6);
            Assert.AreEqual(bankImport.TotalTransactionsCount, 14);

            // Manually perform calculation and apply exchange rate conversions to test correctness.
            var debitSum = 100.00M + 60.00M + (250.00M * 1.3M) + 100.00M + (30.00M * 1.3M) + (20.00M * 1.3M) + 30.00M;
            var creditSum = 100.00M;

            Assert.AreEqual(bankImport.DebitSum, debitSum);
            Assert.AreEqual(bankImport.CreditSum, creditSum);
            Assert.AreEqual(bankImport.Balance, debitSum - creditSum);
        }

        [TestMethod]
        public void TestBankImportValidation()
        {
            var statement = new Statement(DateTime.Now, StatusTypes.Pending);
            var user = new User("kmozes", null, "Kardos Mozes", "HR", null);

            var bankImportTestA = new BankImport(null, user, "CIBC", StatusTypes.Pending);
            Assert.AreEqual(bankImportTestA.IsValid, false);

            var bankImportTestB = new BankImport(statement, user, "Scotiabank", StatusTypes.Pending)
            {
                File = null
            };

            Assert.AreEqual(bankImportTestB.IsValid, false);

            var bankImportTestC = new BankImport(statement, null, "TD", StatusTypes.Pending);
            Assert.AreEqual(bankImportTestC.IsValid, false);

            var file = new File(user, "testFile.txt", @"C:\Upload\Path", "TXT", string.Empty, 2);
            var bankImportTestD = new BankImport(statement, user, "Scotiabank", StatusTypes.Pending)
            {
                Id = 100,
                File = file
            };
            Assert.AreEqual(bankImportTestD.IsValid, true);
        }

    }
}
