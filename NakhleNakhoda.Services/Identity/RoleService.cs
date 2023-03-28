using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.Services.Identity
{
    public interface IRoleService : IRepository<Role>
    {
        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <param name="showPublished">A value indicating whether to show hidden records</param>
        /// <returns>Roles</returns>
        Task<IList<Role>> GetRoles();

        Task<IList<RoleModel>> GetAllRole();

        Task<Role> FindRoleIncludeRoleClaimsAsync(long roleId);
        Task AddOrUpdateRoleClaimsAsync(long roleId, string roleClaimType, IList<string> selectedRoleClaimValues);
    }

    public class RoleService : Repository<Role>, IRoleService
    {
        public RoleService(IUnitOfWork uow) : base(uow)
        {
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <param name="showPublished">A value indicating whether to show hidden records</param>
        /// <returns>Categories</returns>
        public virtual async Task<IList<Role>> GetRoles()
        {
            var query = Table;
            query = query.OrderByDescending(x => x.Id);
            query = from c in query
                    select c;

            return await query.ToListAsync();
        }

        public async Task<IList<RoleModel>> GetAllRole()
        {
            var model = await GetAllByEnumerableAsync();
            return model.OrderByDescending(x => x.Id).Select(e => new RoleModel
            {
                Id = e.Id,
                Name = e.Name!,
                Description = e.Description,
            }).ToList();
        }

        public async Task<Role> FindRoleIncludeRoleClaimsAsync(long roleId)
        {
            return (await Table.Include(x => x.RoleClaim).FirstOrDefaultAsync(x => x.Id == roleId))!;
        }

        public async Task AddOrUpdateRoleClaimsAsync(
            long roleId,
            string roleClaimType,
            IList<string> selectedRoleClaimValues)
        {
            var role = await FindRoleIncludeRoleClaimsAsync(roleId);
            /* if (role == null)
             {
                 return IdentityResult.Failed(new IdentityError
                 {
                     Code = "RoleNotFound",
                     Description = "نقش مورد نظر یافت نشد."
                 });
             }
 */
            var currentRoleClaimValues = role.RoleClaim.Where(roleClaim => roleClaim.ClaimType == roleClaimType)
                                                    .Select(roleClaim => roleClaim.ClaimValue)
                                                    .ToList();

            selectedRoleClaimValues ??= new List<string>();
            var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
            foreach (var claimValue in newClaimValuesToAdd)
            {
                role.RoleClaim.Add(new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = roleClaimType,
                    ClaimValue = claimValue
                });
            }

            var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
            foreach (var claimValue in removedClaimValues)
            {
                var roleClaim = role.RoleClaim.SingleOrDefault(rc => rc.ClaimValue == claimValue &&
                                                                  rc.ClaimType == roleClaimType);
                if (roleClaim != null)
                {
                    role.RoleClaim.Remove(roleClaim);
                }
            }

            Entities.UpdateRange(role);
            await SaveAsync();

            //return await _roleManager.UpdateAsync(role);
        }
    }
}