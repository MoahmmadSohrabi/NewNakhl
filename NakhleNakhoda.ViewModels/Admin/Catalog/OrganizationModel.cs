using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Admin.Catalog
{
    public class OrganizationModel : BaseEntityModel
    {
        [Display(Name = "نام سازمان")]
        public string Name { get; set; } = "";

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "اتمام پروژه")]
        public bool AccessToFinish { get; set; }

        [Display(Name = "سازمان بالایی")]
        public long? ParentId { get; set; }

        [Display(Name = "سازمان بالایی")]
        public OrganizationModel? Parent { get; set; }

        [Display(Name = "ایجاد شده در")]
        public DateTime? CreatedOnUtc { get; set; }

        [Display(Name = "بروز شده در")]
        public DateTime? UpdatedOnUtc { get; set; }
        public List<OrganizationGraphModel> Organizations { get; set; } = new List<OrganizationGraphModel>();
        //public IList<SelectListItem> AvailableOrganization { get; set; } = new List<SelectListItem>();
    }
}
