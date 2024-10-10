namespace DynatronWebApi.Dtos
{
    /// <summary>
    /// Represents a request to create a new customer.
    /// </summary>
    public record CustomerRequest(
        /// <summary>
        /// The first name of the customer.
        /// </summary>
        string FirstName,

        /// <summary>
        /// The last name of the customer.
        /// </summary>
        string LastName,

        /// <summary>
        /// The email address of the customer.
        /// </summary>
        string Email);
}