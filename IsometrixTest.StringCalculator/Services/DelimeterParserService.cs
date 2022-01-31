using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace IsometrixTest.StringCalculator.Services
{
    /// <inheritdoc/>
    public class DelimeterParserService : IDelimetersParser
    {
        private static readonly Regex _multiDelimeterRegex = new Regex(@"\[([^\[\]]+)\]", RegexOptions.CultureInvariant);

        /// <inheritdoc/>
        public IEnumerable<string> Parse(string delimetersExpression)
        {
            // for empty expression, just return defaults
            if (string.IsNullOrEmpty(delimetersExpression))
                return new string[] { ",", "\n" };

            // use regex to parse groups of []-wrapped delimeters
            MatchCollection longDelimeterMatches = _multiDelimeterRegex.Matches(delimetersExpression);
            if (longDelimeterMatches.Any())
            {
                // add delimeter from each group to a collection
                List<string> delimeters = new List<string>(longDelimeterMatches.Count() + 1);
                for (int i = 0; i < longDelimeterMatches.Count; i++)
                {
                    string delimeterValue = longDelimeterMatches[i].Groups[1].Value;

                    if (string.IsNullOrEmpty(delimeterValue))
                        throw new ArgumentException("Empty brackets delimeter is invalid");

                    delimeters.Add(delimeterValue);
                }
                // new line is always a valid delimeter
                delimeters.Add("\n");
                return delimeters;
            }

            // if regex didn't match, means simple one-char delimeter is used
            else if (delimetersExpression.Length > 1)
                throw new ArgumentException("Non-bracket delimeter cannot have more than 1 character", nameof(delimetersExpression));
            return new string[] { delimetersExpression, "\n" };
        }
    }
}
