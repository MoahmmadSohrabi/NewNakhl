using System.ComponentModel.DataAnnotations.Schema;

namespace NakhleNakhoda.Domain.Logging
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public long? EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity name
        /// </summary>
        public string EntityName { get; set; } = "";

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user browser 
        /// </summary>
        public string UserBrowser { get; set; } = "";

        /// <summary>
        /// Gets or sets the IP address
        /// </summary>
        public string IpAddress { get; set; } = "";

        /// <summary>
        /// Gets or sets the Short Message
        /// </summary>
        public string ShortMessage { get; set; } = "";

        /// <summary>
        /// Gets or sets the Full Message
        /// </summary>
        public string FullMessage { get; set; } = "";

        /// <summary>
        /// Gets or sets the method identifier
        /// </summary>
        public int MethodId { get; set; }

        /// <summary>
        /// Gets or sets the log level identifier
        /// </summary>
        public int LogLevelId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the log level
        /// </summary>
        [NotMapped]
        public LogLevel LogLevel
        {
            get => (LogLevel)LogLevelId;
            set => LogLevelId = (int)value;
        }

        /// <summary>
        /// Gets or sets the Method
        /// </summary>
        [NotMapped]
        public Method Method
        {
            get => (Method)MethodId;
            set => MethodId = (int)value;
        }
    }
}
