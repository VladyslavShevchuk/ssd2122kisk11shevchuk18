using System;
using System.Collections.Generic;

namespace SoftwareSystemDesignApp
{
    // Main class (Viewer)
    class Program
    {
        // Instruction value
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

        static void Main(string[] args)
        {
            // Infinite loop - console menu
            while (true)
            {
                // Display command options
                Console.WriteLine("Please enter one of following command, enter '-h' for help:");
                foreach(var option in OPTIONS)
                {
                    Console.WriteLine($"{option.Key, 3}, {option.Value}");
                }
                string userOption = Console.ReadLine();
                Console.Clear();
                              
                // Menu implementation
                switch (userOption)
                {
                    // Read sequnce from file .ini, .xml, .json extention case
                    case "-sf":                    
                        Console.WriteLine("Enter location to file with SF, or enter again '-sf' to exit from this menu option:");
                        while (true)
                        {
                            string filePath = Console.ReadLine();
                            if(filePath == "-sf")
                            {
                                Console.Clear();
                                break;
                            }
                            string dataFromFile = FileReader.ReadDataFromFile(filePath); // C:\torrent\TestData\TestFile.ini
                            // Verify that entered sequence is correct
                            if (Calculation.IsSequnceCorrect(dataFromFile))
                            {
                                Calculation.SetSequnce(dataFromFile);
                                isSequenseWasRecieved = true;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Entered sequence contains validation error. Please enter new sequnce.");
                            }
                        }                  
                        break;
                    // Read sequence from console case
                    case "-s":
                        Console.WriteLine("Enter sequnce, or enter again '-s' to exit from this menu option:");
                        while (true)
                        {
                            string sequenceValue = Console.ReadLine();
                            if (sequenceValue == "-s")
                            {
                                Console.Clear();
                                break;
                            }
                            // Verify that entered sequence is correct
                            if (Calculation.IsSequnceCorrect(sequenceValue))
                            {
                                Calculation.SetSequnce(sequenceValue);
                                isSequenseWasRecieved = true;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Entered sequence contains validation error. Please enter new sequnce.");
                            }
                        }
                        break;
                    // Read number of elements from console case
                    case "-n":
                        Console.WriteLine("Enter number of sequnce elements, or enter again '-n' to exit from this menu option:");
                        while (true)
                        {                           
                            string numberOfSequence = Console.ReadLine();
                            if (numberOfSequence == "-n")
                            {
                                Console.Clear();
                                break;
                            }
                            // Verify that entered number is correct (uint)
                            if (Calculation.IsNumberCorrect(numberOfSequence))
                            {
                                Calculation.SetNumberOfSequnceElements(int.Parse(numberOfSequence));
                                isNumberWasRecieved = true;
                                Console.Clear();
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Wrong format of number. Please enter only positive integer type (max - 2147483647).");
                            }                          
                        }                      
                        break;
                    // Call user manual case
                    case "-h":
                        Calculation.CallUserHelpInfo();
                        break;
                    // Call version number case
                    case "-v":
                        Console.WriteLine(Calculation.CallVersionNumber());
                        break;
                    // Exit from program case
                    case "-lv":
                        Calculation.ExitFromProgram();
                        break;
                    default:
                        break;
                }

                // Execute sequence 
                if (isSequenseWasRecieved && isNumberWasRecieved)
                {
                    CalculateAndSaveResults();
                    isNumberWasRecieved = false;
                    isSequenseWasRecieved = false;
                }
            }
        }

        /// <summary>
        /// Method-helper for calling calculating and saving results
        /// </summary>
        private static void CalculateAndSaveResults()
        {
            Calculation.CalculateSequnceElements();
            Console.WriteLine(Calculation.GetSequnceResults());
            Calculation.SendSequnceResultToWriter();
        }
    }
}
