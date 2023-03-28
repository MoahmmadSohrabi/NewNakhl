using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Identity;
using NakhleNakhoda.Services.Media;
using NakhleNakhoda.Services.Identity;
using NakhleNakhoda.Web.Areas.Admin.Factories;
using AutoMapper;
using NakhleNakhoda.Services.Logging;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Common;
using Microsoft.EntityFrameworkCore;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = PolicieNames.DynamicPermission)]
    [DisplayName("کاربران")]
    public class MemberController : BaseAdminController
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPictureService _pictureService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public MemberController(
            ILoggerService loggerService,
            IMapper mapper,
            IBaseAdminModelFactory baseAdminModelFactory,
            UserManager<Member> userManager,
            RoleManager<Role> roleManager,
            IPictureService pictureService,
            IUserService userService,
            IUserRoleService userRoleService
            )
        {
            _loggerService = loggerService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _userManager = userManager;
            _roleManager = roleManager;
            _pictureService = pictureService;
            _userService = userService;
            _userRoleService = userRoleService;
            _mapper = mapper;
        }

        [HttpGet]
        [DisplayName("لیست")]
        public virtual async Task<IActionResult> List()
        {
            var model = await _userService.GetUsers();
            return View(model);
            //return View();
        }

        [HttpGet]
        [DisplayName("جدید(Get)")]
        public async Task<IActionResult> Create()
        {
            var model = new MemberModel
            {
                Active = true
            };

            await _baseAdminModelFactory.PrepareRoles(model.AvailableRoles, false);

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("جدید(Post)")]
        public async Task<IActionResult> Create(MemberModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                //model.Organizations = await _organizationService.GetByIds(model.SelectedOrganizationIds);
                var user = _mapper.Map<Member>(model);
                var result = await _userManager.CreateAsync(user, Constants.DefaultPassword);

                if (result.Succeeded)
                {
                    var roles = await _roleManager.Roles.Where(x => model.SelectedRoleIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();
                    if (roles.Any())
                    {
                        IdentityResult roleResult = await _userManager.AddToRolesAsync(user, roles!);
                    }

                    //await _userOrganizationService.InsertAsync(model.SelectedOrganizationIds.Select(x => new UserOrganization
                    //{
                    //    UserId = user.Id,
                    //    OrganizationId = x,
                    //}));

                    if (!continueEditing)
                        return RedirectToAction(nameof(List));

                    return RedirectToAction(nameof(Edit), new { id = user.Id });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            await _baseAdminModelFactory.PrepareRoles(model.AvailableRoles, false);
            return View(model);
        }

        [HttpGet]
        [DisplayName("ویرایش(Get)")]
        public async Task<IActionResult> Edit(long? id)
        {
            var model = new MemberModel();

            if (id != null)
            {
                //User? user = await _userManager.FindByIdAsync(id.ToString()!);
                var user = await _userService.GetByIdAsync(id);
                //, include: User => User.Include(a => a.UserOrganizations)
                if (user == null || user.Deleted)
                    return RedirectToAction(nameof(List));

                if (user != null)
                {
                    model = _mapper.Map<MemberModel>(user);
                    await _baseAdminModelFactory.PrepareRoles(model.AvailableRoles, false);

                    model.SelectedRoleIds = await _userRoleService.GetRoleIdsByUserId(user.Id);
                    //model.SelectedOrganizationIds = user.UserOrganizations.Select(x => x.OrganizationId).ToList();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ویرایش(Post)")]
        public async Task<IActionResult> Edit(long id, MemberModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                if (user != null)
                {

                    user = _mapper.Map(model, user);

                    IdentityResult result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        //Delete Old Roles And Add New 
                        var oldUserRole = await _userRoleService.GetUserRoleByUserId(id);
                        await _userRoleService.DeleteAsync(oldUserRole);
                        var roles = await _roleManager.Roles.Where(x => model.SelectedRoleIds.Contains(x.Id)).Select(x => x.Name).ToListAsync();
                        if (roles.Any())
                        {
                            IdentityResult roleResult = await _userManager.AddToRolesAsync(user, roles!);
                        }

                        ////Delete Old Organization Then Add New
                        //await _userOrganizationService.DeleteByUserId(id);

                        //await _userOrganizationService.InsertAsync(model.SelectedOrganizationIds.Select(x => new UserOrganization
                        //{
                        //    UserId = user.Id,
                        //    OrganizationId = x,
                        //}));

                        if (!continueEditing)
                            return RedirectToAction(nameof(List));

                        return RedirectToAction(nameof(Edit), new { id = user.Id });
                    }
                }
            }
            await _baseAdminModelFactory.PrepareRoles(model.AvailableRoles, false);
            return View(model);
        }


        [HttpGet]
        [DisplayName("ویرایش رمز عبور(Get)")]
        public async Task<IActionResult> ChangePassword(long? id)
        {
            var model = new MemberPasswordModel();

            if (id != null)
            {
                Member? user = await _userManager.FindByIdAsync(id.ToString()!);
                if (user == null || user.Deleted)
                    return RedirectToAction(nameof(List));

                if (user != null)
                {
                    model = _mapper.Map<MemberPasswordModel>(user);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ویرایش رمز عبور(Post)")]
        public async Task<IActionResult> ChangePassword(MemberPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                Member? user = await _userManager.FindByIdAsync(model.Id.ToString());

                if (user != null)
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        var s = await _userManager.AddPasswordAsync(user, model.Password);
                        return RedirectToAction(nameof(List));
                    }
                }
            }
            return View(model);
        }
    }
}
