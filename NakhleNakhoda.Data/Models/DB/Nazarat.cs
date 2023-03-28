using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class Nazarat
    {
        [Key]
        public int Nazarat_ID { get; set; }

        [Display(Name = "فرستنده")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Nazarat_Name { get; set; }

        [Display(Name = "متن نظر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(200, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Nazarat_Matn { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Nazarat_Zaman { get; set; }

        [Display(Name = "شناسه هتل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Nazarat_HotelID { get; set; }

        [Display(Name = "ایمیل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Nazarat_Email { get; set; }

        [Display(Name = "امتیاز")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Nazarat_Emtyaz { get; set; }

        [ForeignKey(nameof(Nazarat_HotelID))]
        public virtual Hotels HotelNazar { get; set; }
    }
}
