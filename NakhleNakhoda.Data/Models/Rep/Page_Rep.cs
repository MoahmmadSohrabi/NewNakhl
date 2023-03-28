using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Data.Models.DB;
using NakhleNakhoda.Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.Rep
{
    public class Page_Rep : IDisposable
    {
        private readonly ApplicationDbContext db = null;
        public Page_Rep(ApplicationDbContext _db)
        {
            db = _db;
        }

        public Page GetPage(int id)
        {
            
                var get = (from a in db.hotels
                           where a.Hotel_ID == id && a.Faal == true
                           select a).SingleOrDefault();

                if (get == null)
                    return null;

                var qemkanat = db.emkanatHotels.Where(a => a.Emkanat_IdHotel.Equals(id)).ToList();

                List<Page.Emkanat> emkanats = new List<Page.Emkanat>();
                foreach (var item in qemkanat)
                {

                    Page.Emkanat lsemkanant = new Page.Emkanat();
                    lsemkanant.Emkanat_Icon = item.Emkanat_Icon;
                    lsemkanant.Emkanat_ID = item.Emkanat_ID;
                    lsemkanant.Emkanat_IdHotel = item.Emkanat_IdHotel;
                    lsemkanant.Emkanat_Name = item.Emkanat_Name;
                    emkanats.Add(lsemkanant);

                }

                var qnazar = db.nazarats.Where(a => a.Nazarat_HotelID.Equals(id)).ToList();
                List<Page.Nazarha> lstnazarha = new List<Page.Nazarha>();
                foreach (var item in qnazar)
                {
                    Page.Nazarha n = new Page.Nazarha();
                    n.Emtyaz = item.Nazarat_Emtyaz;
                    n.Nazarat_Email = item.Nazarat_Email;
                    n.Nazarat_ID = item.Nazarat_ID;
                    n.Nazarat_Name = item.Nazarat_Name;
                    n.Nazarat_Zaman = item.Nazarat_Zaman;
                    n.Nazarat_Matn = item.Nazarat_Matn;
                    lstnazarha.Add(n);
                }

                var qtasvir = db.tasavirHotels.Where(a => a.Tasavir_IDHotel.Equals(id)).ToList();
                List<Page.Tasvirha> lstasvir = new List<Page.Tasvirha>();
                foreach (var item in qtasvir.Where(a => a.Tasavir_Asli == false))
                {
                    Page.Tasvirha t = new Page.Tasvirha();
                    t.Tasavir_Asli = item.Tasavir_Asli;
                    t.Tasavir_ID = item.Tasavir_ID;
                    t.Tasavir_Name = item.Tasavir_Name;
                    t.Url = "/Ghaleb/img/img/" + item.Tasavir_Name + "";
                    lstasvir.Add(t);
                }

                var qjozeyat = db.jozeyatHotels
                 .Where(a => a.Jozeyat_HotelID.Equals(get.Hotel_ID))
                 .OrderByDescending(a => a.Jozeyat_ZarfiatOtaghID)
                 .ToList();

                List<Page.lstjozeyat> list1 = new List<Page.lstjozeyat>();
                foreach (var item in qjozeyat)
                {


                    var stareh = db.tedadStarehs
                     .Where(a => a.TedadStareh_ID == item.Jozeyat_TedadStareID)
                     .FirstOrDefault();

                    var zarfyat = db.zarfyatOtaghHotels
                        .Where(a => a.ZarfyatOtagh_ID == item.Jozeyat_ZarfiatOtaghID)
                        .FirstOrDefault();

                    var takht = db.tedadTakhtHotels
                        .Where(a => a.TedadTakh_ID == item.Jozeyat_TedadTakhtID)
                        .FirstOrDefault();

                    Page.lstjozeyat lstjozeyat = new Page.lstjozeyat();
                    lstjozeyat.TedadStare = stareh.TedadStareh_Name == null ? "هتل معمولی" : stareh.TedadStareh_Name;
                    lstjozeyat.TedadTakht = takht.TedadTakh_Name == null ? "بدون تخت" : takht.TedadTakh_Name;
                    lstjozeyat.ZarfiatOtagh = zarfyat.ZarfyatOtagh_Name == null ? "بدون محدودیت" : zarfyat.ZarfyatOtagh_Name;
                    list1.Add(lstjozeyat);


                }

                Page p = new Page();
                p.Faal = get.Faal;
                p.GheymatYekShab = get.Jozeyat_Gheymat;
                p.HotelID = get.Hotel_ID;
                p.NameHotel = get.Name_Hotel;
                p.ZamanPayan = get.ZamanPayan;
                p.ZamanShoroa = get.ZamanShoroa;
                p.Tozihat = get.Tozihat;
                p.lstemakanat = emkanats;
                p.lstnazar = lstnazarha;
                p.lsttasvir = lstasvir;
                p.lstjozeyats = list1;
                p.imgUrl = "/Ghaleb/img/img/" + qtasvir.Where(a => a.Tasavir_Asli.Equals(true)).FirstOrDefault().Tasavir_Name + "";
                return p ?? null;

            


        }

        public Page GetNemayeshHotel(DateTime tarikhvorod, DateTime tarikhkhoroj, string tedadtakht, string zarfeyat, int id)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dtv = new DateTime(tarikhvorod.Year, tarikhvorod.Month, tarikhvorod.Day, pc);
            DateTime dtkh = new DateTime(tarikhkhoroj.Year, tarikhkhoroj.Month, tarikhkhoroj.Day, pc);

            var qtakht = db.tedadTakhtHotels.Where(a => a.TedadTakh_Name == tedadtakht)
                .FirstOrDefault();

            var qnafar = db.zarfyatOtaghHotels.Where(a => a.ZarfyatOtagh_Name == zarfeyat)
                .FirstOrDefault();

            var qhotel = db.hotels.Where(a => a.Hotel_ID.Equals(id) && a.ZamanShoroa <= dtv && a.ZamanPayan >= dtkh && a.Faal == true && dtkh > DateTime.Now && dtv >= DateTime.Today).FirstOrDefault();

            if (qhotel == null)
            {

                return null;
            }
            else
            {
                var qjs = db.jozeyatHotels
                    .Where(a => a.Jozeyat_HotelID.Equals(qhotel.Hotel_ID) && qtakht.TedadTakh_ID == a.Jozeyat_TedadTakhtID && a.Jozeyat_ZarfiatOtaghID == qnafar.ZarfyatOtagh_ID && a.Jozeyat_Teadad > 0)
                    .ToList();

                if (qjs.Count() > 0)
                {
                    List<Page.lstjozeyat> list1 = new List<Page.lstjozeyat>();
                    foreach (var item in qjs)
                    {
                        

                        var stareh = db.tedadStarehs
                         .Where(a => a.TedadStareh_ID == item.Jozeyat_TedadStareID)
                         .FirstOrDefault();

                        Page.lstjozeyat lstjozeyat = new Page.lstjozeyat();
                        lstjozeyat.TedadStare = stareh.TedadStareh_Name == null ? "هتل معمولی" : stareh.TedadStareh_Name;
                        lstjozeyat.TedadTakht = qtakht.TedadTakh_Name == null ? "بدون تخت" : qtakht.TedadTakh_Name;
                        lstjozeyat.ZarfiatOtagh = qnafar.ZarfyatOtagh_Name == null ? "بدون محدودیت" : qnafar.ZarfyatOtagh_Name;
                        list1.Add(lstjozeyat);


                    }

                    var qemkanat = db.emkanatHotels.Where(a => a.Emkanat_IdHotel.Equals(id)).ToList();

                    List<Page.Emkanat> emkanats = new List<Page.Emkanat>();
                    foreach (var item in qemkanat)
                    {

                        Page.Emkanat lsemkanant = new Page.Emkanat();
                        lsemkanant.Emkanat_Icon = item.Emkanat_Icon;
                        lsemkanant.Emkanat_ID = item.Emkanat_ID;
                        lsemkanant.Emkanat_IdHotel = item.Emkanat_IdHotel;
                        lsemkanant.Emkanat_Name = item.Emkanat_Name;
                        emkanats.Add(lsemkanant);

                    }

                    var tasvir = db.tasavirHotels.Where(a => a.Tasavir_Asli.Equals(true) && a.Tasavir_IDHotel == id).FirstOrDefault().Tasavir_Name;

                    Page p = new Page();

                    p.GheymatYekShab = qhotel.Jozeyat_Gheymat;
                    p.HotelID = qhotel.Hotel_ID;
                    p.NameHotel = qhotel.Name_Hotel;
                    p.ZamanPayan = qhotel.ZamanPayan;
                    p.ZamanShoroa = qhotel.ZamanShoroa;
                    p.lstemakanat = emkanats;
                    p.lstjozeyats = list1;
                    p.imgUrl = "/Ghaleb/img/img/" + tasvir + "";
                    return p ?? null;

                }
                else
                {
                    return null;
                }
            }
        }

        public Rezerv getrezervhotel(int id)
        {
            var q = db.hotels.Where(a => a.Hotel_ID == id && a.Faal == true).FirstOrDefault();
            var tasvir = db.tasavirHotels.Where(a => a.Tasavir_IDHotel == id && a.Tasavir_Asli == true).FirstOrDefault();
            Rezerv r = new Rezerv();
            r.IDHotel = q.Hotel_ID;
            r.NameHotel = q.Name_Hotel;
            r.Tasvir = "/Ghaleb/img/img/" + tasvir.Tasavir_Name + "";
            r.Gheymat = q.Jozeyat_Gheymat;

            return r;
        }

        

        ~Page_Rep()
        {
            Dispose(true);
        }
        public void Dispose()
        {

        }

        public void Dispose(bool D)
        {
            if (D)
            {
                Dispose();
            }
        }

    }
}





