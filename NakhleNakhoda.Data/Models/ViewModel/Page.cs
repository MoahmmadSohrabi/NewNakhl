using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.ViewModel
{
    public class Page
    {
        [Key]
        public int id { get; set; }
        public int HotelID { get; set; }
        public string NameHotel { get; set; }
        public long GheymatYekShab { get; set; }
        public string imgUrl { get; set; }
        public DateTime ZamanShoroa { get; set; }
        public DateTime ZamanPayan { get; set; }
        public bool Faal { get; set; }
        public string Tozihat { get; set; }

        public List<lstjozeyat> lstjozeyats { get; set; }
        public List<Nazarha> lstnazar { get; set; }
        public List<Tasvirha> lsttasvir { get; set; }
        public List<Emkanat> lstemakanat { get; set; }

        public class lstjozeyat
        {
            public string ZarfiatOtagh { get; set; }
            public string TedadTakht { get; set; }
            public string TedadStare { get; set; }
        }

        public class Nazarha
        {
            public int Nazarat_ID { get; set; }
            public string Nazarat_Name { get; set; }
            public string Nazarat_Zaman { get; set; }
            public string Nazarat_Email { get; set; }
            public int Emtyaz { get; set; }
            public string Nazarat_Matn { get; set; }
        }

        public class Tasvirha
        {
            public int Tasavir_ID { get; set; }
            public string Tasavir_Name { get; set; }
            public bool Tasavir_Asli { get; set; }
            public string Url { get; set; }
        }

        public class Emkanat
        {
            public int Emkanat_ID { get; set; }
            public string Emkanat_Name { get; set; }
            public string Emkanat_Icon { get; set; }
            public int Emkanat_IdHotel { get; set; }
        }

    }
}
