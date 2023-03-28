using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class RezervHotel
    {
        [Key]
        public int Rezerv_ID { get; set; }

        [Display(Name = "شناسه هتل")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Rezerv_IDHotel { get; set; }


        [Display(Name = "جنسیت رزرو کننده")]
        public int Rezerv_Jensiat { get; set; }


        [Display(Name = "نام ")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_Name { get; set; }


        [Display(Name = "نام خانوادگی ")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_NameKhanevadgi { get; set; }


        [Display(Name = "کدملی")]
        [MaxLength(10, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_Codemeli { get; set; }


        [Display(Name = "تلفن همراه ")]
        [MaxLength(11, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_Mobile { get; set; }


        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_Email { get; set; }


        [Display(Name = "تعداد نفرات")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Rezerv_TeadadNafarat { get; set; }

        [Display(Name = "تعداد اتاق")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Rezerv_TeadadOthagh { get; set; }

        [Display(Name = "تاریخ ورود")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public DateTime Rezerv_Vorod { get; set; }

        [Display(Name = "تاریخ خروج")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public DateTime Rezerv_Khoroj { get; set; }

        [Display(Name = "مبلغ پرداختی")]
        public long Rezerv_Mablagh { get; set; }


        [Display(Name = "وضعیت")]
        public bool Rezerv_Vazeyt { get; set; }

        [Display(Name = "تعداد روز اقامت")]
        public int Rezerv_Roz { get; set; }

        [Display(Name = "شناسه IP")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Rezerv_IP { get; set; }

        //[ForeignKey(nameof(Rezerv_IDHotel))]
        //public virtual Hotels HotelsHotel_ID { get; set; }

        public virtual ICollection<PardakhtHotel> PardakhtHotels { get; set; }
    }
}
