using System;

namespace IsometrixTest.StringCalculator
{
    /// <summary>Performs operations on numbers in a string.</summary>
    public interface IStringCalculator
    {
        /// <summary>Sums all numbers in a string.</summary>
        /// <param name="expression">Expression for numbers to sum. Can contain custom delimeters expression.</param>
        /// <returns>Sum of all numbers in expression.</returns>
        /// <exception cref="ArgumentNullException">Expression was null.</exception>
        /// <exception cref="ArgumentException">Something was wrong with the expression.</exception>
        /// <exception cref="NegativesNotAllowedException">Expression contains negative values.</exception>
        int Add(string expression);
    }
}
