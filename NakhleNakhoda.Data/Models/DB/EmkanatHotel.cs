using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class EmkanatHotel
    {
        [Key]
        public int Emkanat_ID { get; set; }


        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Emkanat_Name { get; set; }

        [Display(Name = "تصویر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Emkanat_Icon { get; set; }

        public int Emkanat_IdHotel { get; set; }

        [ForeignKey(nameof(Emkanat_IdHotel))]
        public virtual Hotels HotelEmkanat { get; set; }
    }
}
