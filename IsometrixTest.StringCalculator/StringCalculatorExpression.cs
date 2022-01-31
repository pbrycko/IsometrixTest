using System;

namespace IsometrixTest.StringCalculator
{
    /// <summary>Represents string calculator expression as separate delimeters expression and numbers expression.</summary>
    public class StringCalculatorExpression
    {
        /// <summary>Expression of the delimeters part.</summary>
        /// <remarks>Can be consumed by <see cref="IDelimetersParser"/></remarks>
        public string DelimetersExpression { get; }
        /// <summary>Expression of the numbers part.</summary>
        /// <remarks>Can be consumed by <see cref="INumbersParser"/></remarks>
        public string NumbersExpression { get; }

        public StringCalculatorExpression(string delimeters, string numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            this.DelimetersExpression = delimeters;
            this.NumbersExpression = numbers;
        }
    }
}
