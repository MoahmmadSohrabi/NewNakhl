using System.ComponentModel.DataAnnotations;


namespace NakhleNakhoda.ViewModels.Admin.Dashboard
{
    public class ChangePasswordModel : BaseEntityModel
    {
        public ChangePasswordModel(string oldPassword, string newPassword, string confirmPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }

        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف داشته باشد.", MinimumLength = 4)]
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز عبور یکی نیست")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string ConfirmPassword { get; set; }
    }
}
