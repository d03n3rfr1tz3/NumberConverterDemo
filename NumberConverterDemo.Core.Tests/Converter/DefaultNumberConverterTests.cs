namespace NumberConverterDemo.Core.Tests.Converter
{
    using NumberConverterDemo.Core.Converter;

    [TestClass]
    public class DefaultNumberConverterTests
    {
        [TestMethod]
        [TestCategory("NumberConverter.CanConvert")]
        [Description("Tests the CanConvert method with numbers that should give a positive result.")]
        [DataRow(0)]
        [DataRow(1.99)]
        [DataRow(100.90)]
        [DataRow(1000.00)]
        [DataRow(999999999.99)]
        public void CanConvertPositiveTest(double number)
        {
            // prepare
            var converter = new DefaultNumberConverter();

            // act
            var result = converter.CanConvert((decimal)number);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [TestCategory("NumberConverter.CanConvert")]
        [Description("Tests the CanConvert method with numbers that should give a negative result.")]
        [DataRow(-1)]
        [DataRow(1.999)]
        [DataRow(999999999.999)]
        [DataRow(1000000000.00)]
        public void CanConvertNegativeTest(double number)
        {
            // prepare
            var converter = new DefaultNumberConverter();

            // act
            var result = converter.CanConvert((decimal)number);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        [TestCategory("NumberConverter.Convert")]
        [Description("Tests the Convert method with the numbers from the Coding Challenge requirements.")]
        [DataRow(0, "zero dollars")]
        [DataRow(1, "one dollar")]
        [DataRow(0.01, "zero dollars and one cent")]
        [DataRow(25.10, "twenty-five dollars and ten cents")]
        [DataRow(45100.00, "forty-five thousand one hundred dollars")]
        [DataRow(999999999.99, "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        public async Task ConvertRequirementsTest(double number, string expected)
        {
            // prepare
            var converter = new DefaultNumberConverter();

            // act
            var result = await converter.Convert((decimal)number);

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("NumberConverter.Convert")]
        [Description("Tests the Convert method with some additional well known numbers.")]
        [DataRow(42, "forty-two dollars")]
        [DataRow(69, "sixty-nine dollars")]
        [DataRow(100, "one hundred dollars")]
        [DataRow(1337, "one thousand three hundred thirty-seven dollars")]
        [DataRow(9000, "nine thousand dollars")]
        [DataRow(08.15, "eight dollars and fifteen cents")]
        [DataRow(4711.50, "four thousand seven hundred eleven dollars and fifty cents")]
        [DataRow(123456789, "one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine dollars")]
        [DataRow(9876543.21, "nine million eight hundred seventy-six thousand five hundred forty-three dollars and twenty-one cents")]
        [DataRow(100000000.00, "one hundred million dollars")]
        public async Task ConvertAdditionalTest(double number, string expected)
        {
            // prepare
            var converter = new DefaultNumberConverter();

            // act
            var result = await converter.Convert((decimal)number);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}