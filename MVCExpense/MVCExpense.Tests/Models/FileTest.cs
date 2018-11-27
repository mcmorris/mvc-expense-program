namespace MVCExpense.Tests.Model
{
    using System.Text;

    using ExpenseModel;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileTest : TestBase
    {
        [TestMethod]
        public void TestFileUpload()
        {
            // TODO:
        }

        [TestMethod]
        public void TestFileDownload()
        {
            // TODO:
        }

        [TestMethod]
        public void TestFileValidation()
        {
            var user = new User("smenke", null, "Sikke Menke", "HR", null);
            var stringAbove255 = this.Helper.CreateStringLongerThan(256);

            var fileTestA = this.Model.File(null, "File.png", @"C:\PathHere\File.png", "Image", 2);
            Assert.AreEqual(fileTestA.IsValid, false);

            var fileTestB = this.Model.File(user, null, @"C:\PathHere\File.png", "Image", 2);
            Assert.AreEqual(fileTestB.IsValid, false);

            var fileTestC = this.Model.File(user, stringAbove255, @"C:\PathHere\File.png", "Image", 2);
            Assert.AreEqual(fileTestC.IsValid, false);

            var fileTestD = this.Model.File(user, "File.png", null, "Image", 2);
            Assert.AreEqual(fileTestD.IsValid, false);
            
            var fileTestE = this.Model.File(user, "File.png", stringAbove255, "Image", 2);
            Assert.AreEqual(fileTestE.IsValid, false);

            var fileTestF = this.Model.File(user, "File.png", @"C:\PathHere\File.png", stringAbove255, 2);
            Assert.AreEqual(fileTestF.IsValid, false);

            var fileTestG = this.Model.File(user, "File.png", @"C:\PathHere\File.png", "Image", 2);
            Assert.AreEqual(fileTestG.IsValid, true);
        }
    }
}
