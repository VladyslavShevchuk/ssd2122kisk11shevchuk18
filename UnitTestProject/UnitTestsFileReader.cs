using SoftwareSystemDesignApp;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestsFileReader
    {
        private const string PATH_TO_FILE_WITH_PROHIBITED_EXTENTION = "../../FileReaderTestData/FILE_WITH_PROHIBITED_EXTENTION.txt";
        private const string PATH_TO_FILE_PATTERN = "../../FileReaderTestData/";
        private const string EXPECTED_DATA_FROM_FILES = "(n*2+2/n*(1+5))/2";

        // Verify reading data from prohibited file extencion
        [TestMethod]
        public void TryToReadDataFromFileWithProhibitedExtentionINI()
        {
            Assert.AreEqual("", FileReader.ReadFromINI(PATH_TO_FILE_WITH_PROHIBITED_EXTENTION));
        }

        [TestMethod]
        [ExpectedException(typeof(System.Xml.XmlException))]
        public void TryToReadDataFromFileWithProhibitedExtentionXML()
        {
            FileReader.ReadFromXML(PATH_TO_FILE_WITH_PROHIBITED_EXTENTION);
        }

        [TestMethod]
        public void TryToReadDataFromFileWithProhibitedExtentionJSON()
        {
            Assert.AreEqual("", FileReader.ReadFromJSON(PATH_TO_FILE_WITH_PROHIBITED_EXTENTION));
        }

        // Verify reading wrong data
        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "INI_with_incorrect_data.ini", DisplayName = "Read incorrect data from INI")]
        public void ReadDataFromFileWithIncorrectDataINI(string filePath)
        {
            string data = FileReader.ReadFromINI(filePath);
            Assert.AreEqual(data, "");
        }

        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "XML_with_incorrect_data.xml", DisplayName = "Read incorrect data from XML")]
        public void ReadDataFromFileWithIncorrectDataXML(string filePath)
        {
            string data = FileReader.ReadFromXML(filePath);
            Assert.AreEqual(data, "");
        }

        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "JSON_with_incorrect_data.json", DisplayName = "Read incorrect data from JSON")]
        public void ReadDataFromFileWithIncorrectDataJSON(string filePath)
        {
            string data = FileReader.ReadFromJSON(filePath);
            Assert.AreEqual(data, "");
        }

        // Verify reading correct data
        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "INI_with_correct_data.ini", DisplayName = "Read correct data from INI")]
        public void ReadDataFromFileWithCorrectDataINI(string filePath)
        {
            string data = FileReader.ReadFromINI(filePath);
            Assert.AreEqual(data, EXPECTED_DATA_FROM_FILES);
        }

        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "XML_with_correct_data.xml", DisplayName = "Read correct data from XML")]
        public void ReadDataFromFileWithCorrectDataXML(string filePath)
        {
            string data = FileReader.ReadFromXML(filePath);
            Assert.AreEqual(data, EXPECTED_DATA_FROM_FILES);
        }

        [TestMethod]
        [DataRow(PATH_TO_FILE_PATTERN + "JSON_with_correct_data.json", DisplayName = "Read correct data from JSON")]
        public void ReadDataFromFileWithCorrectDataJSON(string filePath)
        {
            string data = FileReader.ReadFromJSON(filePath);
            Assert.AreEqual(data, EXPECTED_DATA_FROM_FILES);
        }
    }
}
