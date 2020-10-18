using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeToTest.Tests
{
    [TestClass]
    public class AlgorithmsTest
    {
        private Algorithms _SUT;

        public AlgorithmsTest()
        {
            _SUT = new Algorithms();
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = _SUT.IsPrime(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FinMin_AllValuesPositive_ShouldReturnCorrectValue()
        {
            // Arrange
            var arr = new[] { 10, 5, 20, 1, 33, 2, 46, 47 };

            // Act
            var min = _SUT.FindMin(arr);

            // Assert
            Assert.AreEqual(1, min);
        }
    }

}
