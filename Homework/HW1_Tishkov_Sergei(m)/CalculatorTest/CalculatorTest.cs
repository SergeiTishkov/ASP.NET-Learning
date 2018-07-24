using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit;
using NUnit.Framework;
using CalculatorLib;

namespace CalculatorTest
{
    public class CalculatorTest
    {
        [Test]
        public void AdditionOfTwoPositiveNumbers()
        {
            // arrange
            double expected = 5;
            double actual;

            // act
            actual = Calculator.Add(2, 3);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AdditionOfPositiveAndNumbers()
        {
            // arrange
            double expected = 5;
            double actual;

            // act
            actual = Calculator.Add(7, -2);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubstractionFromPositiveNumberAnotherPositiveNumber()
        {
            // arrange
            double expected = 10;
            double actual;

            // act
            actual = Calculator.Subtract(17, 7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubstractionFromPositiveNumberNegativeNumber()
        {
            // arrange
            double expected = 24;
            double actual;

            // act
            actual = Calculator.Subtract(17, -7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubstractionFromNegativeNumberAnotherNegativeNumber()
        {
            // arrange
            double expected = -24;
            double actual;

            // act
            actual = Calculator.Subtract(-17, 7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyTwoPositiveNumbers()
        {
            // arrange
            double expected = 50;
            double actual;

            // act
            actual = Calculator.Multiply(10, 5);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyOnePositiveAndOneNegativeNumbers()
        {
            // arrange
            double expected = -50;
            double actual;

            // act
            actual = Calculator.Multiply(-10, 5);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyOnePositiveNumberAnotherZero()
        {
            // arrange
            double expected = 0;
            double actual;

            // act
            actual = Calculator.Multiply(-10, 0);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DividePositiveOnPositive()
        {
            // arrange
            double expected = 3;
            double actual;

            // act
            actual = Calculator.Divide(21, 7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DividePositiveByNegative()
        {
            // arrange
            double expected = -3;
            double actual;

            // act
            actual = Calculator.Divide(21, -7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideNegativeByNegative()
        {
            // arrange
            double expected = 3;
            double actual;

            // act
            actual = Calculator.Divide(-21, -7);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DivideByZero()
        {
            // arrange
            bool actual1;
            bool actual2;

            // act
            actual1 = double.IsPositiveInfinity(Calculator.Divide(21, 0));
            actual2 = double.IsNegativeInfinity(Calculator.Divide(-21, 0));

            // assert
            Assert.IsTrue(actual1);
            Assert.IsTrue(actual2);
        }

        [Test]
        public void DivideWithResultWithEndlessMantissa()
        {
            // arrange
            double expected = 3.3333333333333333;
            double actual;

            // act
            actual = Calculator.Divide(10, 3);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateSquareRootOfPositiveNumber()
        {
            // arrange
            double expected = 10;
            double actual;

            // act
            actual = Calculator.Sqrt(100);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CalculateSquareRootONegativeNumber()
        {
            // arrange
            double expected = double.NaN;
            double actual;

            // act
            actual = Calculator.Sqrt(-100);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaisePositiveNumberToPositivePower()
        {
            // arrange
            double expected = 1024;
            double actual;

            // act
            actual = Calculator.Pow(2, 10);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaisePositiveNumberToNegativePower()
        {
            // arrange
            double expected = 0.0009765625;
            double actual;

            // act
            actual = Calculator.Pow(2, -10);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaiseNegativeNumberToNegativeEvenPower()
        {
            // arrange
            double expected = 0.0009765625;
            double actual;

            // act
            actual = Calculator.Pow(-2, -10);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaiseNegativeNumberToNegativeOddPower()
        {
            // arrange
            double expected = -0.001953125;
            double actual;

            // act
            actual = Calculator.Pow(-2, -9);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaiseNegativeNumberToPositiveEvenPower()
        {
            // arrange
            double expected = 1024;
            double actual;

            // act
            actual = Calculator.Pow(-2, 10);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaiseNegativeNumberToPositiveOddPower()
        {
            // arrange
            double expected = -512;
            double actual;

            // act
            actual = Calculator.Pow(-2, 9);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RaiseNumberToZeroPower()
        {
            // arrange
            double expected = 1;
            double actual;

            // act
            actual = Calculator.Pow(9999, 0);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
