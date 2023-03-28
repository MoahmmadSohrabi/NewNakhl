using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace NakhleNakhoda.Services.Security
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicPermissionsAuthorizationHandler(
            ISecurityTrimmingService securityTrimmingService,
            IHttpContextAccessor httpContextAccessor)
        {
            _securityTrimmingService = securityTrimmingService ?? throw new ArgumentNullException(nameof(securityTrimmingService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected override async Task HandleRequirementAsync(
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var routeData = _httpContextAccessor?.HttpContext?.GetRouteData();

            var areaName = routeData?.Values["area"]?.ToString();
            var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

            var controllerName = routeData?.Values["controller"]?.ToString();
            var controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

            var actionName = routeData?.Values["action"]?.ToString();
            var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;

            if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}