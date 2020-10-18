using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeToTest.Tests
{
    [TestClass]
    public class AlgorithmsTest
    {
        private Algorithms _SUT;

        // Bad practice
        public AlgorithmsTest()
        {
            _SUT = new Algorithms();
        }

        [DataTestMethod]
        [DataRow(new[] { 10, 15, 30, 1, 22, 2 })]
        [DataRow(new[] { 4, 5, 40, 1, 33, 2000 })]
        public void FinMin_AllValuesPositive_ReturnCorrectValue(int[] arr)
        {
            var min = _SUT.FindMin(arr);

            Assert.AreEqual(1, min);
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

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(5)]
        public void IsPrime_ValuesGreaterThan2_ReturnTrue(int value)
        {
            var result = _SUT.IsPrime(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPrime_NotPrimeNumber_ReturnFalse()
        {
            // Arrange/Act
            var result = _SUT.IsPrime(10);

            // Assert
            Assert.IsFalse(result);
        }
    }

}
