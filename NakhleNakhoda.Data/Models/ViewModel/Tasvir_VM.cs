using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.ViewModel
{
    public class Tasvir_VM
    {
        [Key]
        public int Tasavir_ID { get; set; }

        [Display(Name = "  تصویر")]
//[Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        public IFormFile Tasavir { get; set; }

        public int Tasavir_IDHotel { get; set; }//کلید خارجی

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [Display(Name = " تصویر پیشفرض")]
        public bool Tasavir_Asli { get; set; }

    }
}
