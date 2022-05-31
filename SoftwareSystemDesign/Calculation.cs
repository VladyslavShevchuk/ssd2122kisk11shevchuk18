using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SoftwareSystemDesignApp
{
    public static class Calculation
    {    
        private static readonly string VERSION_NUMBER = "1";
        public static string Sequence { get;  private set; } // Data with sequence at string format
        public static int NumberOfSequnce { get; private set; } // Number of sequnce element which will be calculated
        public static List<double> Answer = new List<double>();

        /// <summary>
        /// Reads sequence from file as a string and sets to 'Sequnce' property
        /// </summary>
        /// <param name="sequence">Readed sequnce from file</param>
        public static void GetSequnceByFile(string sequnceByFile)
        {
            Calculation.Sequence = sequnceByFile;
        }

        /// <summary>
        /// Reads sequence from console and sets to 'Sequnce' property
        /// </summary>
        /// <param name="sequence">Readed sequnce from console</param>
        public static void GetSequnceByManualTyping(string sequenceByConsole)
        {        
            Calculation.Sequence = sequenceByConsole;
        }  
        
        /// <summary>
        /// Reads number of sequence from console and sets to 'NumberOfSequnce' property
        /// </summary>
        /// <param name="sequence">Readed sequnce</param>
        public static void GetNumberOfSequnceElements(int numberOfSequnce)
        {
            Calculation.NumberOfSequnce = numberOfSequnce;
        }

        /// <summary>
        /// Printing user manual
        /// </summary>
        public static void CallUserHelpInfo()
        {
            string userHelpInfoPath = "..\\..\\UserHelpInfo.txt";
            Process.Start(userHelpInfoPath);          
        }

        /// <summary>
        /// Printing number of program version
        /// </summary>
        public static string CallVersionNumber()
        {
            string versionNumber = $"Version number: {VERSION_NUMBER}";
            return versionNumber;
        }

        /// <summary>
        /// Exiting from program
        /// </summary>
        public static void ExitFromProgram()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Calculate numbers of sequnce
        /// </summary>
        public static void CalculateSequnceElements(string sequnce, int number)
        {          
            try
            {
                int[] availableIterations = new int[number];
                for(int i = 0; i < availableIterations.Length; i++)
                {
                    availableIterations[i] = i+1;
                }

                availableIterations.Aggregate(0.0, (acc, value) => {
                    string formattedSequence = sequnce.Replace("n", value.ToString());
                    double ans = acc+= Parse(formattedSequence);
                    Answer.Add(ans);
                    return ans;
                });
            }
            catch
            {
                Console.WriteLine("Error of calculation.");
            }
        }

        public static void PrintAnswers()
        {
            foreach (var number in Answer)
            {
                Console.Write($"{number}, ");
            }
        }

        private static double Parse(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        public static bool IsSequnceCorrect(string sequnce)
        {
            string sequncePattern = @"\+|-|\*|/|\)|\(|[0-9]|n";
            Regex regex = new Regex(sequncePattern);          
            try
            {
                var matchedExpressionChars = Regex.Matches(sequnce, sequncePattern)
                    .Cast<Match>().Select(m => m.Value).ToArray();
                bool isExpressionValid = string.Join("", matchedExpressionChars).Length == string.Join("", sequnce.Split(' ')).Length;
                return isExpressionValid;
            }
            catch
            {
                return false;
            }
        }
    }
}
