using System;
using System.Collections.Generic;

namespace IsometrixTest.StringCalculator
{
    /// <summary>Parses delimeters expressions.</summary>
    public interface IDelimetersParser
    {
        /// <summary>Parses delimeters from delimeters expression.</summary>
        /// <param name="delimetersExpression">Delimeters expression to parse.</param>
        /// <returns>Enumerable of all delimeters.</returns>
        /// <exception cref="ArgumentException">One of delimeters was invalid.</exception>
        IEnumerable<string> Parse(string delimetersExpression);
    }
}
