namespace MVCExpense.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AttachmentTest
    {
        [TestMethod]
        public void TestAttachmentCoveringASingleStatement()
        {
            
            var user = new User("pcressac", null, "Platt Cressac", "Finance", null);
            var file = new File(user, "TestAttachment.png", @"C:\Path\TestAttachment.png", "Image", null, 8);
            var statement = new Statement(DateTime.Now, StatusTypes.Pending);
            var attachment = new Attachment(file, null, StatusTypes.Pending, statement, null);
            Assert.AreEqual(attachment.StatementCovered, statement);
            Assert.AreEqual(attachment.TransactionsCovered, null);
        }

        [TestMethod]
        public void TestAttachmentCoveringASingleTransaction()
        {
            var user       = new User("lzantir", null, "L'Kola Zantir", "Finance", null);
            var file       = new File(user, "TestAttachmentB.png", @"C:\Path\TestAttachmentB.png", "Image", null, 12);
            var statement  = new Statement(DateTime.Now, StatusTypes.Pending);
            var bankImport = new BankImport(statement, user, "TD", StatusTypes.Pending);

            var usd = new ISO4217Currency("USD", 2, "United States Dollars", null);
            var exchangeRate = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            bankImport.Transactions = new List<Transaction>
            {
                new Transaction(user.Id, new DateTime(2015, 3, 22), "mileage", exchangeRate, 100.00M, exchangeRate, 0.00M, "4500********9975"),
            };

            var attachment = new Attachment(file, null, StatusTypes.Pending, statement, bankImport.Transactions);
            Assert.AreEqual(attachment.StatementCovered, statement);
            Assert.AreEqual(attachment.TransactionsCovered, bankImport.Transactions);
            Assert.AreEqual(attachment.TransactionsCovered.Count, 1);
        }

        [TestMethod]
        public void TestAttachmentCoveringMultipleNonSequentialTransactions()
        {
            var user       = new User("lzantir", null, "L'Kola Zantir", "Finance", null);
            var file       = new File(user, "TestAttachmentB.png", @"C:\Path\TestAttachmentB.png", "Image", null, 12);
            var statement  = new Statement(DateTime.Now, StatusTypes.Pending);
            var bankImport = new BankImport(statement, user, "TD", StatusTypes.Pending);

            var usd          = new ISO4217Currency("USD", 2, "United States Dollars", null);
            var cad = new ISO4217Currency("CAD", 2, "Canadian Dollars", null);
            var exchangeRateA = new ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            var exchangeRateB = new ExchangeRate(DateTime.Now, 1.3M, cad, usd);

            var attachmentTransactionA = new Transaction(user.Id, new DateTime(2015, 3, 22), "mileage", exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975");
            var transactionB = new Transaction(user.Id, new DateTime(2015, 3, 23), "dinner", exchangeRateA, 60.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionB = new Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets", exchangeRateB, 250.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionC = new Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateB, 0.00M, "4500********9975");
            var transactionD = new Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast", exchangeRateB, 30.00M, exchangeRateB, 0.00M, "4500********9975");
            var transactionE = new Transaction(user.Id, new DateTime(2015, 3, 23), "Uber", exchangeRateB, 20.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionF = new Transaction(user.Id, new DateTime(2015, 3, 24), "Uber", exchangeRateA, 30.00M, exchangeRateA, 0.00M, "4500********9975");

            var attachment = new Attachment(file, null, StatusTypes.Pending, statement, new List<Transaction> { attachmentTransactionA, attachmentTransactionB, attachmentTransactionC });
            attachment.AppendTransaction(attachmentTransactionF);

            Assert.AreEqual(attachment.TransactionsCovered.Count, 4);
            Assert.AreEqual(attachment.CoversTransaction(attachmentTransactionA), true);
            Assert.AreEqual(attachment.CoversTransaction(attachmentTransactionB), true);
            Assert.AreEqual(attachment.CoversTransaction(attachmentTransactionC), true);
            Assert.AreEqual(attachment.CoversTransaction(attachmentTransactionF), true);
            Assert.AreEqual(attachment.CoversTransaction(transactionB), false);
            Assert.AreEqual(attachment.CoversTransaction(transactionD), false);
            Assert.AreEqual(attachment.CoversTransaction(transactionE), false);
        }

        [TestMethod]
        public void TestAttachmentValidation()
        {
            var user = new User("tbarry", null, "Tilly Barry", "HR", null);

            // Statement cannot be null.
            var file       = new File(user, "TestAttachment.png", @"C:\Path\TestAttachment.png", "Image", null, 8);
            var statement  = new Statement(DateTime.Now, StatusTypes.Pending);
            var testAttachmentA = new Attachment(file, "Missing statement", StatusTypes.Pending, null, null);
            Assert.AreEqual(testAttachmentA.IsValid, false);

            // File cannot be null.
            var testAttachmentB = new Attachment(null, "Missing file", StatusTypes.Pending, statement, null);
            Assert.AreEqual(testAttachmentB.IsValid, false);

            var testAttachmentC = new Attachment(file, null, StatusTypes.Pending, statement, null);
            Assert.AreEqual(testAttachmentC.IsValid, true);
        }
    }
}
