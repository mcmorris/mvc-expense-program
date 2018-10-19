// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeControllerTest.cs" company="Michael Morris">
//  (c) Michael Morris, 2018.
// </copyright>
// <summary>Defines tests for the HomeController class</summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MVCExpense.Tests.Controllers
{
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MVCExpense.Controllers;

    /// <summary>
    /// Test class to test all functions of HomeController.
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        /// <summary>
        /// Test index function on HomeController using Arrange, Act Assert.
        /// </summary>
        [TestMethod]
        public void Index()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Test about function on HomeController using Arrange, Act Assert.
        /// </summary>
        [TestMethod]
        public void About()
        {
            var controller = new HomeController();
            var result = controller.About() as ViewResult;
            Assert.AreEqual("Your application description page.", result?.ViewBag.Message);
        }

        /// <summary>
        /// Test contact function on HomeController using Arrange, Act Assert.
        /// </summary>
        [TestMethod]
        public void Contact()
        {
            var controller = new HomeController();
            var result = controller.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
