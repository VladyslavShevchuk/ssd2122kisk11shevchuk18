using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SoftwareSystemDesignApp
{
    // Class for writing sequence from console to files with .ini, .xml and .json extensions
    // and creating .csv exit file
    public static class FileWriter
    {
        public static void WriteDataToCSVFile(List<double> membersOfSequnce)
        {
            StringBuilder csv = new StringBuilder();
            string filePath = "C:\\torrent\\TestData\\zalupaBomja228.csv";

            using (FileStream fs = File.Create(filePath))

            foreach (var member in membersOfSequnce)
            {
                csv.AppendLine(member.ToString());
            }
            File.WriteAllText(filePath, csv.ToString());
            // Clear data
            Calculation.Answer.Clear();
        }
    }
}
