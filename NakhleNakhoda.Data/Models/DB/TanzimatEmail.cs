using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.DB
{
    public class TanzimatEmail
    {
        [Key]
        public int Eamil_ID { get; set; }

        [Display(Name = " ایمیل ارسال")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string Eamil_EmailSend { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]
        public string Eamil_Password { get; set; }


        [Display(Name = "نام کاربری")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string Eamil_UserName { get; set; }

        [Display(Name = "پورت")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string Eamil_Port { get; set; }

        [Display(Name = "Smtp")]
        [MaxLength(100, ErrorMessage = ErrorMessage.MaxLenghtMsg)]

        public string Eamil_Smtp { get; set; }
    }
}
