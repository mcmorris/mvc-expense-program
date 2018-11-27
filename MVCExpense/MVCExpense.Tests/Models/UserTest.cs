namespace MVCExpense.Tests.Model
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserTest : TestBase
    {
        [TestMethod]
        public void TestUserAddAndRemoveAccounts()
        {

        }

        [TestMethod]
        public void TestUserCannotAddCreditCardActiveOnAnotherUser()
        {

        }

        [TestMethod]
        public void TestUserCanAddACreditCardThatIsInactive()
        {

        }

        [TestMethod]
        public void TestUserCanReAddTheSameCreditCard()
        {

        }

        [TestMethod]
        public void TestUserCalculatedFields()
        {

        }

        [TestMethod]
        public void TestUserValidation()
        {
            var userTestA = this.Model.User("bacreson", "Bob Acreson", "HR");
            Assert.AreEqual(userTestA.IsValid, true);

            var userTestB = this.Model.User(null, "Bob Acreson", "HR");
            Assert.AreEqual(userTestB.IsValid, false);

            var userTestC = this.Model.User(this.Helper.CreateStringLongerThan(257), "Bob Acreson", "HR");
            Assert.AreEqual(userTestC.IsValid, false);

            var userTestE = this.Model.User("bacreson", "Bob Acreson", "HR");
            userTestE.JobTitle = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(userTestE.IsValid, false);

            var userTestF = this.Model.User("bacreson", "Bob Acreson", "HR");
            userTestF.DepartmentName = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(userTestF.IsValid, false);

            var userTestG = this.Model.User("bacreson", null, "HR");
            Assert.AreEqual(userTestG.IsValid, false);
            userTestG.FullName = this.Helper.CreateStringLongerThan(256);
            Assert.AreEqual(userTestG.IsValid, false);
        }
    }
}
