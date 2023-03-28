using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Services.Catalog;
using NakhleNakhoda.ViewModels.Public;

namespace NakhleNakhoda.Web.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(BookModel? model)
        {
            foreach (var modelValue in ModelState.Values)
            {
                modelValue.Errors.Clear();
            }
            model = await _bookService.InitBook(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BookModel model, string? returnUrl)
        {
            var response = await _bookService.AddBook(model);
            if (!response.IsSuccess)
            {
                foreach (var item in response.Content!)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
            }
            else
            {
                return RedirectToAction(nameof(Payment), new { Id = response.StatusCode });
            }

            model = await _bookService.InitBook(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Payment(long Id)
        {
            if (Id < 1)
            {
                return RedirectToAction(nameof(Index));
            }
            var model = await _bookService.GetPayment(Id);
            if (model == null)
            {
                RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(long Id, string? returnUrl)
        {
            if (Id < 1) return RedirectToAction(nameof(Index));
            var domainName = $"{Request.Scheme}://{Request.Host.Value}";
            var url = await _bookService.RequestPay(domainName, Id);

            if (url == null) return RedirectToAction(nameof(Index));

            return Redirect(url);
        }

        [HttpGet]
        public async Task<IActionResult> Validate(long Id)
        {
            if (Id < 1) return RedirectToAction(nameof(Index));

            var model = await _bookService.ValidatePay(Id);
            if (model == null) return RedirectToAction(nameof(Index));

            return View(model);
        }
    }
}
