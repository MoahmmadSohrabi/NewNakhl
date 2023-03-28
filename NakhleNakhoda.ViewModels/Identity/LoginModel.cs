using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Identity
{
    public class LoginModel
    {
        [Required(ErrorMessage = "شماره موبایل الزامیست.")]
        [Phone(ErrorMessage = "شماره موبایل معتبر نیست.")]
        [RegularExpression(@"^([0]{1}[9]{1}[0-9]{9})|([0]{0}[9]{1}[0-9]{9})$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        [StringLength(11, ErrorMessage = "شماره موبایل معتبر نیست.")]
        [Display(Name = "شماره موبایل خود را وارد کنید")]
        public string PhoneNumber { get; set; } = "";

        //[Required(ErrorMessage = "کد تایید الزامیست")]
        //[Phone]
        //[Display(Name = "تائید شماره موبایل")]
        //public string? OtpNumber { get; set; } = "";
    }

    public class LoginOtpModel
    {
        public LoginOtpModel(string phoneNumber, string otpNumber)
        {
            PhoneNumber = phoneNumber;
            OtpNumber = otpNumber;
        }

        [Required(ErrorMessage = "شماره موبایل الزامیست.")]
        [Phone(ErrorMessage = "شماره موبایل معتبر نیست.")]
        [RegularExpression(@"^([0]{1}[9]{1}[0-9]{9})|([0]{0}[9]{1}[0-9]{9})$", ErrorMessage = "شماره موبایل معتبر نیست.")]
        [StringLength(11, ErrorMessage = "شماره موبایل معتبر نیست.")]
        [Display(Name = "شماره موبایل خود را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "کد تایید الزامیست")]
        [Phone]
        [Display(Name = "تائید شماره موبایل")]
        public string OtpNumber { get; set; }
    }
}
