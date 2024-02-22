namespace NumberConverterDemo.Core.Converter
{
    public interface INumberConverter
    {
        /// <summary>
        /// True/False wether the given number can be converted.
        /// </summary>
        /// <param name="number">The given number.</param>
        /// <returns>True if the number can be converted, otherwise false.</returns>
        bool CanConvert(decimal number);

        /// <summary>
        /// Converts the number into a readable string representation.
        /// </summary>
        /// <param name="number">The given number.</param>
        /// <returns>The number as readable string.</returns>
        string Convert(decimal number);
    }
}
