namespace NumberConverterDemo.Core.Converter
{
    using System;

    public class DefaultNumberConverter : INumberConverter
    {
        /// <summary>
        /// True/False wether the given number can be converted.
        /// </summary>
        /// <param name="number">The given number.</param>
        /// <returns>True if the number can be converted, otherwise false.</returns>
        public bool CanConvert(decimal number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the number into a readable string representation.
        /// </summary>
        /// <param name="number">The given number.</param>
        /// <returns>The number as readable string.</returns>
        public string Convert(decimal number)
        {
            throw new NotImplementedException();
        }
    }
}
