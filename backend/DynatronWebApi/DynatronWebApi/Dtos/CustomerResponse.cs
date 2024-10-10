namespace DynatronWebApi.Dtos
{
    /// <summary>
    /// Represents a customer response.
    /// </summary>
    /// <param name="Id">The unique identifier of the customer.</param>
    /// <param name="FirstName">The first name of the customer.</param>
    /// <param name="LastName">The last name of the customer.</param>
    /// <param name="Email">The email address of the customer.</param>
    /// <param name="Created">The date and time when the customer was created.</param>
    /// <param name="LastUpdated">The date and time when the customer was last updated, or null if the customer has never been updated.</param>
    public record CustomerResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        DateTime Created,
        DateTime? LastUpdated);
}