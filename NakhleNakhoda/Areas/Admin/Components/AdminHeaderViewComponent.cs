using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.Web.Areas.Admin.Components
{
    public class AdminHeaderViewComponent : ViewComponent
    {
        #region Fields

        private readonly UserManager<Member> _userManager;
        //private readonly IRoleService _roleService;
        //private readonly IUserService _userService;
        //private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public AdminHeaderViewComponent(
            UserManager<Member> userManager
            )
        {
            this._userManager = userManager;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MemberModel model = new MemberModel();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                model.Email = user.Email!;
                model.UserName = user.UserName!;
                model.DisplayName = user.DisplayName;
                //model.RoleNames = _userService.GetUserRole(user.Id).UserRole.OrderBy(x => x.RoleId).FirstOrDefault()!.Role.Description;
            }

            //model.PictureModel = new PictureModel
            //{
            //ImageUrl = await _pictureService.GetPictureUrl(user.PictureId, 160, true, null, PictureType.Avatar),
            //};
            return View(model);
        }

        #endregion
    }
}