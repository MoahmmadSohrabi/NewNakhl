using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Identity;

namespace NakhleNakhoda.Services.Identity
{
    public interface IUserRoleService : IRepository<MemberRole>
    {
        Task<IList<long>> GetRoleIdsByUserId(long userId);
        Task<IList<MemberRole>> GetUserRoleByUserId(long userId);

        /// <summary>
        /// Gets a role mapping collection
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns> role name collection</returns>
        Task<IList<string>> GetRoleNamesByUserId(long userId);
    }

    public class UserRoleService : Repository<MemberRole>, IUserRoleService
    {
        public UserRoleService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IList<long>> GetRoleIdsByUserId(long userId)
        {
            var query = from ur in Table
                        where ur.UserId.Equals(userId)
                        select ur.RoleId;
            return await query.ToListAsync();
        }

        public async Task<IList<MemberRole>> GetUserRoleByUserId(long userId)
        {
            var query = from ur in Table
                        where ur.UserId.Equals(userId)
                        select ur;
            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets a role mapping collection
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns> role name collection</returns>
        public virtual async Task<IList<string>> GetRoleNamesByUserId(long userId)
        {
            if (userId == 0)
                return new List<string>();

            var query = from ur in Table
                        where ur.UserId.Equals(userId)
                        select ur.Role!.Description;

            return await query.ToListAsync();
        }
    }
}