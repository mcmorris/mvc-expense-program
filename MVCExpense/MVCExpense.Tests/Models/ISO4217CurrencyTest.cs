using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCExpense.Tests.Models
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ISO4217CurrencyTest : TestBase
    {
        [TestMethod]
        public void TestISO4217CurrencyValidation()
        {

        }
        /*
         *         [Key][Required][MaxLength(3)]
        public string        Id             { get; set; }

        [Required][Range(0, 4)]
        public int           Exponent       { get; set; }

        [Required][MaxLength(255)][DataType(DataType.Text)][Index("IDX_CurrencyName")]
        public string        Name           { get; set; }

        [DataType(DataType.DateTime)][DateRangeBetweenYear2000AndNow]
        public DateTime?     WithdrawalDate { get; set; }

         */
    }
}
