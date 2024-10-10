using FluentValidation.Results;

namespace DynatronWebApi.Exceptions
{
    /// <summary>
    ///     ValidationException class
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        ///     ValidationException constructor
        /// </summary>
        public ValidationException() : base("One or more validation failures have occurred.")
        {
            Errors = new List<string>();
        }

        /// <summary>
        ///     ValidationException constructor
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures) Errors.Add(failure.ErrorMessage);
        }

        /// <inheritdoc />
        public ValidationException(string message, IDictionary<string, string[]> validationErrors, List<string> errors) :
            base(message)
        {
            ValidationErrors = validationErrors;
            Errors = errors;
        }

        /// <summary>
        ///     ValidationException constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        /// <param name="validationErrors"></param>
        /// <param name="errors"></param>
        public ValidationException(string message, Exception innerException, IDictionary<string, string[]> validationErrors,
            List<string> errors) : base(message, innerException)
        {
            ValidationErrors = validationErrors;
            Errors = errors;
        }

        /// <summary>
        ///     ValidationException constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationResult"></param>
        /// <param name="validationErrors"></param>
        /// <param name="errors"></param>
        public ValidationException(string message, ValidationResult validationResult,
            IDictionary<string, string[]> validationErrors, List<string> errors) : base(message)
        {
            Errors = errors;
            ValidationErrors = validationResult.ToDictionary();
        }

        /// <summary>
        ///     Constructor for handle validations
        /// </summary>
        /// <param name="message"></param>
        /// <param name="validationResult"></param>
        public ValidationException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        /// <summary>
        ///     ValidationErrors property
        /// </summary>
        public IDictionary<string, string[]> ValidationErrors { get; set; } = null!;

        /// <summary>
        ///     Errors property
        /// </summary>
        public List<string> Errors { get; }
    }
}