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
    // Class for reading sequence from files with .ini, .xml and .json extensions
    public static class FileReader
    {
        // Variables for files internal data path
        private const string FILES_SECTION_NAME = "DataModel";
        private const string FILES_TAGS_NAME = "Sequence";

        /// <summary>
        /// Choose implementation of file read method by it extension
        /// </summary>
        /// <param name="filePath">Path to file which shoul be readed</param>
        /// <returns>Data from sequence file</returns>
        static public string ReadDataFromFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string data = "";
            try
            {           
                // Choose file reader implementation by file extension
                switch (extension)
                {
                    case ".ini":
                        data = ReadFromINI(filePath);
                        break;
                    case ".json":
                        data = ReadFromJSON(filePath);
                        break;
                    case ".xml":
                        data = ReadFromXML(filePath);
                        break;
                    default:
                        Console.Clear();
                        if (data == null)
                        {
                            Console.WriteLine("Section with sequence wasn't founded in this file.");
                        }
                        else
                        {
                            Console.WriteLine("Unknown file extencion. Please reenter file path or enter '-sf' command again to exit from this menu.");
                        }                      
                        break;
                }                
                return data;
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("File not found. Please reenter file path.");
                return null;
            }
        }

        /// <summary>
        /// Read sequence from INI file
        /// </summary>
        /// <param name="pathINI">Path to INI file</param>
        /// <returns>Sequence from INI file</returns>
        static string ReadFromINI(string pathINI)
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
                        if (key.KeyName == FILES_TAGS_NAME)
                        {
                            return key.Value.Replace("\"", "");
                        }
                    }
                }                           
            }
            return null; // return error if not find data at file format adress
        }

        /// <summary>
        /// Read sequence from JSON file
        /// </summary>
        /// <param name="pathJSON">Path to JSON file</param>
        /// <returns>Sequence from JSON file</returns>
        static string ReadFromJSON(string pathJSON)
        {
            // Read JSON directly from a file
            using (StreamReader file = File.OpenText(pathJSON))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject json = (JObject)JToken.ReadFrom(reader);
                return json[FILES_TAGS_NAME].Value<string>();
            }
            return null; // return error if not find data at file by format adress
        }

        /// <summary>
        /// Read sequence from XML file
        /// </summary>
        /// <param name="pathXML">Path to XML file</param>
        /// <returns>Sequence from XML file</returns>
        static string ReadFromXML(string pathXML)
        {
            XElement xelement = XElement.Load(pathXML);
            IEnumerable<XElement> sequenceData = xelement.Elements();
            // Iterate through all the sections
            foreach (var sequence in sequenceData)
            {
                if (sequence.Name.LocalName == FILES_TAGS_NAME)
                {
                    return sequence.FirstNode.Parent.Value;
                }            
            }          
            return null; // return error if not find data at file format adress
        }
    }
}
