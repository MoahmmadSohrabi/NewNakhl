using Microsoft.AspNetCore.Mvc.Rendering;
using NakhleNakhoda.ViewModels.Admin.Media;
using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Admin.Identity
{
    public class MemberModel : BaseEntityModel
    {

        [DataType(DataType.Text)]
        [Display(Name = "نام")]
        //[Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string FirstName { get; set; } = "";

        [DataType(DataType.Text)]
        [Display(Name = "نام خانوادگی")]
        //[Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string LastName { get; set; } = "";

        [Display(Name = "نام")]
        public string? DisplayName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "نام کاربری")]
        [RegularExpression(@"^.*[a-zA-Z0-9]$", ErrorMessage = "فقط حروف لاتین مجاز است.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string UserName { get; set; } = "";

        [DataType(DataType.EmailAddress, ErrorMessage = "آدرس ایمیل معتبر وارد کنید.")]
        [Display(Name = "ایمیل")]
        //[Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Email { get; set; } = "";

        [UIHint("Picture")]
        [Display(Name = "عکس پروفایل")]
        public long? PictureId { get; set; }

        [Display(Name = "عکس پروفایل")]
        public PictureModel? PictureModel { get; set; } = new PictureModel();

        [Display(Name = "نقش های کاربر")]
        public string? RoleNames { get; set; }

        [Display(Name = "وضعیت کاربر")]
        public bool Active { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Phone(ErrorMessage = "شماره همراه معتبر نیست.")]
        [RegularExpression(@"^([0]{1}[9]{1}[0-9]{9})$", ErrorMessage = "شماره همراه معتبر نیست.")]
        [StringLength(11, ErrorMessage = "شماره همراه معتبر نیست.")]
        public string PhoneNumber { get; set; } = "";
        [Display(Name = "تایید ایمیل")]
        public bool EmailConfirmed { get; set; } = true;

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime? CreatedOnUtc { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime UpdatedOnUtc { get; set; }

        [Display(Name = "تاریخ آخرین ورود")]
        public DateTime? LastLoginDateUtc { get; set; }

        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public IList<long> SelectedRoleIds { get; set; } = new List<long>();

        public IList<SelectListItem> AvailableRoles { get; set; } = new List<SelectListItem>();

        [Display(Name = "سازمان")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public IList<long> SelectedOrganizationIds { get; set; } = new List<long>();
        public IList<SelectListItem> AvailableOrganization { get; set; } = new List<SelectListItem>();
    }
}
