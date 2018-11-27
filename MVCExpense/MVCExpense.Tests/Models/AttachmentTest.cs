namespace MVCExpense.Tests.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AttachmentTest : TestBase
    {
        [TestMethod]
        public void TestAttachmentCoveringASingleStatement()
        {
            
            var user = this.Model.User("pcressac", "Platt Cressac", "Finance");
            var file = this.Model.File(user, "TestAttachment.png", @"C:\Path\TestAttachment.png", "Image", 8);
            var statement = this.Model.Statement();
            var attachment = this.Model.Attachment(file, null, StatusTypes.Pending, statement);
            Assert.AreEqual(attachment.StatementCovered, statement);
            Assert.AreEqual(attachment.TransactionsCovered, null);
        }

        [TestMethod]
        public void TestAttachmentCoveringASingleTransaction()
        {
            var user       = this.Model.User("lzantir", "L'Kola Zantir", "Finance");
            var file       = this.Model.File(user, "TestAttachmentB.png", @"C:\Path\TestAttachmentB.png", "Image", 12);
            var statement  = this.Model.Statement();
            var bankImport = this.Model.BankImport(statement, null, user, "TD", StatusTypes.Pending);

            var exchangeRate = this.Model.ExchangeRate(DateTime.Now, 1.0M, this.Model.USD, this.Model.USD);
            bankImport.Transactions = new List<Transaction>
            {
                this.Model.Transaction(user.Id, new DateTime(2015, 3, 22), "mileage", exchangeRate, 100.00M, exchangeRate, 0.00M, "4500********9975"),
            };

            var attachment = this.Model.Attachment(file, null, StatusTypes.Pending, statement, bankImport.Transactions);
            Assert.AreEqual(attachment.StatementCovered, statement);
            Assert.AreEqual(attachment.TransactionsCovered, bankImport.Transactions);
            Assert.AreEqual(attachment.TransactionsCovered.Count, 1);
        }

        [TestMethod]
        public void TestAttachmentCoveringMultipleNonSequentialTransactions()
        {
            var user       = this.Model.User("lzantir", "L'Kola Zantir", "Finance");
            var file       = this.Model.File(user, "TestAttachmentB.png", @"C:\Path\TestAttachmentB.png", "Image", 12);
            var statement  = this.Model.Statement();
            var bankImport = this.Model.BankImport(statement, file, user, "TD", StatusTypes.Pending);

            var usd          = this.Model.ISO4217Currency("USD", 2, "United States Dollars", null);
            var cad = this.Model.ISO4217Currency("CAD", 2, "Canadian Dollars", null);
            var exchangeRateA = this.Model.ExchangeRate(DateTime.Now, 1.0M, usd, usd);
            var exchangeRateB = this.Model.ExchangeRate(DateTime.Now, 1.3M, cad, usd);

            var attachmentTransactionA = this.Model.Transaction(user.Id, new DateTime(2015, 3, 22), "mileage", exchangeRateA, 100.00M, exchangeRateA, 0.00M, "4500********9975");
            var transactionB = this.Model.Transaction(user.Id, new DateTime(2015, 3, 23), "dinner", exchangeRateA, 60.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionB = this.Model.Transaction(user.Id, new DateTime(2015, 3, 21), "convention tickets", exchangeRateB, 250.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionC = this.Model.Transaction(user.Id, new DateTime(2015, 3, 21), "Best Western stay (3 nights)", exchangeRateA, 100.00M, exchangeRateB, 0.00M, "4500********9975");
            var transactionD = this.Model.Transaction(user.Id, new DateTime(2015, 3, 22), "breakfast", exchangeRateB, 30.00M, exchangeRateB, 0.00M, "4500********9975");
            var transactionE = this.Model.Transaction(user.Id, new DateTime(2015, 3, 23), "Uber", exchangeRateB, 20.00M, exchangeRateB, 0.00M, "4500********9975");
            var attachmentTransactionF = this.Model.Transaction(user.Id, new DateTime(2015, 3, 24), "Uber", exchangeRateA, 30.00M, exchangeRateA, 0.00M, "4500********9975");

            var attachment = this.Model.Attachment(file, null, StatusTypes.Pending, statement, new List<Transaction> { attachmentTransactionA, attachmentTransactionB, attachmentTransactionC });
            attachment.AddTransaction(attachmentTransactionF);

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
            var user = this.Model.User("tbarry", "Tilly Barry", "HR");

            // Statement cannot be null.
            var file       = this.Model.File(user, "TestAttachment.png", @"C:\Path\TestAttachment.png", "Image", 8);
            var statement  = this.Model.Statement();
            var testAttachmentA = this.Model.Attachment(file, "Missing statement", StatusTypes.Pending, null);
            Assert.AreEqual(testAttachmentA.IsValid, false);

            // File cannot be null.
            var testAttachmentB = this.Model.Attachment(null, "Missing file", StatusTypes.Pending, statement);
            Assert.AreEqual(testAttachmentB.IsValid, false);

            var testAttachmentC = this.Model.Attachment(file, null, StatusTypes.Pending, statement);
            Assert.AreEqual(testAttachmentC.IsValid, true);
        }
    }
}
