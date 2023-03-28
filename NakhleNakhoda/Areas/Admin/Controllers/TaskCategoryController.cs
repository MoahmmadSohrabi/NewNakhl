//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel;
//using NakhleNakhoda.Common.Security;
//using NakhleNakhoda.ViewModels.Admin.Catalog;

//namespace NakhleNakhoda.Web.Areas.Admin.Controllers
//{
//    [Authorize(Policy = PolicieNames.DynamicPermission)]
//    [DisplayName("دسته بندی")]
//    public class TaskCategoryController : BaseAdminController
//    {
//        private readonly ITaskCategoryService _taskCategoryService;
//        private readonly IMapper _mapper;

//        public TaskCategoryController(
//            ITaskCategoryService taskCategoryService,
//            IMapper mapper
//            )
//        {
//            _taskCategoryService = taskCategoryService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [DisplayName("لیست")]
//        public virtual async Task<IActionResult> List()
//        {
//            var model = await _taskCategoryService.GetTaskCategories();

//            return View(model);
//        }

//        [HttpGet]
//        [DisplayName("جدید(Get)")]
//        public IActionResult Create()
//        {
//            var model = new TaskCategoryModel();
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [DisplayName("جدید(Post)")]
//        public async Task<IActionResult> Create(TaskCategoryModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                model.Description ??= "";
//                model.Structure ??= "";
//                var taskCategory = _mapper.Map<TaskCategory>(model);
//                await _taskCategoryService.InsertAsync(taskCategory);
//                return RedirectToAction(nameof(List));
//            }
//            return View(model);
//        }

//        [HttpGet]
//        [DisplayName("ویرایش(Get)")]
//        public async Task<IActionResult> Edit(long id)
//        {
//            TaskCategory? category = await _taskCategoryService.GetByIdAsync(id);
//            var model = _mapper.Map<TaskCategoryModel>(category);

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [DisplayName("ویرایش(Post)")]
//        public async Task<IActionResult> Edit(TaskCategoryModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                TaskCategory? category = await _taskCategoryService.GetByIdAsync(model.Id);
//                if (category == null)
//                {
//                    return View(model);
//                }
//                else if (category != null)
//                {
//                    category = _mapper.Map(model, category);
//                    await _taskCategoryService.UpdateAsync(category!);
//                }

//                return RedirectToAction(nameof(List));
//            }

//            return View(model);
//        }

//        // POST: Admin/Category/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [DisplayName("حذف(Post)")]
//        public async Task<IActionResult> Delete(long id)
//        {
//            try
//            {
//                var model = await _taskCategoryService.GetByIdAsync(id);
//                if (model == null)
//                    return Ok(new ResponseMessage { IsError = true, Message = "متاسفانه خطایی رخ داده است" });
//                await _taskCategoryService.DeleteTaskCategory(model);
//                return Ok(new ResponseMessage { Message = "حذف با موفقیت انجام شد" });
//            }
//            catch (Exception e)
//            {
//                return Ok(new ResponseMessage { IsError = true, Message = e.Message });
//            }
//        }

//    }
//}
