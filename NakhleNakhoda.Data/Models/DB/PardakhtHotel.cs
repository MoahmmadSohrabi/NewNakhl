using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class PardakhtHotel
    {
        [Key]
        public int Pardakh_ID { get; set; }

        [Display(Name = "شناسه اتاق")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Pardakh_IDHotel { get; set; }

        [Display(Name = "تاریخ پرداخت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public DateTime Pardakh_ZamanVariz { get; set; }

        [MaxLength(30, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        [Display(Name = "شماره پیگیری")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public string Pardakh_Pigiry { get; set; }

        [Display(Name = "مبلغ پرداخت")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public long Pardakh_Mablagh { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public bool Pardakh_Vazeiat { get; set; }

        [MaxLength(50, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        [Display(Name = "شماره تراکنش")]
        public string Pardakh_Trakonesh { get; set; }

        [MaxLength(50, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        [Display(Name = "شماره مرجع بانکی")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public string Pardakh_Marjaa { get; set; }

        [Display(Name = "شماره رزرو")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public int Pardakh_Rezerv { get; set; }


        [ForeignKey(nameof(Pardakh_Rezerv))]
        public virtual RezervHotel RezervHotel { get; set; }

        [ForeignKey(nameof(Pardakh_IDHotel))]
        public virtual Hotels HotelPardakht { get; set; }
    }
}
