namespace DynatronWebApi.Entities
{
    /// <summary>
    /// Represents a base entity with common properties.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets the unique identifier of the entity.
        /// </summary>
        public Guid Id { get; protected init; }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets the date and time when the entity was last modified, or null if it has not been modified.
        /// </summary>
        public DateTime? LastUpdated { get; set; }
    }
}