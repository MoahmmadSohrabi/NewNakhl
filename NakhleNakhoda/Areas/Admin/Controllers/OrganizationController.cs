//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.ComponentModel;
//using NakhleNakhoda.Common.Security;
//using NakhleNakhoda.Domain.Catalog;
//using NakhleNakhoda.Services.Catalog;
//using NakhleNakhoda.ViewModels.Admin.Catalog;
//using NakhleNakhoda.ViewModels;
//using NakhleNakhoda.Web.Areas.Admin.Factories;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace NakhleNakhoda.Web.Areas.Admin.Controllers
//{
//    [Authorize(Policy = PolicieNames.DynamicPermission)]
//    [DisplayName("سازمان")]
//    public class OrganizationController : BaseAdminController
//    {
//        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
//        private readonly IOrganizationService _organizationService;
//        private readonly IMapper _mapper;

//        public OrganizationController(
//            IBaseAdminModelFactory baseAdminModelFactory,
//            IOrganizationService organizationService,
//            IMapper mapper
//            )
//        {
//            _baseAdminModelFactory = baseAdminModelFactory;
//            _organizationService = organizationService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        [DisplayName("لیست")]
//        public virtual async Task<IActionResult> List()
//        {
//            var model = await _organizationService.GetOrganizations();

//            return View(model);
//        }

//        [HttpGet]
//        [DisplayName("جدید(Get)")]
//        public async Task<IActionResult> Create()
//        {
//            var model = new OrganizationModel();
//            model.Organizations = await _organizationService.GetOrganizationsGraph();
//            //await _baseAdminModelFactory.PrepareOrganization(model.AvailableOrganization, true, "بدون دسته پدر");
//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [DisplayName("جدید(Post)")]
//        public async Task<IActionResult> Create(OrganizationModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                model.Description ??= "";
//                var organization = _mapper.Map<Organization>(model);
//                await _organizationService.InsertAsync(organization);
//                return RedirectToAction(nameof(List));
//            }
//            return View(model);
//        }

//        [HttpGet]
//        [DisplayName("ویرایش(Get)")]
//        public async Task<IActionResult> Edit(long id)
//        {
//            Organization? organization = await _organizationService.GetByIdAsync(id);
//            var model = _mapper.Map<OrganizationModel>(organization);
//            model.Organizations = await _organizationService.GetOrganizationsGraph();
//            //await _baseAdminModelFactory.PrepareOrganization(model.AvailableOrganization, true, "بدون دسته پدر");

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [DisplayName("ویرایش(Post)")]
//        public async Task<IActionResult> Edit(OrganizationModel model)
//        {
//            if (model.ParentId == 0) model.ParentId = null;
//            if (model.ParentId == model.Id)
//            {
//                ModelState.AddModelError(string.Empty, "نمیتوانید سازمان بالایی را خود سازمان انتخاب کنید");
//            }

//            model.Organizations = await _organizationService.GetOrganizationsGraph();
//            if (ModelState.IsValid)
//            {
//                Organization? category = await _organizationService.GetByIdAsync(model.Id);
//                if (category == null)
//                {
//                    return View(model);
//                }
//                else if (category != null)
//                {
//                    category = _mapper.Map(model, category);
//                    await _organizationService.UpdateAsync(category!);
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
//                var model = await _organizationService.GetByIdAsync(id);
//                if (model == null)
//                    return Ok(new ResponseMessage { IsError = true, Message = "متاسفانه خطایی رخ داده است" });
//                await _organizationService.DeleteOrganization(model);
//                return Ok(new ResponseMessage { Message = "حذف با موفقیت انجام شد" });
//            }
//            catch (Exception e)
//            {
//                return Ok(new ResponseMessage { IsError = true, Message = e.Message });
//            }
//        }
//    }
//}
