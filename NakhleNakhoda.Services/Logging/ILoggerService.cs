using NakhleNakhoda.Domain;
using NakhleNakhoda.Domain.Logging;

namespace NakhleNakhoda.Services.Logging
{
    /// <summary>
    /// Log service interface
    /// </summary>
    public interface ILoggerService : IRepository<Log>
    {
        /// <summary>
        /// Inserts an log item
        /// </summary>
        /// <param name="logLevel">LogLevel</param>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        /// <returns>Log item</returns>
        Task<Log> InsertLog(LogLevel logLevel, Method method, BaseEntity? entity = null, string shortMessage = "", string fullMessage = "");

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        Task Information(Method method, BaseEntity? entity = null, string shortMessage = "", Exception? exception = null);

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        Task Warning(Method method, BaseEntity? entity = null, string shortMessage = "", Exception? exception = null);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        Task Error(Method method, BaseEntity? entity = null, string shortMessage = "", Exception? exception = null);

    }
}
