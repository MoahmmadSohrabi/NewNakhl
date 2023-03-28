
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Dashboard;
using System.ComponentModel;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [DisplayName("داشبورد")]
    [Authorize/*(Policy = PolicieNames.DynamicPermission)*/]
    [Area("Admin")]
    public class DashboardController : BaseAdminController
    {
        private readonly UserManager<Member> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(
            UserManager<Member> userManager, RoleManager<Role> roleManager,
            SignInManager<Member> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;

        }

        [DisplayName("صفحه اصلی")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Update()
        {
            /*var order = await _orderService.GetAllByEnumerableAsync();
            var orderCount = order.Count();
            var newOrderCount = order.Where(x => x.OrderStatus.Equals(OrderStatus.Preparing)).Count();
            var model = new DashboardModel()
            {
                OrderCount = orderCount,
                NewOrderCount = newOrderCount,
                //OrderSentPercent = (newOrderCount * 100) / orderCount,
            };
            if (model == null)
                return Ok(new Response<DashboardModel>());
            return Ok(new Response<DashboardModel>() { Data = model });*/
            return View();
        }


        [HttpGet]
        [DisplayName("تغییر رمز عبور(Get)")]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"کاربری با این مشخصات پیدا نشد '{_userManager.GetUserId(User)}'.");
            }

            return View();
        }

        [HttpPost]
        [DisplayName("تغییر رمز عبور(Post)")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"کاربری با این مشخصات پیدا نشد '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    if (error.Code.Equals("PasswordMismatch"))
                        ModelState.AddModelError(string.Empty, "رمز عبور فعلی معتبر نمیباشد");
                }
                return View();
            }

            await _signInManager.RefreshSignInAsync(user);
            //StatusMessage = "Your password has been changed.";

            //return RedirectToPage();
            return RedirectToAction(nameof(Index));

        }
    }
}
