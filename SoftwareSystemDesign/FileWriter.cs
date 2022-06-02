using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SoftwareSystemDesignApp
{
    // Class for writing sequence from console to files with .ini, .xml and .json extensions
    // and creating .csv exit file
    public static class FileWriter
    {
        /// <summary>
        /// Create/rewrite sequnce result data into .csv file
        /// </summary>
        /// <param name="membersOfSequnce">Sequence result which should be stored</param>
        public static void WriteDataToCSVFile(List<double> membersOfSequnce)
        {
            StringBuilder csv = new StringBuilder();
            string fileName = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            //string filePath = $"..\\..\\{fileName}.csv";
            string filePath = "C:\\torrent\\TestData\\TemporaryFileTest.csv"; 

            using (FileStream fs = File.Create(filePath))
            foreach (var member in membersOfSequnce)
            {
                csv.AppendLine(member.ToString());
            }
            File.WriteAllText(filePath, csv.ToString());
        }
    }
}
