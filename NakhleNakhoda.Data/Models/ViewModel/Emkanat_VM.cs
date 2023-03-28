using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.ViewModel
{
    public class Emkanat_VM
    {
        [Key]
        public int Emkanat_ID { get; set; }

        [Display(Name = "نام")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Emkanat_Name { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Emkanat_Icon { get; set; }

        public int Emkanat_IdHotel { get; set; }
    }
}
