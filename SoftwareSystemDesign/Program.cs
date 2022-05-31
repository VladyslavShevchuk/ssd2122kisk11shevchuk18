using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareSystemDesignApp
{
    class Program
    {
        private const string VERSION_NUMBER = "1";
        private static readonly Dictionary<string, string> OPTIONS = new Dictionary<string, string>()
        {
            {"-sf", "set sequence from file"},
            {"-s", "set sequence from console"},
            {"-n", "number of elements that will be generated"},
            {"-h", "open user manual"},
            {"-v", "get software version"},
            {"-lv", "exit from program"}
        };
        private static bool isSequenseWasRecieved;
        private static bool isNumberWasRecieved;

        static void Main(string[] args)
        {
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
                    case "-sf":                    
                        Console.WriteLine("Enter location to file with SF, or enter again '-sf' to exit from this menu:");
                        while (true)
                        {
                            string filePath = Console.ReadLine();
                            if(filePath == "-sf")
                            {
                                Console.Clear();
                                break;
                            }
                            // C:\torrent\TestData\TestFile.ini
                            string dataFromFile = FileReader.ReadDataFromFile(filePath);
                            if(dataFromFile != null)
                            {   
                                Calculation.GetSequnceByFile(dataFromFile);
                                Console.WriteLine(Calculation.Sequence); // Visualize sequence to user
                                isSequenseWasRecieved = true;
                                break;
                            }                       
                        }                  
                        break;

                    case "-s":   
                        Console.WriteLine("Enter sequence:");   
                        Calculation.GetSequnceByManualTyping(Console.ReadLine());
                        isSequenseWasRecieved = true;
                        break;

                    case "-n":
                        Console.WriteLine("Enter number of sequnce elements");
                        
                        bool isresultOfParsingCorrect, isNumberPositive;
                        int numberOfSequnce;
                        // Verify that entered number is correct (uint)
                        while (true)
                        {
                            isresultOfParsingCorrect = int.TryParse(Console.ReadLine(), out numberOfSequnce); // Verify that number is integer
                            isNumberPositive = numberOfSequnce > 0 ? true : false; // Verify that number at least '1'
                            if(isresultOfParsingCorrect && isNumberPositive)
                            {
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Wrong format of number. Please enter only positive integer type (max - 2147483647).");
                            }                          
                        }
                        Calculation.GetNumberOfSequnceElements(numberOfSequnce);
                        isNumberWasRecieved = true;
                        break;

                    case "-h":
                        Calculation.CallUserHelpInfo();
                        break;

                    case "-v":
                        Console.WriteLine(Calculation.CallVersionNumber());
                        break;

                    case "-lv":
                        Calculation.ExitFromProgram();
                        break;

                    default:
                        break;
                }

                // Display info that .csv file was created
                if (isSequenseWasRecieved && isNumberWasRecieved)
                {
                    if (Calculation.IsSequnceCorrect(Calculation.Sequence))
                    {
                        Calculation.CalculateSequnceElements(Calculation.Sequence, Calculation.NumberOfSequnce);
                        Calculation.PrintAnswers();
                        FileWriter.WriteDataToCSVFile(Calculation.Answer);
                        isNumberWasRecieved = false;
                        isSequenseWasRecieved = false;
                    }
                    else
                    {
                        Console.WriteLine("Entered sequence contains validation error. Please enter new sequnce.");
                    }                  
                }
            }
        }
    }
}
