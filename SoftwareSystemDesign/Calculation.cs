using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SoftwareSystemDesignApp
{
    // Class-contoller
    // Perform all calculations with taken data
    public static class Calculation
    {
        // Logger instance
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly string VERSION_NUMBER = "2.0.0."; // Current program version
        private static string Sequence; // Data with sequence at string format
        private static int NumberOfSequence; // Number of sequnce element which will be calculated
        private static List<double> SequenceResult = new List<double>(); // Results of calculations which will be stored at .csv file

        /// <summary>
        /// Print entered sequence to console for user
        /// </summary>
        public static string GetSequence() => Sequence;

        /// <summary>
        /// Print entered number of sequence to console for user
        /// </summary>
        public static int GetNumberOfSequence() => NumberOfSequence;

        /// <summary>
        /// Reads sequence from file as a string and sets to 'Sequnce' property
        /// </summary>
        /// <param name="sequence">Readed sequnce from file</param>
        public static void SetSequnceByFile(string sequnceByFile)
        {
            Sequence = sequnceByFile;
        }

        /// <summary>
        /// Sets sequence
        /// </summary>
        /// <param name="sequence">Sequence value</param>
        public static void SetSequnce(string sequence)
        {        
            Sequence = sequence;
        }

        /// <summary>
        /// Sets number of sequnce
        /// </summary>
        /// <param name="sequence">Number of sequnce</param>
        public static void SetNumberOfSequnceElements(int numberOfSequnce)
        {
            NumberOfSequence = numberOfSequnce;
        }

        /// <summary>
        /// Printing user manual
        /// </summary>
        public static void CallUserHelpInfo()
        {
            log.Debug("User help info was called.");
            string userHelpInfoPath = "..\\..\\UserHelpInfo.txt";
            string additionalHelpInfoPath = "UserHelpInfo.txt";
            try
            {
                Process.Start(userHelpInfoPath);
            }
            catch
            {
                Process.Start(additionalHelpInfoPath);
            }
        }

        /// <summary>
        /// Printing number of program version
        /// </summary>
        public static string CallVersionNumber()
        {
            log.Debug("Version number method was called.");
            string versionNumber = $"Version number: {VERSION_NUMBER}";
            return versionNumber;
        }

        /// <summary>
        /// Exiting from program
        /// </summary>
        public static void ExitFromProgram()
        {
            log.Info("Program shutdown.");
            Environment.Exit(0);
        }

        /// <summary>
        /// Calculate numbers of sequnce
        /// </summary>
        public static void CalculateSequnceElements()
        {
            log.Debug("CalculateSequenceElements method was called.");
            try
            {
                int[] availableIterations = new int[NumberOfSequence];
                for(int i = 0; i < availableIterations.Length; i++)
                {
                    availableIterations[i] = i+1;
                }

                availableIterations.Aggregate(0.0, (acc, value) => {
                    string formattedSequence = Sequence.Replace("n", value.ToString());
                    double ans = acc += Parse(formattedSequence);
                    SequenceResult.Add(ans);
                    return ans;
                });
                NumberOfSequence = 0;
                Sequence = null;
            }
            catch(Exception exception)
            {
                log.Error("Error of calculation was occurred.", exception);
                Console.WriteLine("Error of calculation. Data cleared.");
            }
        }

        /// <summary>
        /// Parse and calculate expression 
        /// </summary>
        /// <param name="expression">Expression which be calculated</param>
        /// <returns>Result of string parsing and calculation</returns>
        private static double Parse(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        /// <summary>
        /// Check if entered sequnce correct
        /// </summary>
        /// <returns>True if sequnce pattern correct, false otherwise</returns>
        public static bool IsSequenceCorrect(string sequence)
        {
            log.Debug("IsSequenceCorrect method was called.");
            if (string.IsNullOrEmpty(sequence))
            {
                return false;
            }
            string sequncePattern = @"\+|-|\*|/|\)|\(|[0-9]|n";
            Regex regex = new Regex(sequncePattern);          
            try
            {
                var matchedExpressionChars = Regex.Matches(sequence, sequncePattern)
                    .Cast<Match>().Select(m => m.Value).ToArray();
                bool isExpressionValid = string.Join("", matchedExpressionChars).Length == string.Join("", sequence.Split(' ')).Length;
                return isExpressionValid;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if entered number if correct
        /// </summary>
        /// <param name="readedNumber">Number entered by console</param>
        /// <returns>True if number is correct, false otherwise</returns>
        public static bool IsNumberCorrect(string readedNumber)
        {
            log.Debug("IsNumberCorrect method was called.");
            int.TryParse(readedNumber, out int number); // Verify that number is integer
            bool result = number > 0 ? true : false; // Verify that number at least '1'
            return result;
        }

        /// <summary>
        /// Sending data to file writer and crearing current sequnce results
        /// </summary>
        public static void SendSequnceResultToWriter()
        {
            log.Debug("SendSequenceResultToWriter method was called.");
            try
            {
                FileWriter.WriteDataToCSVFile(SequenceResult);
            }
            catch(Exception exception)
            {
                log.Error("Error of writing data to file.", exception);
                Console.WriteLine("Error of writing data to file. Results were cleared.");
            }
            finally
            {
                // Clear data in any case
                SequenceResult.Clear();
            }
        }

        /// <summary>
        /// Get sequence results as string to print for user
        /// </summary>
        public static string GetSequenceResults(List<double> sequenceResult)
        {
            log.Debug("GetSequenceResults method was called.");
            StringBuilder sequenceResultForPrint = new StringBuilder();
            foreach (var number in sequenceResult)
            {
                sequenceResultForPrint.Append($"{string.Format("{0:0.00}", number)}; "); // String.Format("{0:0.00}", 123.4567);
            }
            if(sequenceResultForPrint.Length > 2)
            {
                sequenceResultForPrint = sequenceResultForPrint.Remove(sequenceResultForPrint.Length - 2, 2); // Remove last dot-coma symbol
            }           
            return sequenceResultForPrint.ToString();
        }

        /// <summary>
        /// Print sequence results to console for user
        /// </summary>
        public static void PrintSequnceResults()
        {
            Console.WriteLine(GetSequenceResults(SequenceResult));
        }
    }
}
