using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class Hotels
    {
        [Key]
        public int Hotel_ID { get; set; }

        [Display(Name = "نام اتاق")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Name_Hotel { get; set; }

        [Display(Name = "قیمت هر شب")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public long Jozeyat_Gheymat { get; set; }

        [Display(Name = "تاریخ شروع دسترسی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public DateTime ZamanShoroa { get; set; }

        [Display(Name = "تاریخ پایان دسترسی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public DateTime ZamanPayan { get; set; }

        [Display(Name = "دسترسی فعال؟")]
        public bool Faal { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public string Tozihat { get; set; }

        /*-----------------------------------------------------*/

        public virtual ICollection<JozeyatHotel> JozeyatHotels { get; set; }
        public virtual ICollection<Nazarat> Nazarats { get; set; }
        public virtual ICollection<PardakhtHotel> PardakhtHotels { get; set; }
        //public virtual ICollection<RezervHotel> RezervHotels { get; set; }
        public virtual ICollection<TasvirHotel> TasavirHotels { get; set; }
        public virtual ICollection<EmkanatHotel> EmkanatHotels { get; set; }
    }
}
