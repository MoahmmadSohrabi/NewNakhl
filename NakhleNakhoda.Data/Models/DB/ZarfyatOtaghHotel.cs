using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class ZarfyatOtaghHotel
    {
        [Key]
        public int ZarfyatOtagh_ID { get; set; }


        [Display(Name = "ظرفیت اتاق")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string ZarfyatOtagh_Name { get; set; }

        public virtual ICollection<JozeyatHotel> Jozeyats { get; set; }
    }
}
