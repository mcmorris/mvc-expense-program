namespace MVCExpense.Tests.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class InvalidTransactionTest : TestBase
    {
        [TestMethod]
        public void TestInvalidTransactionValidation()
        {
            var user = this.Model.User("id", "fullName", "department");
            var bankImport = new BankImport(
                new Statement(DateTime.Now, StatusTypes.Pending),
                user,
                "CIBC",
                StatusTypes.Pending
            );

            var invalidTestA = this.Model.InvalidTransaction(
                bankImport,
                user.Id,
                "2015/07/01 12:00:00",
                "Test description",
                "USD",
                "10.00",
                "USD",
                "0.00",
                "This is an error message",
                "4500********5397",
                "06/21"
            );
            Assert.AreEqual(invalidTestA.IsValid, true);

            var invalidTestB = this.Model.InvalidTransaction(
                bankImport,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );
            Assert.AreEqual(invalidTestB.IsValid, true);

            var invalidTestC = invalidTestA;
            invalidTestC.UserName = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC = invalidTestA;
            invalidTestC.Description = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC             = invalidTestA;
            invalidTestC.DebitCurrencyCode = this.Helper.CreateStringLongerThan(4);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC            = invalidTestA;
            invalidTestC.DebitValue = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC                   = invalidTestA;
            invalidTestC.CreditCurrencyCode = this.Helper.CreateStringLongerThan(4);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC            = invalidTestA;
            invalidTestC.CreditValue = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC             = invalidTestA;
            invalidTestC.Issue = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC       = invalidTestA;
            invalidTestC.MaskedCardNumber = this.Helper.CreateStringLongerThan(17);
            Assert.AreEqual(invalidTestC.IsValid, false);

            invalidTestC                  = invalidTestA;
            invalidTestC.CardExpiry = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(invalidTestC.IsValid, false);
        }
    }
}
