using EquationTransform.Transformator.Contract.Exceptions;
using EquationTransform.Transformator.SimpleTransformator;
using System;
using Xunit;

namespace EquationTransform.Transformator.Tests
{
    public class SimpleEquationTransformatorTest
    {
        private readonly SimpleEquationTransformator _transformator;

        public SimpleEquationTransformatorTest()
        {
            _transformator = new SimpleEquationTransformator();
        }

        [Fact]
        public void TransformatorNullArgumentTest()
        {
            Assert.Throws<ArgumentNullException>(() => _transformator.TransformToCanonical(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void TransformatorEmptyTest(string inputString)
        {
            Assert.Throws<ArgumentException>(() => _transformator.TransformToCanonical(inputString));
        }

        [Theory]
        [InlineData("x^2")]
        [InlineData("x^2 - + y")]
        [InlineData("x^2 + 3.5xy + y - y^2 - xy + y")]
        [InlineData("x^y = 0")]
        [InlineData("x^2.5 = 0")]
        [InlineData("x^2.5 / 5 = 0")]
        [InlineData("x^2.5 y 5 = 0")]
        public void TransformatorIncorrectFormatTest(string equation)
        {
            Assert.Throws<IncorrectEquationFormatException>(() => _transformator.TransformToCanonical(equation));
        }

        [Theory]
        [InlineData("x^2 = 0", "x^2 = 0")]
        [InlineData("x^(-2) = 0", "x^(-2) = 0")]
        [InlineData("3x = 0", "3x = 0")]
        [InlineData("0 = -3x", "3x = 0")]
        [InlineData("-3x = 0", "3x = 0")]
        [InlineData("a = a", "0 = 0")]
        public void TransformatorSimpleCorrectTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("x^2 = y", "x^2 - y = 0")]
        [InlineData("x^(-2) = y", "y - x^(-2) = 0")]
        [InlineData("2.5x = 1.5y", "2.5x - 1.5y = 0")]
        [InlineData("y = -3x", "3x + y = 0")] //?
        [InlineData("a^2 + ab + b^2 = a^2 + ab + b^2", "0 = 0")]
        public void Transformator2VariableCorrectTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("x^2 + 3.5xy + y = y^2 - xy + y", "x^2 - y^2 + 4.5xy = 0")]
        [InlineData("-1.5x^2 + 3.5xy + y = y^2 - xy + y", "1.5x^2 + y^2 - 4.5xy = 0")]
        public void TransformatorCorrectTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("3.5(x^2) = 0", "3.5x^2 = 0")]
        public void TransformatorSimpleBracketsTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("x^2 + 3.5xy + y = y^2 - (xy + y)", "x^2 - y^2 + 4.5xy + 2y = 0")]
        [InlineData("x^(-2) = y", "y - x^(-2) = 0")]
        [InlineData("((-(x^2) + 3.5xy) + y) = y^2 - (xy + y)", "x^2 + y^2 - 4.5xy - 2y = 0")]
        public void TransformatorCorrectBracketsTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("-(-(-(-x))) = 0", "x = 0")]
        [InlineData("-(-(-x)) = 0", "x = 0")]
        [InlineData("y -(-(-x)) = 0", "x - y = 0")]
        [InlineData("-(-(-x)) - y = 0", "x + y = 0")]
        public void TransformatorMultipleBracketsTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("3.5xy + y = 3.5yx", "y = 0")]
        [InlineData("2(x^2)(y^2) = (y^2)(x^2)", "(x^2)(y^2) = 0")]
        public void TransformatorDifferentVariableOrderTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("3.5xy + 10 = y", "3.5xy - y + 10 = 0")]
        [InlineData("x^2 = 5", "x^2 - 5 = 0")]
        public void TransformatorWithConstantsTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("x^2 = -y^3", "y^3 + x^2 = 0")]
        [InlineData("x^2 = y^3", "y^3 - x^2 = 0")]
        public void TransformatorSortPowerTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("(x^2)(y^2) = -y^3", "y^3 + (x^2)(y^2) = 0")]
        public void TransformatorMultiplePoweredVariablesTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("x^-2 = 0", "x^(-2) = 0")]
        [InlineData("x^(-2) = 0", "x^(-2) = 0")]
        public void TransformatorNegativePowerTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }

        [Theory]
        [InlineData("(x^2) = 0", "x^2 = 0")]
        public void TransformatorFullExpressionBracketsTest(string inputEquation, string outputEquation)
        {
            var result = _transformator.TransformToCanonical(inputEquation);
            Assert.Equal(outputEquation, result);
        }
    }
}
