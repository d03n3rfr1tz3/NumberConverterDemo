namespace NumberConverterDemo.Core.Converter
{
    using System;
    using System.Linq;
    using System.Text;

    public class DefaultNumberConverter : INumberConverter
    {
        private static IList<string> LowerDigitMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static IList<string> HigherDigitMap = new[] { string.Empty, "teen", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        private static IDictionary<long, string> OtherDigitMap = new Dictionary<long, string> { { 100, "hundred" }, { 1000, "thousand" }, { 1000000, "million" } };
        private static IList<string> LowerCurrencyMap = new[] { "cents", "cent" };
        private static IList<string> HigherCurrencyMap = new[] { "dollars", "dollar" };

        /// <summary>
        /// Determines wether the given number can be converted.
        /// </summary>
        /// <param name="number">The given number.</param>
        /// <returns>True if the number can be converted, otherwise false.</returns>
        public bool CanConvert(decimal number)
        {
            return number >= 0 &&                                   // number is zero or higher
                   number < 1000000000 &&                           // number is lower then 1 000 000 000
                   (number - decimal.Truncate(number)).Scale <= 2;  // number has not more then 2 digits after the point
        }

        /// <summary>
        /// Converts the irrational number into a readable currency representation like a human would read it.
        /// </summary>
        /// <param name="number">The given irrational number.</param>
        /// <returns>The irrational number as readable currency.</returns>
        public async Task<string> Convert(decimal number)
        {
            var result = new StringBuilder();

            var numberDollars = decimal.Truncate(number);
            var numberCents = (number - decimal.Truncate(number)) * 100;
            var resultDollars = Task.Run(() => this.ConvertNumber(numberDollars));
            var resultCents = Task.Run(() => this.ConvertNumber(numberCents));

            // wait for the dollars-task and append it
            var higherCurrency = HigherCurrencyMap.ElementAtOrDefault((int)numberDollars) ?? HigherCurrencyMap[0];
            result.Append(string.Join(" ", (await resultDollars).Concat(new[] { higherCurrency })));

            // wait for the cents-task and append it, if it is not zero
            if (numberCents > 0)
            {
                var lowerCurrency = LowerCurrencyMap.ElementAtOrDefault((int)numberCents) ?? LowerCurrencyMap[0];
                result.Append(" and ");
                result.Append(string.Join(" ", (await resultCents).Concat(new[] { lowerCurrency })));
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts the natural number into a readable number representation like a human would read it.
        /// </summary>
        /// <param name="input">The given natural number.</param>
        /// <returns>The natural number as readable number.</returns>
        private IEnumerable<string> ConvertNumber(decimal input)
        {
            var result = new List<string>();
            var number = Math.Truncate(input);

            // recursively convert from higher to lower numbers for the configured tresholds
            foreach (var otherDigitPair in OtherDigitMap.Reverse())
            {
                // checking for treshold
                if (((int)number / otherDigitPair.Key) > 0)
                {
                    // divide number to get something lower then 100, convert it and set the number to the remaining value
                    var convertedChilds = this.ConvertNumber((int)number / otherDigitPair.Key);
                    number %= otherDigitPair.Key;

                    result.AddRange(convertedChilds);
                    result.Add(otherDigitPair.Value);
                }
            }

            // convert numbers lower then the lowest threshold
            if (number >= LowerDigitMap.Count)
            {
                // higher two-parted numbers between 20 and 99
                var higherIndex = (int)number / 10;
                var lowerIndex = (int)number - (higherIndex * 10);
                var higherDigit = HigherDigitMap[higherIndex];
                var lowerDigit = LowerDigitMap[lowerIndex];

                if (lowerIndex > 0)
                {
                    result.Add($"{higherDigit}-{lowerDigit}");
                }
                else
                {
                    result.Add($"{higherDigit}");
                }
            }
            else if (number > 0 || number == Math.Truncate(input))
            {
                // lower numbers with concrete mapping
                var lowerDigit = LowerDigitMap[(int)number];
                result.Add(lowerDigit);
            }

            return result;
        }
    }
}
