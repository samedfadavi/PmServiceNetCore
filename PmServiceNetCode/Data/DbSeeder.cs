using Microsoft.EntityFrameworkCore;
using PmServiceNetCode.Models;
using pmService.Models;

namespace PmServiceNetCode.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(MaznetModel context)
        {
            // 1. Role
            var adminRole = await context.Roles
                .FirstOrDefaultAsync(x => x.Name == "Admin");

            if (adminRole == null)
            {
                adminRole = new Role
                {
                    Name = "Admin",
                    Description = "System administrator"
                };

                context.Roles.Add(adminRole);
                await context.SaveChangesAsync();
            }

            // 2. Permissions
            var permissionNames = new[]
            {
                "Farayand.Read",
                "Farayand.Create",
                "Farayand.Update",
                "Farayand.Delete"
            };

            foreach (var permissionName in permissionNames)
            {
                var permission = await context.Permissions
                    .FirstOrDefaultAsync(x => x.Name == permissionName);

                if (permission == null)
                {
                    context.Permissions.Add(new Permission
                    {
                        Name = permissionName,
                        Description = permissionName
                    });
                }
            }

            await context.SaveChangesAsync();

            // 3. Role -> Permissions
            var permissions = await context.Permissions
                .ToListAsync();

            foreach (var permission in permissions)
            {
                if (!permissionNames.Contains(permission.Name))
                    continue;

                var exists = await context.RolePermissions
                    .AnyAsync(x =>
                        x.RoleId == adminRole.Id &&
                        x.PermissionId == permission.Id);

                if (!exists)
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            // 4. User -> Admin Role
            var user = await context.Users
                .FirstOrDefaultAsync();

            if (user != null)
            {
                var userRoleExists = await context.UserRoles
                    .AnyAsync(x =>
                        x.UserId == user.Code_User &&
                        x.RoleId == adminRole.Id);

                if (!userRoleExists)
                {
                    context.UserRoles.Add(new UserRole
                    {
                        UserId = user.Code_User,
                        RoleId = adminRole.Id
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}