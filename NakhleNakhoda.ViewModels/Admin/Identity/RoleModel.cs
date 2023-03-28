using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Admin.Identity
{
    public class RoleModel : BaseEntityModel
    {
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "نام (لاتین)")]
        [RegularExpression(@"^.*[a-zA-Z0-9]$", ErrorMessage = "فقط حروف لاتین مجاز است.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "توضیح نقش (فارسی)")]
        public string Description { get; set; } = "";
    }
}
