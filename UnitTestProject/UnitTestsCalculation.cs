using SoftwareSystemDesignApp;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsCalculation
    {
        private const string VERSION_NUMBER = "Version number: 1.1.2.";
        private readonly List<double> EMPTY_SEQUENCE_DATA = new List<double>();
        private readonly List<double> CORRECT_SEQUENCE_DATA = new List<double> { -1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7, 8.8, 9.9, 10.10 };
        private const string SEQUENCE_DATA_TO_DISPLAY = "-1,10; 2,20; 3,30; 4,40; 5,50; 6,60; 7,70; 8,80; 9,90; 10,10";

        [TestMethod]
        public void VerifyCallVersionNumber()
        {
            Assert.AreEqual(VERSION_NUMBER, Calculation.CallVersionNumber(), "Expected version number is not equal to actual.");
        }

        [TestMethod]
        [DataRow("10", true, DisplayName = "Correct data")]
        [DataRow("0010", true, DisplayName = "Correct data with zero's at first place")]
        [DataRow("2147483648", false, DisplayName = "Number more than INT32")]
        [DataRow("12.1", false, DisplayName = "Number with point")]
        [DataRow("66,37", false, DisplayName = "Number with coma")]
        [DataRow("0", false, DisplayName = "Zero")]
        [DataRow("-17", false, DisplayName = "Negative number")]
        [DataRow("36-", false, DisplayName = "Negative number in different format")]
        [DataRow("dfsdfs", false, DisplayName = "String without numbers")]
        [DataRow("123weef312/'/;", false, DisplayName = "String with different symbols")]
        [DataRow(",./?'%$^@!#;:\\*-()+-*`", false, DisplayName = "String with only specsymbols")]
        [DataRow("dfsdfs", false, DisplayName = "String without numbers")]
        public void VerifyIsNumberCorrect(string number, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, Calculation.IsNumberCorrect(number), "Method returns wrong result.");         
        }

        [TestMethod]
        public void VerifyGetSequnceResultsEmpty()
        {
            Assert.AreEqual("", Calculation.GetSequnceResults(EMPTY_SEQUENCE_DATA));
        }

        [TestMethod]
        public void VerifyGetSequnceResults()
        {
            Assert.AreEqual(SEQUENCE_DATA_TO_DISPLAY, Calculation.GetSequnceResults(CORRECT_SEQUENCE_DATA));
        }

        [TestMethod]
        [DataRow("(n*2+2/n*(1+5))/2", true, DisplayName = "Correct data 1")]
        [DataRow("n+2", true, DisplayName = "Correct data 2")]
        [DataRow("((n*3", true, DisplayName = "Data with icorrect number of brackets")]
        [DataRow("17n+1", true, DisplayName = "Data with incorect sequnce")] // Should be fixed
        [DataRow("(mn*2+2/nm*(1+5))/2", false, DisplayName = "Data with wrong variable")]    
        [DataRow("13*{n-6}", false, DisplayName = "Data with incorect symbols")]
        [DataRow("", false, DisplayName = "Empty sequence")]      
        public void VerifyIsSequnceCorrect(string sequence, bool expectedResult)
        {
            Assert.AreEqual(expectedResult, Calculation.IsSequnceCorrect(sequence), "Method returns wrong result.");
        }
    }
}
