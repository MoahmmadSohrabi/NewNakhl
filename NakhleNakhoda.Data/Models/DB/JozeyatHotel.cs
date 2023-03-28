using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class JozeyatHotel
    {
        [Key]
        public int Jozeyat_ID { get; set; }

        public int Jozeyat_HotelID { get; set; }

        [Display(Name = "ظرفیت اتاق")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Jozeyat_ZarfiatOtaghID { get; set; }

        [Display(Name = "تعداد تخت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Jozeyat_TedadTakhtID { get; set; }

        [Display(Name = "تعداد ستاره")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Jozeyat_TedadStareID { get; set; }


        [Display(Name = "موجودی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Jozeyat_Teadad { get; set; }

        /*---------------INJA DAR GHESMAT 5 TAGHIR DADE SHOD------------------*/

        [ForeignKey(nameof(Jozeyat_ZarfiatOtaghID))]
        public virtual ZarfyatOtaghHotel ZarfyatOtagh { get; set; }

        //[ForeignKey(nameof(ZarfyatOtaghHotel))]
        //public virtual ZarfyatOtaghHotel ZarfyatOtagh { get; set; }


        [ForeignKey(nameof(Jozeyat_TedadStareID))]
        public virtual TedadStareh TedadStareh { get; set; }


        [ForeignKey(nameof(Jozeyat_TedadTakhtID))]
        public virtual TedadTakhtHotel TedadTakhtHotel { get; set; }


        [ForeignKey(nameof(Jozeyat_HotelID))]
        public virtual Hotels Hoteljoz { get; set; }
    }
}
