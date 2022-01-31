using System;

namespace IsometrixTest.StringCalculator.Services
{
    /// <inheritdoc/>
    public class ExpressionParserService : IExpressionParser
    {
        /// <inheritdoc/>
        public StringCalculatorExpression Parse(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            string delimetersExpression;
            string numbersExpression;

            if (expression.StartsWith("//"))
            {
                int newLineIndex = expression.IndexOf('\n');
                if (newLineIndex < 0)
                    throw new ArgumentException("Expression is of invalid format - delimeters must be separated from numbers with a new line", nameof(expression));

                // delimeters expression is anything between // and new line
                // if delimeters expression exist, numbers expression will be anything after the new line
                delimetersExpression = expression.Substring(2, newLineIndex - 2);
                numbersExpression = expression.Substring(newLineIndex + 1);
            }
            else
            {
                // if delimeter expression doesn't exist, the entire expression is numbers expression
                delimetersExpression = null;
                numbersExpression = expression;
            }

            return new StringCalculatorExpression(delimetersExpression, numbersExpression);
        }
    }
}
