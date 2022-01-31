using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IsometrixTest.StringCalculator
{
    public class StringCalculatorService : IStringCalculator
    {
        public int Add(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return 0;
            string[] delimeters = this.GetDelimeters(expression);
            IEnumerable<int> numbers = this.GetNumbers(expression, delimeters);
            return numbers.Sum();
        }

        private string[] GetDelimeters(string expression)
        {
            if (!expression.StartsWith("//"))
                return new string[] { ",", "\n" };
            string delimetersText = expression.Substring(2, expression.IndexOf('\n') - 2);
            Match longDelimeterMatch = Regex.Match(delimetersText, @"^\[([^\[\]]+)\]$");
            if (longDelimeterMatch.Success)
            {
                string delimeterValue = longDelimeterMatch.Groups[1].Value;
                if (string.IsNullOrEmpty(delimeterValue))
                    throw new ArgumentException("Empty brackets delimeter is invalid");
                return new string[] { delimeterValue, "\n" };
            }
            if (delimetersText.Length > 1)
                throw new ArgumentException("Non-bracket delimeter cannot have more than 1 character", nameof(expression));
            return new string[] { delimetersText, "\n" };
        }

        private IEnumerable<int> GetNumbers(string expression, string[] delimeters)
        {
            if (expression.StartsWith("//"))
                expression = expression.Substring(expression.IndexOf('\n') + 1);

            IEnumerable<int> numbers = Regex.Split(expression, string.Join('|', delimeters.Select(delimeter => this.EscapeSpecialCharacters(delimeter)))).Select(num => Convert.ToInt32(num)).Where(num => num <= 1000);
            IEnumerable<int> negatives = numbers.Where(num => num < 0);
            if (negatives.Any())
                throw new NegativesNotAllowedException(negatives);
            return numbers;
        }

        private string EscapeSpecialCharacters(string input)
        {
            string result = new string(input);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                char character = input[i];
                if (!char.IsLetterOrDigit(character))
                    result = result.Insert(i, "\\");
            }
            return result;
        }
    }
}
