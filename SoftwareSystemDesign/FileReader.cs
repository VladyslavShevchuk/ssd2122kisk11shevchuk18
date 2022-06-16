using System;
using System.IO;
using IniParser;
using IniParser.Model;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace SoftwareSystemDesignApp
{
    // Class for reading data from files with .ini, .xml and .json extensions
    public static class FileReader
    {
        // Variables for files internal data path
        private const string FILES_SECTION_NAME = "DataModel";
        private const string FILES_TAGS_NAME = "Sequence";
        private const string FILES_TAGS_NUMBER = "Number";

        /// <summary>
        /// Choose implementation of file read method by it extension
        /// </summary>
        /// <param name="filePath">Path to file which shoul be readed</param>
        /// <param name="isSequence">Choose search criteria</param>
        /// <returns>Data from sequence file</returns>
        static public string ReadDataFromFile(string filePath, bool isSequence)
        {
            string tagNameSearch = isSequence ? FILES_TAGS_NAME : FILES_TAGS_NUMBER;
            string extension = Path.GetExtension(filePath);
            string data = null;
            // Handle errors of file reading
            try
            {           
                // Choose file reader implementation by file extension
                switch (extension)
                {
                    case ".ini":
                        data = ReadFromINI(filePath, tagNameSearch);
                        break;
                    case ".json":
                        data = ReadFromJSON(filePath, tagNameSearch);
                        break;
                    case ".xml":
                        data = ReadFromXML(filePath, tagNameSearch);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unknown file extencion. Please reenter file path or enter '-sf' command again to exit from this menu.");                   
                        break;
                }
                // Notify user that data in entered file wasn't found
                if (data == "")
                {
                    Console.WriteLine($"Section with '{tagNameSearch}' wasn't founded in this file. Please reenter file path.");
                }
                return data;
            }
            // Notify user about error when file reading
            catch
            {
                Console.Clear();
                Console.WriteLine("Error of reading data from file. Probably file not found. Please reenter file path.");
                return null;
            }
        }

        /// <summary>
        /// Read data from INI file
        /// </summary>
        /// <param name="pathINI">Path to INI file</param>
        /// <param name="searchTagCriteria">Tag with data</param>
        /// <returns>Data from INI file</returns>
        static public string ReadFromINI(string pathINI, string searchTagCriteria)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(pathINI);
            // Iterate through all the sections
            foreach (SectionData section in data.Sections)
            {
                if(section.SectionName == FILES_SECTION_NAME)
                {
                    foreach (KeyData key in section.Keys)
                    {
                        if (key.KeyName == searchTagCriteria)
                        {
                            return key.Value.Replace("\"", "");
                        }
                    }
                }                           
            }
            return ""; // return error if not find data at file format adress
        }

        /// <summary>
        /// Read data from JSON file
        /// </summary>
        /// <param name="pathJSON">Path to JSON file</param>
        /// <param name="searchTagCriteria">Tag with data</param>
        /// <returns>Data from JSON file</returns>
        static public string ReadFromJSON(string pathJSON, string searchTagCriteria)
        {
            // Read JSON directly from a file
            try
            {
                using (StreamReader file = File.OpenText(pathJSON))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    return json[searchTagCriteria].Value<string>();
                }
            }
            catch
            {
                return ""; // return error if not find data at file by format adress
            }     
        }

        /// <summary>
        /// Read data from XML file
        /// </summary>
        /// <param name="pathXML">Path to XML file</param>
        /// <param name="searchTagCriteria">Tag with data</param>
        /// <returns>Data from XML file</returns>
        static public string ReadFromXML(string pathXML, string searchTagCriteria)
        {
            XElement xelement = XElement.Load(pathXML);
            IEnumerable<XElement> sequenceData = xelement.Elements();
            // Iterate through all the sections
            foreach (var sequence in sequenceData)
            {
                if (sequence.Name.LocalName == searchTagCriteria)
                {
                    return sequence.FirstNode.Parent.Value;
                }            
            }
            return ""; // return error if not find data at file format adress
        }
    }
}