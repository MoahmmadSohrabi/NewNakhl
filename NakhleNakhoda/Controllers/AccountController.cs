using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Common;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Data.Models;
using NakhleNakhoda.Data.Models.DB.User;
using NakhleNakhoda.Data.Models.ViewModel.User;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Factories;
using NakhleNakhoda.Services.Convertors;
using NakhleNakhoda.Services.Generator;
using NakhleNakhoda.Services.Install;
using NakhleNakhoda.Services.Security;
using NakhleNakhoda.Services.Services.Interfaces;
using NakhleNakhoda.ViewModels.Identity;
using System;
using System.Security.Claims;

namespace NakhleNakhoda.Controllers
{
    public class AccountController : Controller
    {
        private IUserServices _userService;
        private readonly IEmailSender _emailSender;
        private IViewRenderService _viewRender;
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IBaseModelFactory _baseModelFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppInitializationService _appInitialization;

        public AccountController(IUserServices userService, IEmailSender emailSender, IViewRenderService viewRender,
            SignInManager<Member> signInManager,
            UserManager<Member> userManager,
            ILogger<AccountController> logger,
            IBaseModelFactory baseModelFactory,
            IHttpContextAccessor httpContextAccessor,
            IAppInitializationService appInitialization)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _baseModelFactory = baseModelFactory;
            _httpContextAccessor = httpContextAccessor;
            _appInitialization = appInitialization;
            _userService = userService;
            _emailSender = emailSender;
            _viewRender = viewRender;
        }
        //public IList<AuthenticationScheme> ExternalLogins { get; set; }


        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }


            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }


            User user = new User()
            {
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "~/Defult.jpg",
                UserName = register.UserName
            };
            _userService.AddUser(user);

            #region Send Activation Email

            string body = _viewRender.RenderToStringAsync("_ActiveEmail", user);
            _emailSender.SendEmailAsync(user.Email, "فعالسازی", body);
            //SendEmail.SendEmailAsync(user.Email, "فعالسازی", body);

            #endregion

            return View("SuccessRegister", user);
        }
        #region Login
        [Route("Login")]
        public ActionResult Login2(bool EditProfile = false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login2(LoginViewModel login, string ReturnUrl = "/")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = login.RememberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;
                    if (ReturnUrl != "/")
                    {
                        return Redirect(ReturnUrl);
                    }
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد");
                }
            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(login);
        }

        #endregion

        #region Active Account

        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }

        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout2()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region Forgot Password
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
                return View(forgot);

            string fixedEmail = FixedText.FixEmail(forgot.Email);
            NakhleNakhoda.Data.Models.DB.User.User user = _userService.GetUserByEmail(fixedEmail);

            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgot);
            }

            string bodyEmail = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            //SendEmail.Send(user.Email, "بازیابی حساب کاربری", bodyEmail);
            _emailSender.SendEmailAsync(user.Email, "بازیابی حساب کاربری", bodyEmail);
            ViewBag.IsSuccess = true;

            return View();
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await _appInitialization.InitializeAsync();
            if (HttpContext.Request.Host.ToString().Contains("localhost"))
            {
                var user = await _userManager.FindByNameAsync("mahdi");
                await _signInManager.SignInAsync(user!, isPersistent: true);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                if (_baseModelFactory.VarifyingPhoneNumber(model.PhoneNumber))
                {
                    var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                    // if user not exist(sign up)
                    var password = _baseModelFactory.GenerateVerifyingCode();

                    if (user == null)
                    {

                        user = new Member
                        {
                            UserName = model.PhoneNumber,
                            PhoneNumber = model.PhoneNumber,
                            Otp = password,
                            OtpExpireAt = DateTime.UtcNow.AddHours(1),
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            LockoutEnd = DateTime.UtcNow.AddYears(-1),
                        };

                        var result = await _userManager.CreateAsync(user, password);

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        user.Otp = password;
                        user.OtpExpireAt = DateTime.UtcNow.AddHours(1);
                        await _userManager.UpdateAsync(user);
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, password);
                    }

                    // send verifying code with sms
                    _ = _baseModelFactory.SendSmsMessage(model.PhoneNumber, password);

                    //after send varifying code redirect to confirm page
                    return RedirectToAction(nameof(VerifyPhone), new { phone = model.PhoneNumber, returnUrl });
                }

                ModelState.AddModelError(string.Empty, "شماره موبایل معتبر نیست.");

            }
            return View();
        }
        public IActionResult Rooms()
        {
            return View();
        }
        [HttpGet]
        public IActionResult VerifyPhone(string phone, string? returnUrl)
        {
            LoginOtpModel model = new(phone, "");
            if (phone == null)
            {
                return RedirectToAction(nameof(Login));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPhone(string PhoneNumber, string OtpNumber, string? returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            LoginOtpModel model = new(PhoneNumber, OtpNumber);
            var httpContext = _httpContextAccessor?.HttpContext;

            if (ModelState.IsValid)
            {
                if (_baseModelFactory.VarifyingPhoneNumber(model.PhoneNumber))
                {
                    var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.OtpNumber, true, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                        if (user == null)
                        {
                            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                            return RedirectToAction(nameof(Login));
                        }

                        user.PhoneNumberConfirmed = true;
                        user.Active = true;
                        user.LastIpAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "";
                        user.LastLoginDateUtc = DateTime.UtcNow;
                        await _userManager.UpdateAsync(user);
                        await _userManager.AddToRoleAsync(user, RoleNames.Member);

                        //var roles = await _userManager.GetRolesAsync(user);
                        //foreach (var role in roles)
                        //{
                        //    await _userManager.AddToRoleAsync(user, role);
                        //}

                        // Clear the existing external cookie to ensure a clean login process
                        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "کد تایید نامعتبر است");

                        return View(model);
                    }
                }
            }

            return LocalRedirect(returnUrl);
        }

        [Authorize]
        public IActionResult Profile()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = UserModel.FromCatalog(_userManager.GetUserAsync(User).Result!);
                return View(user);
            }
            return RedirectToAction(nameof(Login));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProfileAsync(UserModel model)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result!;
                user.GenderId = model.GenderId;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.SocialNumber = model.SocialNumber;
                user.Email = model.Email;
                user.BirthDate = new PersianDateTime(model.BirthYear, model.BirthMonth, model.BirthDay).ToDateTime();
                user.CardNumber = model.CardNumber;
                user.AccountNumber = model.AccountNumber;
                user.ShebaNumber = "IR" + model.ShebaNumber;
                user.Gender = (Gender)model.GenderId;
                user.PictureId = model.PictureId;
                await _userManager.UpdateAsync(user);
                return View(UserModel.FromCatalog(user));
            }
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            return RedirectToAction("Index", "Home");
        }

    }
}
