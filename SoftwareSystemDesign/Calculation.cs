using System;

namespace SoftwareSystemDesignApp
{
    public static class Calculation
    {    
        private static readonly string VERSION_NUMBER = "1";
        public static string Sequence { get;  private set; } // Data with sequence at string format
        public static int NumberOfSequnce { get; private set; } // Number of sequnce element which will be calculated

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
        public static string CallUserHelpInfo()
        {
            string userHelperInfo = "User helper:";
            return userHelperInfo;
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
    }
}
