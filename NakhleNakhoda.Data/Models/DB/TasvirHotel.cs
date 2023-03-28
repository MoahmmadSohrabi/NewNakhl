using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class TasvirHotel
    {
        [Key]
        public int Tasavir_ID { get; set; }

        [Display(Name = " نام تصویر")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Tasavir_Name { get; set; }

        public int Tasavir_IDHotel { get; set; }//کلید خارجی

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.RequierdMsg)]
        [Display(Name = " تصویر پیشفرض")]
        public bool Tasavir_Asli { get; set; }



        [ForeignKey(nameof(Tasavir_IDHotel))]
        public virtual Hotels Hoteltasvir { get; set; }
    }
}
