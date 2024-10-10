namespace DynatronWebApi.Entities
{
    /// <summary>
    /// Represents a customer entity with common properties.
    /// </summary>
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets or sets the email of the customer.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        private Customer()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class with the specified first name, last name, and email.
        /// </summary>
        /// <param name="firstName">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        /// <param name="email">The email of the customer.</param>
        public Customer(string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        /// <summary>
        /// Updates the customer with the specified first name, last name, and email.
        /// </summary>
        /// <param name="firstName">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        /// <param name="email">The email of the customer.</param>
        public void Update(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }
}