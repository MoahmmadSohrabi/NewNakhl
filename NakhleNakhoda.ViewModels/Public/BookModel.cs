using NakhleNakhoda.ViewModels.Catalog;
using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Public
{
    public class BookModel
    {
        [Display(Name = "نام")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Name { get; set; } = "";
        
        [Display(Name = "ایمیل")]
        [DataType(DataType.EmailAddress, ErrorMessage = "آدرس ایمیل معتبر وارد کنید.")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string Email { get; set; } = "";
        
        [Display(Name = "از تاریخ")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string FromDate { get; set; } = "";
        public DateTime FromDateUtc { get; set; }

        [Display(Name = "تا تاریخ")]
        [Required(ErrorMessage = "{0} مورد نیاز است.")]
        public string ToDate { get; set; } = "";
        public DateTime ToDateUtc { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "{0} مورد نیاز است.")]
        [Display(Name = "بزرگسال")]
        public int AdultQty { get; set; } = 1;
        public int ChildQty { get; set; } = 0;
        public int BabyQty { get; set; } = 0;

        //[Required(ErrorMessage = "{0} تعداد را انتخاب کنید.")]
        public int GuestQty => AdultQty + ChildQty + BabyQty;
        public int RoomCount => RoomCategories.Select(x => x.Qty).Sum();
        public List<RoomCategoryModel> RoomCategories { get; set; } = new List<RoomCategoryModel>();
        public List<RoomFacilityModel> RoomFacilities { get; set; } = new List<RoomFacilityModel>();
    }
}
