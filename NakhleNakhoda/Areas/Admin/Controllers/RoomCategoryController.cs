using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Common.Security;
using NakhleNakhoda.Services.Catalog;
using NakhleNakhoda.ViewModels.Catalog;
using NakhleNakhoda.ViewModels.Public;
using NakhleNakhoda.Web.Areas.Admin.Factories;
using System.ComponentModel;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [Authorize(Policy = PolicieNames.DynamicPermission)]
    [DisplayName("نقش کاربران")]
    public class RoomCategoryController : BaseAdminController
    {
        private readonly IRoomCategoryService _roomCategoryService;
        private readonly IRoomService _roomService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;

        public RoomCategoryController(
            IRoomCategoryService roomCategoryService,
            IRoomService roomService,
            IBaseAdminModelFactory baseAdminModelFactory)
        {
            _roomCategoryService = roomCategoryService;
            _roomService = roomService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }

        [HttpGet]
        [DisplayName("لیست")]
        public virtual async Task<IActionResult> List()
        {
            var model = (await _roomCategoryService.GetAllByEnumerableAsync())
                .Select(x => RoomCategoryModel.FromCatalog(x)).ToList();
            return View(model);
        }

        [HttpGet]
        [DisplayName("جدید(Get)")]
        public IActionResult Create()
        {
            var model = new RoomCategoryModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("جدید(Post)")]
        public async Task<IActionResult> Create(RoomCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = model.ToCatalog();
                await _roomCategoryService.InsertAsync(category);
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        [HttpGet]
        [DisplayName("ویرایش(Get)")]
        public async Task<IActionResult> Edit(long id)
        {
            var category = await _roomCategoryService.GetByIdAsync(id);

            if (category == null) return RedirectToAction(nameof(List));

            RoomCategoryModel model = RoomCategoryModel.FromCatalog(category);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("ویرایش(Post)")]
        public async Task<IActionResult> Edit(RoomCategoryModel model)
        {

            if (ModelState.IsValid)
            {
                var category = await _roomCategoryService.GetByIdAsync(model.Id);

                if (category == null) return RedirectToAction(nameof(List));

                category = model.ToCatalog();
                await _roomCategoryService.UpdateAsync(category!);

                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisplayName("حذف(Post)")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var model = await _roomCategoryService.GetByIdAsync(id);
                if (model == null)
                    return Ok(new ResponseModel<dynamic> { IsSuccess = false, Message = "متاسفانه خطایی رخ داده است" });

                await _roomCategoryService.DeleteAsync(model);

                return Ok(new ResponseModel<dynamic> { Message = "حذف با موفقیت انجام شد" });
            }
            catch (Exception e)
            {
                return Ok(new ResponseModel<dynamic> { IsSuccess = false, Message = e.Message });
            }
        }
    }
}
