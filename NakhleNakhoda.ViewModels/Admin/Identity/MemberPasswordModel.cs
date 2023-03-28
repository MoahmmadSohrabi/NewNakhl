using System.ComponentModel.DataAnnotations;


namespace NakhleNakhoda.ViewModels.Admin.Identity
{
    public class MemberPasswordModel : BaseEntityModel
    {
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف داشته باشد.", MinimumLength = 6)]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "تکرار رمز عبور یکی نیست")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
