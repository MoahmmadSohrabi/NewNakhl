using Microsoft.AspNetCore.Mvc.Rendering;
using NakhleNakhoda.Common;
using NakhleNakhoda.Services.Identity;
using NakhleNakhoda.ViewModels.Catalog;

namespace NakhleNakhoda.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the implementation of the base model factory that implements a most common admin model factories methods
    /// </summary>
    public partial class BaseAdminModelFactory : IBaseAdminModelFactory
    {

        #region Fields

        private readonly IRoleService _roleService;

        #endregion

        #region Ctor

        public BaseAdminModelFactory(
            IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Prepare default item
        /// </summary>
        /// <param name="items">Available items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use "All" text</param>
        protected virtual void PrepareDefaultItem(IList<SelectListItem> items, bool withSpecialDefaultItem, string? defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if (!withSpecialDefaultItem)
                return;

            //at now we use "0" as the default value
            const string value = "0";

            //prepare item text
            defaultItemText ??= Constants.NoneItemText;

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = value });
        }

        /// <summary>
        /// Get role list
        /// </summary>
        /// <param name="showPublished">A value indicating whether to show hidden records</param>
        /// <returns>Brand list</returns>
        protected virtual async Task<List<SelectListItem>> GetRoleList(bool showPublished = true)
        {
            var list = await _roleService.GetRoles();
            var listItems = list.Select(c => new SelectListItem
            {
                Text = c.Description,
                Value = c.Id.ToString()
            });

            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in listItems)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }

        /// <summary>
        /// Get vendor list
        /// </summary>
        /// <param name="showPublished">A value indicating whether to show hidden records</param>
        /// <returns>vendor list</returns>
        //protected virtual async Task<List<SelectListItem>> GetOrganizationList()
        //{
        //    return await _organizationService.GetOrganizationsSelectListItem();
        //}

        /// <summary>
        /// Get category list
        /// </summary>
        /// <param name="showPublished">A value indicating whether to show hidden records</param>
        /// <returns>Category list</returns>
        //protected virtual async Task<List<SelectListItem>> GetTaskCategoryList()
        //{
        //    var list = await _taskCategoryService.GetTaskCategories();
        //    return list.Select(c => new SelectListItem
        //    {
        //        Text = c.Title,
        //        Value = c.Id.ToString()
        //    }).ToList();
        //}

        #endregion

        #region Methods

        /// <summary>
        /// Prepare available roles
        /// </summary>
        /// <param name="items">Role items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual async Task PrepareRoles(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string? defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //prepare available categories
            var availableItems = await GetRoleList();
            foreach (var item in availableItems)
            {
                items.Add(item);
            }

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        }
        /// <summary>
        /// Prepare available Organization
        /// </summary>
        /// <param name="rooms">RoomModels items</param>
        /// <param name="selectedIds">selected Rooms</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        public virtual IList<SelectListItem> PrepareRoom(IList<RoomModel> rooms,
            List<long> selectedIds, bool withSpecialDefaultItem, string defaultItemText)
        {
            if (rooms == null)
                throw new ArgumentNullException(nameof(rooms));

            IList<SelectListItem> items = rooms.Select(x => new SelectListItem
            {
                Selected = selectedIds.Contains(x.Id),
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();

            //insert special item for the default value
            PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
            return items;
        }

        /// <summary>
        /// Prepare available TaskCategories
        /// </summary>
        /// <param name="items">Category items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        //public virtual async Task PrepareTaskCategories(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string? defaultItemText = null)
        //{
        //    if (items == null)
        //        throw new ArgumentNullException(nameof(items));

        //    //prepare available categories
        //    var availableItems = await GetTaskCategoryList();
        //    foreach (var item in availableItems)
        //    {
        //        items.Add(item);
        //    }

        //    //insert special item for the default value
        //    PrepareDefaultItem(items, withSpecialDefaultItem, defaultItemText);
        //}
        #endregion

    }
}