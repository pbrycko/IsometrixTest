using System;
using System.Collections.Generic;

namespace IsometrixTest.StringCalculator
{
    /// <summary>Parses numbers expressions.</summary>
    public interface INumbersParser
    {
        /// <summary>Parse expression with given set of delimeters.</summary>
        /// <param name="numbersExpression">Expression to parse.</param>
        /// <param name="delimeters">Delimeters to split numbers by.</param>
        /// <returns>Enumerable of found numbers.</returns>
        /// <exception cref="ArgumentNullException">Expression or delimeters enumerable are null.</exception>
        /// <exception cref="ArgumentException">Delimeters enumerable is empty.</exception>
        /// <exception cref="NegativesNotAllowedException">Expression contains negative values.</exception>
        IEnumerable<int> Parse(string numbersExpression, IEnumerable<string> delimeters);
    }
}
