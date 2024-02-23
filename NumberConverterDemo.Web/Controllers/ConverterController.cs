namespace NumberConverterDemo.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NumberConverterDemo.Core.Converter;
    using NumberConverterDemo.Web.Models;

    [ApiController]
    [Route("[controller]")]
    public class ConverterController : ControllerBase
    {
        /// <summary>Initializes a new instance of the <see cref="ConverterController" /> class.</summary>
        /// <param name="numberConverter">The number converter.</param>
        /// <param name="logger">The logger.</param>
        public ConverterController(INumberConverter numberConverter, ILogger<ConverterController> logger)
        {
            this.NumberConverter = numberConverter;
            this.Logger = logger;
        }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; private set; }

        /// <summary>Gets the number converter.</summary>
        /// <value>The number converter.</value>
        protected INumberConverter NumberConverter { get; private set; }

        /// <summary>
        /// Converts a user specified number into a readable string representation.
        /// </summary>
        /// <param name="input">The number given by the user.</param>
        /// <returns>The converted number.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">number</exception>
        [HttpGet("{number:decimal}")]
        public async Task<NumberConversion> Get(decimal number)
        {
            if (!this.NumberConverter.CanConvert(number)) // make sure the given number can be converted
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // actually convert it now
            var result = this.NumberConverter.Convert(number);

            return new NumberConversion
            {
                Number = number,
                Text = await result
            };
        }
    }
}