using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.Common
{
    public enum Gender : int
    {
        [Display(Name = "مرد")]
        Man = 1,
        [Display(Name = "زن")]
        Female = 2,
    }
}
