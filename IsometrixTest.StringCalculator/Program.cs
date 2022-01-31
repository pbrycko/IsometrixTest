using Microsoft.Extensions.DependencyInjection;
using System;
using IsometrixTest.StringCalculator.Services;

namespace IsometrixTest.StringCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IStringCalculator, StringCalculatorService>();
            serviceCollection.AddTransient<INumbersParser, NumbersParserService>();
            serviceCollection.AddTransient<IExpressionParser, ExpressionParserService>();
            serviceCollection.AddTransient<IDelimetersParser, DelimeterParserService>();

            IServiceProvider services = serviceCollection.BuildServiceProvider();
            IStringCalculator stringCalculator = services.GetRequiredService<IStringCalculator>();

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
