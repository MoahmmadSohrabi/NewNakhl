using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Context;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.Services.Identity
{
    public interface IUserService : IRepository<Member>
    {
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        Task<IList<MemberModel>> GetUsers();
        Member GetUserRole(long id);
        Task<Member?> GetByPhoneNumber(string phone);
        Task<Member?> GetByUserId(long userId);

    }

    public class UserService : Repository<Member>, IUserService
    {
        //private readonly IPictureService _pictureService;
        private readonly IUserRoleService _userRoleService;

        public UserService(
            IUnitOfWork uow,
            //IPictureService pictureService,
            IUserRoleService userRoleService) : base(uow)
        {
            //_pictureService = pictureService;
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>Users</returns>
        public virtual async Task<IList<MemberModel>> GetUsers()
        {
            var query = Table;
            query = query.Where(x => !x.Deleted);
            query = query.OrderBy(x => x.Id);
            query = from c in query
                    select c;

            var items = new List<MemberModel>();
            foreach (var x in await query.ToListAsync())
            {
                var user = new MemberModel
                {
                    Id = x.Id,
                    DisplayName = x.DisplayName,
                    UserName = x.UserName ?? "",
                    Email = x.Email ?? "",
                    PhoneNumber = x.PhoneNumber ?? "",
                    Active = x.Active,
                    //PictureModel = new PictureModel
                    //{
                    //    ThumbImageUrl = await _pictureService.GetPictureUrl(x.PictureId, 75, true, null, Domain.Media.PictureType.Avatar),
                    //},
                };
                // select user role names
                var userRole = await _userRoleService.GetRoleNamesByUserId(user.Id);
                user.RoleNames = string.Join(", ", userRole);

                items.Add(user);
            }

            return items;
        }

        public Member GetUserRole(long id)
        {
            var query = GetByInclude(
              null, x => x.Id.Equals(id), null,
              include: User => User.Include(a => a.UserRole).ThenInclude(a => a.Role!));

            return query;
        }

        public async Task<Member?> GetByPhoneNumber(string phone)
        {
            var query = Table;
            query = query.Where(x => x.PhoneNumber!.Equals(phone));
            query = from c in query
                    select c;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Member?> GetByUserId(long userId)
        {
            var query = Table;
            query = query.Where(x => x.Id.Equals(userId));
            query = from c in query
                    select c;
            return await query.FirstOrDefaultAsync();
        }
    }
}