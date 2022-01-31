using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IsometrixTest.StringCalculator.Services
{
    /// <inheritdoc/>
    public class NumbersParserService : INumbersParser
    {
        /// <inheritdoc/>
        public IEnumerable<int> Parse(string numbersExpression, IEnumerable<string> delimeters)
        {
            if (numbersExpression == null)
                throw new ArgumentNullException(nameof(numbersExpression));
            if (delimeters == null)
                throw new ArgumentNullException(nameof(delimeters));
            if (!delimeters.Any())
                throw new ArgumentException("At least one delimeter must be specified", nameof(delimeters));

            // special case - empty expression
            if (string.IsNullOrEmpty(numbersExpression))
                return new int[] { 0 };

            // we're using regex for splitting the delimeters, so we need to escape special characters in each delimeter
            delimeters = delimeters.Select(delimeters => EscapeSpecialCharacters(delimeters));
            // build dynamic regex - simply OR every delimeter
            Regex delimetersRegex = new Regex(string.Join('|', delimeters), RegexOptions.CultureInvariant | RegexOptions.Multiline);

            // parse numbers: 
            IEnumerable<int> numbers =
                // - split with dynamic regex
                delimetersRegex.Split(numbersExpression)
                // - convert to int
                .Select(num => Convert.ToInt32(num))
                // - disallow values greater than 1000
                .Where(num => num <= 1000);

            // check for negatives - if any, throw
            IEnumerable<int> negatives = numbers.Where(num => num < 0);
            if (negatives.Any())
                throw new NegativesNotAllowedException(negatives);

            return numbers;
        }

        private static string EscapeSpecialCharacters(string input)
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
