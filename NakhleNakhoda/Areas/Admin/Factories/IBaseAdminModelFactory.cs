using Microsoft.AspNetCore.Mvc.Rendering;
using NakhleNakhoda.ViewModels.Catalog;

namespace NakhleNakhoda.Web.Areas.Admin.Factories
{
    /// <summary>
    /// Represents the base model factory that implements a most common admin model factories methods
    /// </summary>
    public partial interface IBaseAdminModelFactory
    {
        /// <summary>
        /// Prepare available roles
        /// </summary>
        /// <param name="items">Role items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        Task PrepareRoles(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string? defaultItemText = null);
        IList<SelectListItem> PrepareRoom(IList<RoomModel> rooms, List<long> selectedIds, bool withSpecialDefaultItem, string defaultItemText);

        /// <summary>
        /// Prepare available vendors
        /// </summary>
        /// <param name="items">vendor items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        //Task PrepareOrganization(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string? defaultItemText = null);

        /// <summary>
        /// Prepare available categories
        /// </summary>
        /// <param name="items">category items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use default value of the default item text</param>
        //Task PrepareTaskCategories(IList<SelectListItem> items, bool withSpecialDefaultItem = true, string? defaultItemText = null);
    }
}