using IsometrixTest.StringCalculator.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace IsometrixTest.StringCalculator.Tests
{
    public class MainRequirements
    {
        private IServiceProvider _services;
        private IStringCalculator _calculator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IStringCalculator, StringCalculatorService>();
            serviceCollection.AddTransient<INumbersParser, NumbersParserService>();
            serviceCollection.AddTransient<IExpressionParser, ExpressionParserService>();
            serviceCollection.AddTransient<IDelimetersParser, DelimeterParserService>();

            this._services = serviceCollection.BuildServiceProvider();
        }

        [SetUp]
        public void Setup()
        {
            this._calculator = this._services.GetRequiredService<IStringCalculator>();
        }

        [Test]
        [TestCase("", 0)]
        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        public void Step1_AddUpTo2(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("1,2,3", 6)]
        [TestCase("2,4,6,8,10", 30)]
        public void Step2_AddUnknownAmount(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("1\n2,3", 6)]
        [TestCase("1,2\n4", 7)]
        public void Step3_NewLineDelimeter(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("//;\n1;2", 3)]
        [TestCase("//G\n1G4", 5)]
        public void Step4_CustomDelimeter(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        // note: no need to test first line being optional, as steps 1-3 will still test that

        [Test]
        [TestCase("//;\n1;2\n3", 6)]
        [TestCase("//G\n1G4\n2", 7)]
        public void Step4_CustomDelimeter_StillSupportsNewLine(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("-1,2")]
        [TestCase("1,-2")]
        public void Step5_NegativeNumbers_Throw(string expression)
        {
            Assert.Throws<NegativesNotAllowedException>(() => this._calculator.Add(expression));
        }

        [Test]
        [TestCase("-1,2", new int[] { -1 })]
        [TestCase("1,-2,-3", new int[] { -2, -3 })]
        public void Step5_NegativeNumbers_ListsNegatives(string expression, int[] negatives)
        {
            string negativesMessage = string.Join(", ", negatives);

            NegativesNotAllowedException exception = Assert.Catch<NegativesNotAllowedException>(() => this._calculator.Add(expression));

            Assert.AreEqual(negativesMessage, exception.Message);
        }

        [Test]
        [TestCase("2,1001", 2)]
        [TestCase("2,3,12345", 5)]
        public void Step6_IgnoreBiggerThan1000(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("2,1000", 1002)]
        public void Step6_AllowUpTo100Inclusive(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }
    }
}