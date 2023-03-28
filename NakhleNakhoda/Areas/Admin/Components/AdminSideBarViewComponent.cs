using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Domain.Media;
using NakhleNakhoda.Services.Identity;
using NakhleNakhoda.Services.Media;
using NakhleNakhoda.ViewModels.Admin.Identity;
using NakhleNakhoda.ViewModels.Admin.Media;

namespace NakhleNakhoda.Web.Areas.Admin.Components
{
    public class AdminSideBarViewComponent : ViewComponent
    {
        #region Fields

        private readonly UserManager<Member> _userManager;
        private readonly IUserService _userService;
        private readonly IPictureService _pictureService;

        #endregion

        #region Ctor

        public AdminSideBarViewComponent(
            UserManager<Member> userManager,
            IUserService userService,
            IPictureService pictureService)
        {
            _userManager = userManager;
            _userService = userService;
            _pictureService = pictureService;
        }

        #endregion

        #region Methods

        public async Task<IViewComponentResult> InvokeAsync()
        {
            MemberModel model = new();
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            if (user != null)
            {
                model.Email = user!.Email!;
                model.UserName = user!.UserName!;
                model.DisplayName = user.DisplayName;
                model.RoleNames = _userService.GetUserRole(user.Id).UserRole?.FirstOrDefault()?.Role?.Description;

                model.PictureModel = new PictureModel
                {
                    ImageUrl = await _pictureService.GetPictureUrl(user.PictureId, 160, true, null, PictureType.Avatar),
                };
            }

            return View(model);
        }

        #endregion
    }
}