using System;

namespace IsometrixTest.StringCalculator
{
    /// <summary>Parses string calculator expression.</summary>
    public interface IExpressionParser
    {
        /// <summary>Parse string calculator expression.</summary>
        /// <param name="expression">String calculator expression to parse.</param>
        /// <returns>Parsed expression.</returns>
        /// <exception cref="ArgumentNullException">Provided expression was null.</exception>
        /// <exception cref="ArgumentException">Provided expression was incorrect format.</exception>
        StringCalculatorExpression Parse(string expression);
    }
}
