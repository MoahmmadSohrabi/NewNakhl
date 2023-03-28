using Microsoft.AspNetCore.Authorization;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Services.Security;

namespace NakhleNakhoda.Web.Core.Extensions
{
    public static class AddDynamicPermissionsExtensions
    {
        public static IServiceCollection AddDynamicPermissions(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(
                    name: PolicieNames.DynamicPermission,
                    configurePolicy: policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new DynamicPermissionRequirement());
                    });
            });

            return services;
        }
    }
}