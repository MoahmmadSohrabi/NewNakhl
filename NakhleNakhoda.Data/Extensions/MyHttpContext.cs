using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NakhleNakhoda.Data.Extensions
{
    public class MyHttpContext
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static HttpContext? Current => _httpContextAccessor?.HttpContext;

        public static string AppBaseUrl => $"{Current?.Request.Scheme}://{Current?.Request.Host}{Current?.Request.PathBase}/";

        internal static void Configure(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }
    }

    public static class HttpContextExtensions
    {
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static IApplicationBuilder UseHttpContext(this IApplicationBuilder app)
        {
            MyHttpContext.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            return app;
        }
    }
}
