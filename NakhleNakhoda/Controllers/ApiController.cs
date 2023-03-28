using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.Factories;
using NakhleNakhoda.ViewModels.Identity;

namespace NakhleNakhoda.Controllers
{

    [Route("api/v1/[action]")]
    public class ApiController : Controller
    {

        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;
        private readonly ILogger<ApiController> _logger;
        private readonly IBaseModelFactory _baseModelFactory;
        public ApiController(
            SignInManager<Member> signInManager,
            UserManager<Member> userManager,
            ILogger<ApiController> logger,
            IBaseModelFactory baseModelFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _baseModelFactory = baseModelFactory;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendVerifyingCodeSms(LoginModel model)
        {

            if (_baseModelFactory.VarifyingPhoneNumber(model.PhoneNumber))
            {
                var user = await _userManager.FindByNameAsync(model.PhoneNumber);
                if (user == null) return Ok();

                var password = _baseModelFactory.GenerateVerifyingCode();

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, password);

                // send verifying code with sms
                _ = _baseModelFactory.SendSmsMessage(model.PhoneNumber, password);
            }
            return Ok();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> VerifyingCode(LoginOtpModel model)
        //{
        //    return Ok();
        //}
    }
}
