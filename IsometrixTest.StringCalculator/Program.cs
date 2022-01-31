using System;

namespace IsometrixTest.StringCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStringCalculator stringCalculator = new StringCalculatorService();

            for (; ; )
            {
                Console.Write("Input string calculator expression: ");
                string input = Console.ReadLine();
                try
                {
                    int result = stringCalculator.Add(input);
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    ConsoleColor previousColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = previousColor;
                }
                Console.WriteLine();
            }
        }
    }
}
