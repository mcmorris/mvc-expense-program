namespace MVCExpense.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Tests.Models;

    [TestClass]
    public class TestBase
    {
        // these are needed on every test
        protected TestHelper Helper;

        protected ModelFactory Model;

        [TestInitialize]
        public void TestInitialize()
        {
            this.Helper = new TestHelper();
            this.Model = new ModelFactory();
        }
    }
}
