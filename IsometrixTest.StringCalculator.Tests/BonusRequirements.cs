using IsometrixTest.StringCalculator.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;

namespace IsometrixTest.StringCalculator.Tests
{
    public class BonusRequirements
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
        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[foo]\n1foo7foo2", 10)]
        public void Step1_AnyLengthDelimeter(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("//***\n1***2***3")]
        [TestCase("//foo\n1foo7foo2")]
        public void Step1_NoSquareBracket_Throws(string expression)
        {
            Assert.Throws<ArgumentException>(() => this._calculator.Add(expression));
        }

        [Test]
        [TestCase("//[]\n1***2***3")]
        public void Step1_SquareBracket_Invalid(string expression)
        {
            Assert.Throws<ArgumentException>(() => this._calculator.Add(expression));
        }

        [Test]
        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[;][,]\n1;7,2", 10)]
        public void Step2_MultipleDelimeters(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("//[***][%]\n1***2%3", 6)]
        [TestCase("//[foo][***]\n1foo7***2", 10)]
        public void Step3_MultipleAnyLengthDelimeters(string expression, int expectedResult)
        {
            int result;

            result = this._calculator.Add(expression);

            Assert.AreEqual(expectedResult, result);
        }
    }
}