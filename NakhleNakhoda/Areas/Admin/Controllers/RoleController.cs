using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Data.Core.Mvc;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Services.Identity;
using NakhleNakhoda.Services.Security;
using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = PolicieNames.DynamicPermission)]
    [DisplayName("نقش کاربران")]
    public class RoleController : BaseAdminController
    {
        private readonly IRoleService _roleService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;


        public RoleController(IRoleService roleService, RoleManager<Role> roleManager,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            _roleService = roleService;
            _roleManager = roleManager;
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
        }

        [HttpGet]
        [DisplayName("لیست")]
        public virtual async Task<IActionResult> List()
        {
            var model = await _roleService.GetAllRole();

            return View(model);
        }

        [HttpGet]
        [DisplayName("جدید(Get)")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("جدید(Post)")]
        public async Task<IActionResult> Create(RoleModel model, bool continueEditing)
        {
            var role = new Role { Name = model.Name, NormalizedName = model.Name.ToUpper(), Description = model.Description };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                if (!continueEditing)
                    return RedirectToAction(nameof(List));

                return RedirectToAction(nameof(Edit), new { id = role.Id });
            }
            return View();
        }

        [HttpGet]
        [DisplayName("ویرایش(Get)")]
        public async Task<IActionResult> Edit(long id)
        {
            RoleModel model = new RoleModel();
            Role? role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                model.Name = role.Name!;
                model.Description = role.Description;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ویرایش(Post)")]
        public async Task<IActionResult> Edit(long id, RoleModel model, bool continueEditing)
        {
            Role? role = await _roleManager.FindByIdAsync(id.ToString());
            if (role != null)
            {
                role.Name = model.Name;
                role.Description = model.Description;
                IdentityResult result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    if (!continueEditing)
                        return RedirectToAction(nameof(List));

                    return RedirectToAction(nameof(Edit), new { id = role.Id });
                }
            }
            return View(model);
        }


        [HttpGet]
        [DisplayName("تعیین سطح دسترسی(Get)")]
        public async Task<IActionResult> RoleClaimsManager(long? id)
        {
            if (!id.HasValue)
            {
                return View("Error");
            }

            var role = await _roleService.FindRoleIncludeRoleClaimsAsync(id.Value);
            if (role == null)
            {
                return View("NotFound");
            }

            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(PolicieNames.DynamicPermission);
            return View(model: new DynamicRoleClaimsManagerViewModel
            {
                SecuredControllerActions = securedControllerActions,
                RoleIncludeRoleClaims = role
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [DisplayName("تعیین سطح دسترسی(Post)")]
        public async Task<IActionResult> RoleClaimsManager(long id, DynamicRoleClaimsManagerViewModel model, bool continueEditing)
        {
            await _roleService.AddOrUpdateRoleClaimsAsync(
                 roleId: id,
                 roleClaimType: PolicieNames.DynamicPermissionClaimType,
                 selectedRoleClaimValues: model.ActionIds);


            if (!continueEditing)
                return RedirectToAction(nameof(List));

            return RedirectToAction(nameof(RoleClaimsManager), new { id = id });
        }
    }
}