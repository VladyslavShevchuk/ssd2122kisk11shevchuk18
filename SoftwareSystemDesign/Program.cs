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
        private static readonly List<string> options = new List<string>() {"-sf", "-s", "-n", "-h", "-v", "-lv"};

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter one of following command, enter '-h' for help:");
                foreach(var option in options)
                {
                    Console.WriteLine(option);
                }
                string userOption = Console.ReadLine();

                switch (userOption)
                {                  
                    case "-sf":
                        Console.Clear();
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
                            Calculation.GetSequnceByFile(FileReader.ReadDataFromFile(filePath));
                            if(Calculation.Sequence != null)
                            {
                                Console.WriteLine(Calculation.Sequence); // Visualize sequence to user
                                break;
                            }                       
                        }                  
                        break;

                    case "-s":   
                        Console.Clear();
                        Console.WriteLine("Enter sequence:");   
                        Calculation.GetSequnceByManualTyping(Console.ReadLine());
                        break;

                    case "-n":
                        Console.Clear();
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
                        break;

                    case "-h":
                        Console.Clear();
                        Console.WriteLine(Calculation.CallUserHelpInfo());
                        break;

                    case "-v":
                        Console.Clear();
                        Console.WriteLine(Calculation.CallVersionNumber());
                        break;

                    case "-lv":
                        Console.Clear();
                        Calculation.ExitFromProgram();
                        break;

                    default:
                        Console.Clear();
                        break;
                }                                 
            }
        }
    }
}
