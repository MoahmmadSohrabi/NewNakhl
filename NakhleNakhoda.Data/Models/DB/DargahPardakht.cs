using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class DargahPardakht
    {
        [Key]
        public int DargahPardakht_ID { get; set; }

        [Display(Name = "نام بانک")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string DargahPardakht_NameBank { get; set; }

        [Display(Name = "کد فعالسازی درگاه")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string DargahPardakht_Code { get; set; }
    }
}
