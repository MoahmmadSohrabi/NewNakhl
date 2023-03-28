using Microsoft.AspNetCore.Http;
using NakhleNakhoda.Common.IdentityToolkit;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain;
using NakhleNakhoda.Domain.Logging;

namespace NakhleNakhoda.Services.Logging
{
    /// <summary>
    /// Log service
    /// </summary>
    public class LoggerService : Repository<Log>, ILoggerService
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        public LoggerService(IUnitOfWork uow, IHttpContextAccessor httpContextAccessor) : base(uow)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether a log level is enabled
        /// </summary>
        /// <param name="level">Log level</param>
        /// <returns>Result</returns>
        public virtual bool IsEnabled(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => false,
                _ => true,
            };
        }

        /// <summary>
        /// Inserts an log item
        /// </summary>
        /// <param name="logLevel">LogLevel</param>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        /// <returns>Log item</returns>
        public virtual async Task<Log> InsertLog(LogLevel logLevel, Method method, BaseEntity? entity = null, string shortMessage = "", string fullMessage = "")
        {
            var httpContext = _httpContextAccessor?.HttpContext;

            var log = new Log
            {
                EntityId = entity?.Id,
                EntityName = entity?.GetType().Name ?? "Unknown",
                ShortMessage = shortMessage,
                FullMessage = fullMessage,
                IpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown",
                UserId = httpContext == null ? 0 : GetUserId(httpContext),
                UserBrowser = httpContext?.Request?.Headers["User-Agent"].ToString() ?? "Unknown",
                LogLevel = logLevel,
                Method = method,
                CreatedDate = DateTime.UtcNow
            };

            await InsertAsync(log);

            return log;
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        public virtual async Task Information(Method method, BaseEntity? entity, string shortMessage, Exception? exception)
        {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;
            //InsertLog(LogLevel logLevel, Entities.Logging.Action action, Method method, long entityId, BaseEntity entity = null, string shortMessage = "", string fullMessage = "")
            if (IsEnabled(LogLevel.Information))
                await InsertLog(LogLevel.Information, method, entity, shortMessage, exception?.ToString() ?? string.Empty);
        }

        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        public virtual async Task Warning(Method method, BaseEntity? entity, string shortMessage, Exception? exception)
        {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Warning))
                await InsertLog(LogLevel.Warning, method, entity, shortMessage, exception?.ToString() ?? string.Empty);
        }

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="method">Method</param>
        /// <param name="entity">Entity</param>
        /// <param name="short message">Message</param>
        /// <param name="full message">Message</param>
        public virtual async Task Error(Method method, BaseEntity? entity, string shortMessage, Exception? exception)
        {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;

            if (IsEnabled(LogLevel.Error))
                await InsertLog(LogLevel.Error, method, entity, shortMessage, exception?.ToString() ?? string.Empty);
        }


        #endregion

        #region Properties

        private static long? GetUserId(HttpContext httpContext)
        {
            long? userId = null;
            var userIdValue = httpContext?.User?.Identity?.GetUserId();
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                userId = long.Parse(userIdValue);
            }
            return userId;
        }

        #endregion

    }
}