using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.ViewModel
{
    public class Jostejo
    {
        [Key]
        public int id { get; set; }
        public int HotelID { get; set; }
        public string NameHotel { get; set; }
        public long GheymatYekShab { get; set; }
        public string imgUrl { get; set; }
        public string Stareh { get; set; }
    }
}
