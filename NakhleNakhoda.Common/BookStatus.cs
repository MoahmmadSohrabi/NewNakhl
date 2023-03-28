using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.Common
{
    public enum BookStatus : int
    {
        [Display(Name = "لغو شده")]
        Cancel = -1,
        [Display(Name = "ثبت شده")]
        Submit = 0,
        [Display(Name = "رزرو شده")]
        Success = 1,
    }
}