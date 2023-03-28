namespace NakhleNakhoda.Domain.Auditable
{
    public interface IAuditable
    {

        /// <summary>
        /// Gets or sets the date and time of auditable creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the date and time of auditable update
        /// </summary>
        public DateTime UpdatedOnUtc { get; set; }
    }
}
