using NUnit.Framework;
using System;

namespace IsometrixTest.StringCalculator.Tests
{
    public class BonusRequirements
    {
        private IStringCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            this._calculator = new StringCalculatorService();
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
            Assert.Throws<Exception>(() => this._calculator.Add(expression));
        }

        [Test]
        [TestCase("//[]\n1***2***3")]
        public void Step1_SquareBracket_Invalid(string expression)
        {
            Assert.Throws<Exception>(() => this._calculator.Add(expression));
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