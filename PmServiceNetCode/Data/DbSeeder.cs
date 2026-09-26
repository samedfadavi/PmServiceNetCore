using Microsoft.EntityFrameworkCore;
using PmServiceNetCode.Models;
using pmService.Models;
using Endpoint = PmServiceNetCode.Models.Endpoint;

namespace PmServiceNetCode.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MaznetModel context)
        {
            // 1. Role
            // 5. Endpoints
            var endpointDefinitions = new[]
            {
    new
    {
        HttpMethod = "GET",
        Route = "/api/Farayand",
        Description = "Get all Farayand records",
        PermissionName = "Farayand.Read"
    },
    new
    {
        HttpMethod = "POST",
        Route = "/api/Farayand",
        Description = "Create a Farayand record",
        PermissionName = "Farayand.Create"
    },
    new
    {
        HttpMethod = "PUT",
        Route = "/api/Farayand/{id}",
        Description = "Update a Farayand record",
        PermissionName = "Farayand.Update"
    },
    new
    {
        HttpMethod = "DELETE",
        Route = "/api/Farayand/{id}",
        Description = "Delete a Farayand record",
        PermissionName = "Farayand.Delete"
    }
};

            foreach (var definition in endpointDefinitions)
            {
                var endpoint = await context.Endpoints
                    .FirstOrDefaultAsync(x =>
                        x.HttpMethod == definition.HttpMethod &&
                        x.Route == definition.Route);

                if (endpoint == null)
                {
                    endpoint = new Endpoint
                    {
                        HttpMethod = definition.HttpMethod,
                        Route = definition.Route,
                        Description = definition.Description
                    };

                    context.Endpoints.Add(endpoint);
                    await context.SaveChangesAsync();
                }

                var permission = await context.Permissions
                    .FirstOrDefaultAsync(x => x.Name == definition.PermissionName);

                if (permission != null)
                {
                    var endpointPermissionExists = await context.EndpointPermissions
                        .AnyAsync(x =>
                            x.EndpointId == endpoint.Id &&
                            x.PermissionId == permission.Id);

                    if (!endpointPermissionExists)
                    {
                        context.EndpointPermissions.Add(new EndpointPermission
                        {
                            EndpointId = endpoint.Id,
                            PermissionId = permission.Id
                        });
                    }
                }
            }

            await context.SaveChangesAsync();

           
        }
    }
}