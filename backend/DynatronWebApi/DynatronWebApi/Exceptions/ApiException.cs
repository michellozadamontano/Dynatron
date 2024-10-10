using System.Globalization;

namespace DynatronWebApi.Exceptions
{
    /// <summary>
    ///     ApiException class
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        ///     ApiException constructor
        /// </summary>
        public ApiException()
        {
        }

        /// <summary>
        ///     ApiException constructor
        /// </summary>
        /// <param name="message"></param>
        public ApiException(string message) : base(message)
        {
        }

        /// <summary>
        ///     ApiException constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public ApiException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

        /// <summary>
        ///     ApiException constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}