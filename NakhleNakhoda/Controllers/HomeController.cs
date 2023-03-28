using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Models.Rep;
using NakhleNakhoda.Data.Models.ViewModel;
using NakhleNakhoda.ViewModels;
using System;
using System.Diagnostics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NakhleNakhoda.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = null;
        private Page_Rep page_Rep;
        public HomeController(ApplicationDbContext _db, Page_Rep _page_Rep)
        {
            db = _db;
            page_Rep = _page_Rep;
        }
        //[Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllHotels()
        {
            return View();
        }
        
        public IActionResult Jostejo(DateTime tarikhvorod, DateTime tarikhkhoroj)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dtv = new DateTime(tarikhvorod.Year, tarikhvorod.Month, tarikhvorod.Day, pc);
            DateTime dtkh = new DateTime(tarikhkhoroj.Year, tarikhkhoroj.Month, tarikhkhoroj.Day, pc);

            var qhotels = db.hotels
            .Where(a => a.ZamanShoroa <= dtv && a.ZamanPayan >= dtkh && a.Faal == true && dtkh > DateTime.Now && dtv >= DateTime.Today)
            .ToList();

            if (qhotels == null)
                return RedirectToAction(nameof(Index));

            List<int> lstidhotel = new List<int>();
           

            if (lstidhotel.Count() > 0)
            {
                List<Jostejo> lstjostejo = new List<Jostejo>();
                foreach (var item in lstidhotel)
                {
                    if (item > 0)
                    {
                        var qhotel = db.hotels
                        .Where(a => a.Hotel_ID.Equals(item) && a.ZamanShoroa <= dtv && a.ZamanPayan >= dtkh && a.Faal == true && dtkh > DateTime.Now && dtv >= DateTime.Today)
                        .FirstOrDefault();

                        var qimg = db.tasavirHotels
                            .Where(a => a.Tasavir_IDHotel.Equals(item) && a.Tasavir_Asli.Equals(true))
                            .FirstOrDefault();


                        var qjozyat = db.jozeyatHotels
                            .Where(a => a.Jozeyat_HotelID.Equals(item))
                            .FirstOrDefault();

                        if (qjozyat == null)
                            return RedirectToAction(nameof(Index));

                        
                        var qstareh = db.tedadStarehs.Where(a => a.TedadStareh_ID.Equals(qjozyat.Jozeyat_TedadStareID)).FirstOrDefault().TedadStareh_Name;

                        if (qstareh == null || qimg == null || qhotel == null)
                            return RedirectToAction(nameof(Index));

                        Jostejo j = new Jostejo();
                        j.GheymatYekShab = qhotel.Jozeyat_Gheymat;
                        j.HotelID = qhotel.Hotel_ID;
                        j.imgUrl = /*"/img/img/" +*/ qimg.Tasavir_Name /*+ ""*/;
                        j.Stareh = qstareh;
                        j.NameHotel = qhotel.Name_Hotel;
                        lstjostejo.Add(j);
                    }

                }

                string khroj = string.Format("{0}/{1}/{2}", pc.GetYear(dtkh), pc.GetMonth(dtkh), pc.GetDayOfMonth(dtkh));
                string vorod = string.Format("{0}/{1}/{2}", pc.GetYear(dtv), pc.GetMonth(dtv), pc.GetDayOfMonth(dtv));

                ViewData["khoroj"] = khroj;
                ViewData["vorod"] = vorod;
                return View(lstjostejo);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }
        public IActionResult Rooms(DateTime tarikhvorod, DateTime tarikhkhoroj)
        {
            
            //DateTime tarikhvorod = DateTime.ParseExact(tarikhvorod1, "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //DateTime tarikhkhoroj= DateTime.ParseExact(tarikhkhoroj1, "MM-dd-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            PersianCalendar pc = new PersianCalendar();
            DateTime dtv = new DateTime(tarikhvorod.Year, tarikhvorod.Month, tarikhvorod.Day, pc);
            DateTime dtkh =new DateTime(tarikhkhoroj.Year,tarikhkhoroj.Month, tarikhkhoroj.Day, pc);

            var qhotels = db.hotels
            .Where(a => a.ZamanShoroa <= dtv && a.ZamanPayan >= dtkh && a.Faal == true && dtkh > DateTime.Now && dtv >= DateTime.Today)
            .ToList();

            if (qhotels == null)
                return RedirectToAction(nameof(Index));

            List<int> lstidhotel = new List<int>();
            foreach (var o in qhotels)
            {
                var qjozeyat = db.jozeyatHotels
               .Where(a => a.Jozeyat_HotelID == o.Hotel_ID)
               .FirstOrDefault();

                if (qjozeyat != null)
                    lstidhotel.Add(o.Hotel_ID);
                else
                    lstidhotel.Add(0);

            }


            if (lstidhotel.Count() > 0)
            {
                List<Jostejo> lstjostejo = new List<Jostejo>();
                foreach (var item in lstidhotel)
                {
                    if (item > 0)
                    {
                        var qhotel = db.hotels
                        .Where(a => a.Hotel_ID.Equals(item) && a.ZamanShoroa <= dtv && a.ZamanPayan >= dtkh && a.Faal == true && dtkh > DateTime.Now && dtv >= DateTime.Today)
                        .FirstOrDefault();

                        var qimg = db.tasavirHotels
                            .Where(a => a.Tasavir_IDHotel.Equals(item) && a.Tasavir_Asli.Equals(true))
                            .FirstOrDefault();


                        var qjozyat = db.jozeyatHotels
                            .Where(a => a.Jozeyat_HotelID.Equals(item))
                            .FirstOrDefault();

                        if (qjozyat == null)
                            return RedirectToAction(nameof(Index));


                        var qstareh = db.tedadStarehs.Where(a => a.TedadStareh_ID.Equals(qjozyat.Jozeyat_TedadStareID)).FirstOrDefault().TedadStareh_Name;

                        if (qstareh == null || qimg == null || qhotel == null)
                            return RedirectToAction(nameof(Index));

                        Jostejo j = new Jostejo();
                        j.GheymatYekShab = qhotel.Jozeyat_Gheymat;
                        j.HotelID = qhotel.Hotel_ID;
                        j.imgUrl = "/Ghaleb/img/img/" + qimg.Tasavir_Name + "";
                        j.Stareh = qstareh;
                        j.NameHotel = qhotel.Name_Hotel;
                        lstjostejo.Add(j);
                    }

                }

                string khroj = string.Format("{0}/{1}/{2}", pc.GetYear(dtkh), pc.GetMonth(dtkh), pc.GetDayOfMonth(dtkh));
                string vorod = string.Format("{0}/{1}/{2}", pc.GetYear(dtv), pc.GetMonth(dtv), pc.GetDayOfMonth(dtv));

                ViewData["khoroj"] = khroj;
                ViewData["vorod"] = vorod;
                return View(lstjostejo);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult Showroom(int id, DateTime vorod, DateTime khoroj)
        {

            var qpage = page_Rep.GetPage(id);


            PersianCalendar pc = new PersianCalendar();
            DateTime dtv = new DateTime(vorod.Year, vorod.Month, vorod.Day, pc);
            DateTime dtkh = new DateTime(khoroj.Year, khoroj.Month, khoroj.Day, pc);

            string dtk1 = string.Format("{0}/{1}/{2}", pc.GetYear(dtkh), pc.GetMonth(dtkh), pc.GetDayOfMonth(dtkh));
            string dtv1 = string.Format("{0}/{1}/{2}", pc.GetYear(dtv), pc.GetMonth(dtv), pc.GetDayOfMonth(dtv));


            if (dtk1 == "1/1/1")
            {
                DateTime dt1 = DateTime.Today;
                DateTime dt2 = DateTime.Today.AddDays(1);
                string dtk2 = string.Format("{0}/{1}/{2}", pc.GetYear(dt2), pc.GetMonth(dt2), pc.GetDayOfMonth(dt2));
                string dtv2 = string.Format("{0}/{1}/{2}", pc.GetYear(dt1), pc.GetMonth(dt1), pc.GetDayOfMonth(dt1));

                ViewData["khoroj"] = dtk2;
                ViewData["vorod"] = dtv2;
            }
            else
            {
                ViewData["khoroj"] = dtk1;
                ViewData["vorod"] = dtv1;
            }


            return View(qpage);

        }
        public IActionResult Entekhab(DateTime tarikhvorod, DateTime tarikhkhoroj, string tedadtakht, string zarfeyat, int id)
        {

             var qpage = page_Rep.GetNemayeshHotel(tarikhvorod, tarikhkhoroj, tedadtakht, zarfeyat, id);
            ViewData["vorod"] = tarikhvorod;
            ViewData["khoroj"] = tarikhkhoroj;
            ViewData["id"] = id;
            return View(qpage);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}