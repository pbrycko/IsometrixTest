using System;
using System.Collections.Generic;
using System.Linq;

namespace IsometrixTest.StringCalculator
{
    public class StringCalculatorService : IStringCalculator
    {
        public int Add(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return 0;
            IEnumerable<int> numbers = this.GetNumbers(expression);
            return numbers.Sum();
        }

        private IEnumerable<int> GetNumbers(string expression)
        {
            string[] numbers = expression.Split(',');
            return numbers.Select(num => Convert.ToInt32(num));
        }
    }
}
