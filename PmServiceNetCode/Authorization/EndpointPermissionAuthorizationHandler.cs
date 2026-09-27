using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pmService.Models;

namespace PmServiceNetCode.Authorization
{
    public class EndpointPermissionAuthorizationHandler
        : AuthorizationHandler<EndpointPermissionRequirement>
    {
        private readonly MaznetModel _context;

        public EndpointPermissionAuthorizationHandler(MaznetModel context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            EndpointPermissionRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
                return;

            var method = httpContext.Request.Method;

            var routeEndpoint = httpContext.GetEndpoint()
                as Microsoft.AspNetCore.Routing.RouteEndpoint;

            if (routeEndpoint == null)
                return;

            var route = routeEndpoint.RoutePattern.RawText;

            if (string.IsNullOrEmpty(route))
                return;

            var endpoint = await _context.Endpoints
                .Include(e => e.EndpointPermissions)
                    .ThenInclude(ep => ep.Permission)
                .FirstOrDefaultAsync(e =>
                    e.HttpMethod == method &&
                    e.Route == route);

            if (endpoint == null)
                return;

            var requiredPermissions = endpoint.EndpointPermissions
                .Select(ep => ep.Permission.Name)
                .ToList();

            var hasPermission = requiredPermissions.Any(permission =>
                context.User.HasClaim("permission", permission));

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}