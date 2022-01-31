using System;
using System.Collections.Generic;
using System.Linq;

namespace IsometrixTest.StringCalculator.Services
{
    /// <inheritdoc/>
    public class StringCalculatorService : IStringCalculator
    {
        private readonly IExpressionParser _expressionParser;
        private readonly INumbersParser _numbersParser;
        private readonly IDelimetersParser _delimetersParser;

        public StringCalculatorService(IExpressionParser expressionParser, INumbersParser numbersParser, IDelimetersParser delimetersParser)
        {
            this._expressionParser = expressionParser;
            this._numbersParser = numbersParser;
            this._delimetersParser = delimetersParser;
        }

        /// <inheritdoc/>
        public int Add(string expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            StringCalculatorExpression parsedExpression = this._expressionParser.Parse(expression);
            IEnumerable<string> delimeters = this._delimetersParser.Parse(parsedExpression.DelimetersExpression);
            IEnumerable<int> numbers = this._numbersParser.Parse(parsedExpression.NumbersExpression, delimeters);

            return numbers.Sum();
        }
    }
}
