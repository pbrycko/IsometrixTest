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
            char[] delimeters = this.GetDelimeters(expression);
            IEnumerable<int> numbers = this.GetNumbers(expression, delimeters);
            return numbers.Sum();
        }

        private char[] GetDelimeters(string expression)
        {
            if (!expression.StartsWith("//"))
                return new char[] { ',', '\n' };
            string delimetersText = expression.Substring(2, expression.IndexOf('\n') - 2);
            char[] delimeters = new char[delimetersText.Length + 1];
            for (int i = 0; i < delimetersText.Length; i++)
                delimeters[i] = delimetersText[i];
            delimeters[delimeters.Length - 1] = '\n';
            return delimeters;
        }

        private IEnumerable<int> GetNumbers(string expression, char[] delimeters)
        {
            if (expression.StartsWith("//"))
                expression = expression.Substring(expression.IndexOf('\n') + 1);

            IEnumerable<int> numbers = expression.Split(delimeters).Select(num => Convert.ToInt32(num)).Where(num => num <= 1000);
            IEnumerable<int> negatives = numbers.Where(num => num < 0);
            if (negatives.Any())
                throw new NegativesNotAllowedException(negatives);
            return numbers;
        }
    }
}
