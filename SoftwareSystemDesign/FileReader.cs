using System;
using System.IO;
//INI
using IniParser;

namespace SoftwareSystemDesignApp
{
    // Class for reading sequence from files with .ini, .xml and .json extensions
    public static class FileReader
    {
        /// <summary>
        /// Choose implementation of file read method by it extension
        /// </summary>
        /// <param name="filePath">Path to file which shoul be readed</param>
        /// <returns>Data from sequence file</returns>
        static public string ReadDataFromFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string data = null;
            try
            {           
                // Choose file reader implementation by file extension
                switch (extension)
                {
                    case ".ini":
                        data = ReadFromINI(filePath);
                        break;
                    case ".json":
                        data = ReadFromJSONOrXML(filePath);
                        break;
                    case ".xml":
                        data = ReadFromJSONOrXML(filePath);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Unknown file extencion. Please reenter file path or enter '-sf' command again to exit from this menu.");
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
            return parser.ReadFile(pathINI).ToString();
        }

        /// <summary>
        /// Read sequence from JSON, XML file
        /// </summary>
        /// <param name="pathJSON">Path to JSON file</param>
        /// <returns>Sequence from JSON file</returns>
        static string ReadFromJSONOrXML(string pathJSON)
        {
            return File.ReadAllText(pathJSON);
        }
    }
}
