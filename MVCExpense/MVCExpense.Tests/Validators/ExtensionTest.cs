namespace MVCExpense.Tests.Validators
{
    using System;

    using ExpenseModel.Validation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Validation.Properties;

    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void TestDateTimeStringValidationExtensionOnValidDate()
        {
            Assert.AreEqual("1995-05-08 12:00:00".IsValidOldDateTime(), null);
        }

        [TestMethod]
        public void TestDateTimeStringValidationExtensionReturnsCorrectError()
        {
            Assert.AreEqual("198/12:14 129900".IsValidOldDateTime(), Resources.InvalidDateFormat);
            Assert.AreEqual("1989-12-14 12:00:00".IsValidOldDateTime(), Resources.InvalidDateBelowMinimum);
            Assert.AreEqual(DateTime.Now.AddDays(1).ToShortDateString().IsValidOldDateTime(), Resources.InvalidDateAboveMaximum);
        }

        [TestMethod]
        public void TestMaskedCCNumberValidationExtensionOnValidMaskedCC()
        {
            Assert.AreEqual("4500********1253".IsValidMaskedCC(), null);
        }

        [TestMethod]
        public void TestMaskedCCNumberValidationExtensionReturnsCorrectError()
        {
            Assert.AreEqual("4500*******1253".IsValidMaskedCC(), Resources.InvalidCCLength);
            Assert.AreEqual("4500*********1253".IsValidMaskedCC(), Resources.InvalidCCLength);
            Assert.AreEqual("45ab********1253".IsValidMaskedCC(), Resources.InvalidCCNumber);
            Assert.AreEqual("4500********1ef3".IsValidMaskedCC(), Resources.InvalidCCNumber);
            Assert.AreEqual("45001*******1253".IsValidMaskedCC(), Resources.CCMustBeMasked);
            Assert.AreEqual("4500*******11253".IsValidMaskedCC(), Resources.CCMustBeMasked);
            Assert.AreEqual("4*5*0*0*1*3***12".IsValidMaskedCC(), Resources.InvalidCCNumber);
            Assert.AreEqual("4500123456781253".IsValidMaskedCC(), Resources.CCMustBeMasked);
        }
    }
}
