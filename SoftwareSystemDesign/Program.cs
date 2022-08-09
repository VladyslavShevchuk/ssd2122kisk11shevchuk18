using System;
using System.Collections.Generic;

namespace SoftwareSystemDesignApp
{
    // Main class (Viewer)
    class Program
    {
        // Logger instance
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); 
        // Menu options
        private static readonly Dictionary<string, string> OPTIONS = new Dictionary<string, string>()
        {
            {"-sf", "set sequence from file"},
            {"-s", "set sequence from console"},
            {"-n", "number of elements that will be generated"},
            {"-h", "open user manual"},
            {"-v", "get software version"},
            {"-lv", "exit from program"}
        };
        private static bool isSequenseWasRecieved; // flag for calling sequence execution
        private static bool isNumberWasRecieved; // flag for calling sequence execution
        private static bool isSequenseShouldBeStoredInFile; // flag for storing current sequence if file

        static void Main(string[] args)
        {
            log.Info("Program started.");
            // Infinite loop - console menu
            while (true)
            {             
                // Display command options
                PrintMenu();
                string userOption = Console.ReadLine();
                Console.Clear();
                              
                // Menu implementation
                switch (userOption)
                {
                    // Read sequnce from file .ini, .xml, .json extention case
                    case "-sf":
                        log.Debug("-sf section was called.");
                        Console.WriteLine("Enter location to file with SF, or enter again '-sf' to exit from this menu option:");
                        while (true)
                        {                            
                            string filePath = Console.ReadLine();
                            // Exit from '-sf' menu
                            if (filePath == "-sf")
                            {
                                Console.Clear();
                                log.Debug("Exit from -sf section.");
                                break;
                            }
                            // Create/rewrite file with validated sequence from '-s'
                            if (isSequenseShouldBeStoredInFile && !string.IsNullOrEmpty(Calculation.GetSequence()))
                            {
                                FileWriter.WriteSequenceToFile(filePath);
                                break;
                            }
                            // Get data from file with entered extencion
                            else
                            {
                                // Read sequence from file
                                string dataFromFile = FileReader.ReadDataFromFile(filePath, true);
                                // Verify that entered sequence is correct
                                if (!string.IsNullOrEmpty(dataFromFile))
                                {
                                    if (Calculation.IsSequenceCorrect(dataFromFile))
                                    {                                      
                                        Calculation.SetSequnce(dataFromFile);
                                        isSequenseWasRecieved = true;
                                        log.Info("The sequence from file is assigned.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Entered sequence contains validation error. Please enter new file path or '-sf' to to exit from this menu option.");
                                    }
                                }
                                // Read number from file
                                string numberFromFile = FileReader.ReadDataFromFile(filePath, false);
                                // Verify that entered sequence is correct
                                if (!string.IsNullOrEmpty(numberFromFile))
                                {
                                    if (Calculation.IsNumberCorrect(numberFromFile))
                                    {                   
                                        Calculation.SetNumberOfSequnceElements(int.Parse(numberFromFile));
                                        isNumberWasRecieved = true;
                                        log.Info("The number of sequence members from file is assigned.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Wrong format of number. Please enter only positive integer type (max - 2147483647).");
                                    }
                                }
                                // Break from case if any of data was recieved
                                if(isSequenseWasRecieved || isNumberWasRecieved)
                                {
                                    Console.Clear();
                                    Console.WriteLine($"Entered sequence: {dataFromFile}"); // Visualize sequence to user
                                    Console.WriteLine($"Entered number: {numberFromFile}"); // Visualize number to user
                                    break;
                                }
                            }                          
                        }
                        break;
                    // Read sequence from console case
                    case "-s":
                        log.Debug("-s section was called.");
                        Console.WriteLine("Enter sequnce, or enter again '-s' to exit from this menu option:");
                        while (true)
                        {                           
                            string sequenceValue = Console.ReadLine();
                            // Exit from '-s' menu
                            if (sequenceValue == "-s")
                            {
                                Console.Clear();
                                log.Debug("Exit from -s section.");
                                break;
                            }
                            // Verify that entered sequence is correct
                            if (Calculation.IsSequenceCorrect(sequenceValue))
                            {                              
                                Calculation.SetSequnce(sequenceValue);
                                isSequenseWasRecieved = true;
                                isSequenseShouldBeStoredInFile = true;
                                Console.Clear();
                                log.Info("The sequence is assigned.");
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                log.Error("Entered sequence contains validation error.");
                                Console.WriteLine("Entered sequence contains validation error. Please enter new sequnce.");
                            }
                        }
                        break;
                    // Read number of elements from console case
                    case "-n":
                        log.Debug("-n section was called.");
                        Console.WriteLine("Enter number of sequnce elements, or enter again '-n' to exit from this menu option:");
                        while (true)
                        {             
                            string numberOfSequence = Console.ReadLine();
                            // Exit from '-n' menu
                            if (numberOfSequence == "-n")
                            {
                                Console.Clear();
                                log.Debug("Exit from -n section.");
                                break;
                            }
                            // Verify that entered number is correct (uint)
                            if (Calculation.IsNumberCorrect(numberOfSequence))
                            {
                                Calculation.SetNumberOfSequnceElements(int.Parse(numberOfSequence));
                                isNumberWasRecieved = true;
                                Console.Clear();
                                log.Info("The number of sequence members is assigned.");
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                log.Error("Entered number was at incorrect format.");
                                Console.WriteLine("Wrong format of number. Please enter only positive integer type (max - 2147483647).");
                            }                          
                        }                      
                        break;
                    // Call user manual case
                    case "-h":                    
                        Calculation.CallUserHelpInfo();
                        log.Info("Called user help info.");
                        break;
                    // Call version number case
                    case "-v":                     
                        Console.WriteLine(Calculation.CallVersionNumber());
                        log.Info("Called version info.");
                        break;
                    // Exit from program case
                    case "-lv":                       
                        Calculation.ExitFromProgram();
                        break;
                    default:
                        break;
                }

                // Execute sequence calculation
                if (isSequenseWasRecieved && isNumberWasRecieved)
                {
                    log.Info("Sequence members starts caclulation.");
                    CalculateAndSaveResults();
                    isNumberWasRecieved = false;
                    isSequenseWasRecieved = false;
                    isSequenseShouldBeStoredInFile = false;                
                }
            }
        }

        /// <summary>
        /// Method-helper for printing menu options
        /// </summary>
        private static void PrintMenu()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("Please enter one of following command, enter '-h' for help:");
            foreach (var option in OPTIONS)
            {
                Console.WriteLine($"{option.Key,3}, {option.Value}");
            }
            Console.WriteLine("===========================================================");
        }

        /// <summary>
        /// Method-helper for calling calculating and saving results
        /// </summary>
        private static void CalculateAndSaveResults()
        {
            Calculation.CalculateSequnceElements();
            Calculation.PrintSequnceResults();
            Calculation.SendSequnceResultToWriter();
        }
    }
}