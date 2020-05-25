using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            // This method gets called *once* before *each* test,
            // so can be used to initialise or reset the _calculator field.
            _calculator = new Calculator();
        }

        [TestCase(12, 24)]
        public void AddTwoNumbers(int firstNumber, int secondNumber)
        {
            // Arrange - the Setup method handles the Arrange for us

            // Act
            _calculator.SetFirstNumber(firstNumber);
            _calculator.SetSecondNumber(secondNumber);
            _calculator.AddNumbers();

            var result = _calculator.Result;

            // Assert
            Assert.AreEqual(36, result);
        }
    }
}