using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Mvc.Rendering;
using NakhleNakhoda.Common;
using NakhleNakhoda.Common.Extensions;
using NakhleNakhoda.Domain.Identity;
using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Identity
{
    public class UserModel
    {
        public static UserModel FromCatalog(Member model)
        {
            return new UserModel
            {
                Id = model.Id,
                Active = model.Active,
                CreatedOnUtc = model.CreatedOnUtc,
                DisplayName = model.DisplayName,
                Email = model.Email ?? "",
                FirstName = model.FirstName,
                LastName = model.LastName,
                LastLoginDateUtc = model.LastLoginDateUtc,
                PhoneNumber = model.PhoneNumber ?? "",
                PictureId = model.PictureId,
                UpdatedOnUtc = model.UpdatedOnUtc,
                UserName = model.UserName ?? "",
                AccountNumber = model.AccountNumber,
                BirthDate = model.BirthDate,
                BirthDateName = new PersianDateTime(Convert.ToDateTime(model.BirthDate)).ToString("yyyy/MM/dd"),
                BirthYear = int.Parse(new PersianDateTime(Convert.ToDateTime(model.BirthDate)).ToString("yyyy")),
                BirthMonth = int.Parse(new PersianDateTime(Convert.ToDateTime(model.BirthDate)).ToString("MM")),
                BirthDay = int.Parse(new PersianDateTime(Convert.ToDateTime(model.BirthDate)).ToString("dd")),
                CardNumber = model.CardNumber,
                GenderId = model.GenderId,
                Gender = model.Gender.GetDisplayName(),
                AvailableGender = EnumExtensions.GetValuesAsSelectListItem<Gender>(),
                RoleNames = "",
                ShebaNumber = string.IsNullOrEmpty(model.ShebaNumber) ? "IR" : model.ShebaNumber,
                SocialNumber = model.SocialNumber
            };
        }
        public long Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "نام")]
        //[Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string FirstName { get; set; } = "";

        [DataType(DataType.Text)]
        [Display(Name = "نام خانوادگی")]
        //[Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string LastName { get; set; } = "";

        [Display(Name = "نام")]
        public string DisplayName { get; set; } = "";

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
        public long PictureId { get; set; }

        [Display(Name = "نقش های کاربر")]
        public string RoleNames { get; set; } = "";

        [Display(Name = "وضعیت کاربر")]
        public bool Active { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [Phone(ErrorMessage = "شماره همراه معتبر نیست.")]
        [RegularExpression(@"^([0]{1}[9]{1}[0-9]{9})$", ErrorMessage = "شماره همراه معتبر نیست.")]
        [StringLength(11, ErrorMessage = "شماره همراه معتبر نیست.")]
        public string PhoneNumber { get; set; } = "";

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime? CreatedOnUtc { get; set; }

        [Display(Name = "تاریخ ویرایش")]
        public DateTime UpdatedOnUtc { get; set; }

        [Display(Name = "تاریخ آخرید ورود")]
        public DateTime? LastLoginDateUtc { get; set; }

        [Display(Name = "جنسیت")]
        public int GenderId { get; set; }

        [Display(Name = "جنسیت")]
        public string Gender { get; set; } = "";
        public IList<SelectListItem> AvailableGender { get; set; } = new List<SelectListItem>();

        [Display(Name = "شماره کارت")]
        //[StringLength(19, ErrorMessage = "شماره کارت صحیح نیست.")]
        //[Required, StringLength(6, MinimumLength = 0, ErrorMessage = "شماره کارت صحیح نیست.")]
        public string CardNumber { get; set; } = "";

        [Display(Name = "شماره حساب")]
        public string AccountNumber { get; set; } = "";

        [Display(Name = "شماره شبا")]
        [StringLength(30, ErrorMessage = "شماره شبا صحیح نیست.")]
        public string ShebaNumber { get; set; } = "";
        public string ShebaCode { get; set; } = "";
        [Display(Name = "تاریخ تولد")]
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public int BirthDay { get; set; }
        public string BirthDateName { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        [Display(Name = "کد ملی")]
        public string SocialNumber { get; set; } = "";
    }
}
