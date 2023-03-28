using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Models;
using NakhleNakhoda.Data.Models.DB;
using NakhleNakhoda.Data.Models.ViewModel;
using System.Globalization;
using System.Net;
using zp;

namespace NakhleNakhoda.Web.Controllers
{
    public class RezervController : Controller
    {
        private ApplicationDbContext db = null;
        private readonly IEmailSender _emailSender;
        //private Page_Rep page_Rep;
        public RezervController(IEmailSender emailSender,ApplicationDbContext _db/*, Page_Rep _page_Rep*/)
        {
            db = _db;
            _emailSender = emailSender;
            //page_Rep = _page_Rep;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rezerv(int idhotel, string tedadtakht, string zarfeyat, DateTime vorod, DateTime khoroj)
        {

            PersianCalendar pc = new PersianCalendar();
            DateTime dtv = new DateTime(vorod.Year, vorod.Month, vorod.Day, pc);
            DateTime dtkh = new DateTime(khoroj.Year, khoroj.Month, khoroj.Day, pc);

            var nafarat = db.zarfyatOtaghHotels.Where(a => a.ZarfyatOtagh_Name == zarfeyat).FirstOrDefault();
            var otagh = db.tedadTakhtHotels.Where(a => a.TedadTakh_Name == tedadtakht).FirstOrDefault();
            var hotel = db.hotels.Where(a => a.Hotel_ID == idhotel).FirstOrDefault();
            var qtedad = db.jozeyatHotels.Where(a => a.Jozeyat_HotelID == hotel.Hotel_ID && a.Jozeyat_TedadTakhtID == otagh.TedadTakh_ID).FirstOrDefault();

            string n = nafarat.ZarfyatOtagh_Name.Split().First();
            var roz = khoroj.Date - vorod.Date;
            int m = roz.Days;

            if (nafarat == null && zarfeyat == null && hotel == null && qtedad.Jozeyat_Teadad == 0)
                return RedirectToAction("", "");

            long mablagh = hotel.Jozeyat_Gheymat * Convert.ToInt64(n) * m;

            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            var qrezerv = db.rezervHotels.Where(a => a.Rezerv_IP == ip && a.Rezerv_Vazeyt == false && hotel.Hotel_ID == idhotel && a.Rezerv_TeadadOthagh == otagh.TedadTakh_ID && a.Rezerv_TeadadNafarat == nafarat.ZarfyatOtagh_ID && a.Rezerv_Vorod == dtv && a.Rezerv_Khoroj == dtkh).FirstOrDefault();
            if (qrezerv == null)
            {
                RezervHotel r = new RezervHotel();
                r.Rezerv_IDHotel = idhotel;
                r.Rezerv_Khoroj = dtkh;
                r.Rezerv_TeadadNafarat = nafarat.ZarfyatOtagh_ID;
                r.Rezerv_TeadadOthagh = otagh.TedadTakh_ID;
                r.Rezerv_Vorod = dtv;
                r.Rezerv_Vazeyt = false;
                r.Rezerv_Mablagh = mablagh;
                r.Rezerv_Roz = m;
                r.Rezerv_Codemeli = Convert.ToString(123456789);
                r.Rezerv_Email = "12346@gmail.com";
                r.Rezerv_Jensiat = 0;
                r.Rezerv_Mobile = "123456";
                r.Rezerv_Name = "123456";
                r.Rezerv_NameKhanevadgi = "123456";
                r.Rezerv_IP = HttpContext.Connection.RemoteIpAddress.ToString();
                db.rezervHotels.Add(r);
                db.SaveChanges();

                TempData["ip"] = r.Rezerv_IP;
                TempData["vorod"] = vorod;
                TempData["khoroj"] = khoroj;
                TempData["nafarat"] = nafarat.ZarfyatOtagh_ID;
                TempData["takht"] = otagh.TedadTakh_ID;
                return RedirectToAction("ShowRezerv");
            }
            else
            {
                TempData["ip"] = HttpContext.Connection.RemoteIpAddress.ToString();
                TempData["nafarat"] = nafarat.ZarfyatOtagh_ID;
                TempData["takht"] = otagh.TedadTakh_ID;
                return RedirectToAction("ShowRezerv");

            }
        }

        public IActionResult ShowRezerv()
        {
            ViewData["ip"] = TempData["ip"];
            string ip = TempData["ip"].ToString();

            ViewData["nafarat"] = TempData["nafarat"];
            string nafar = TempData["nafarat"].ToString();

            ViewData["takht"] = TempData["takht"];
            string takht = TempData["takht"].ToString();

            var nafarat = db.zarfyatOtaghHotels.Where(a => a.ZarfyatOtagh_ID == Convert.ToInt32(nafar)).FirstOrDefault();
            var otagh = db.tedadTakhtHotels.Where(a => a.TedadTakh_ID == Convert.ToInt32(takht)).FirstOrDefault();

            var qrezerv = db.rezervHotels.Where(a => a.Rezerv_IP == ip && a.Rezerv_Vazeyt == false && a.Rezerv_TeadadOthagh == otagh.TedadTakh_ID && a.Rezerv_TeadadNafarat == nafarat.ZarfyatOtagh_ID).FirstOrDefault();

            if (qrezerv == null)
                return null;
            else
                return View(qrezerv);
        }
        public async Task<IActionResult> Pardakht(int jensyat, string name, string namekhanvadegy, string codemeli, string email, string mobile, int idrezerv, int idhotel)
        {
            var qrezerv = db.rezervHotels.Where(a => a.Rezerv_ID == idrezerv && a.Rezerv_IDHotel == idhotel && a.Rezerv_Vazeyt == false).FirstOrDefault();

            if (qrezerv == null)
                return RedirectToAction("");
            else
            {

                ServicePointManager.Expect100Continue = false;
                PaymentGatewayImplementationServicePortTypeClient zp = new PaymentGatewayImplementationServicePortTypeClient();

                String MerchantID = "a01e03e4-00a2-11e7-93c3-005056a205be";
                String CallbackURL = "http://nakhlenakhoda.ir/Rezerv/KharidNahaei";
                int mablagh = Convert.ToInt32(qrezerv.Rezerv_Mablagh) / 10;
                String Description = "نخل پرداخت تست";

                var x = await zp.PaymentRequestAsync(MerchantID, mablagh, Description, "", "", CallbackURL);
                if (x.Body.Status == 100)
                {
                    Random rnd = new Random();
                    string pigiry = rnd.Next(0000, 999999).ToString() + DateTime.Now.Millisecond;
                    PardakhtHotel p = new PardakhtHotel();
                    p.Pardakh_IDHotel = qrezerv.Rezerv_IDHotel;
                    p.Pardakh_Mablagh = qrezerv.Rezerv_Mablagh;
                    p.Pardakh_Marjaa = x.Body.Authority;
                    p.Pardakh_Pigiry = pigiry;
                    p.Pardakh_Rezerv = idrezerv;
                    p.Pardakh_Vazeiat = false;
                    p.Pardakh_ZamanVariz = DateTime.Now;
                    p.Pardakh_Trakonesh = "123";
                    db.pardakhtHotels.Add(p);
                    db.SaveChanges();

                    qrezerv.Rezerv_Codemeli = codemeli;
                    qrezerv.Rezerv_Email = email;
                    qrezerv.Rezerv_Jensiat = jensyat;
                    qrezerv.Rezerv_Mobile = mobile;
                    qrezerv.Rezerv_Name = name;
                    qrezerv.Rezerv_NameKhanevadgi = namekhanvadegy;
                    db.Update(qrezerv);
                    db.SaveChanges();
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + x.Body.Authority);
                }
                else
                {
                    return RedirectToAction("");
                }


            }

        }

        public async Task<IActionResult> KharidNahaei(string Status, string Authority)
        {
            if (Status == null && Authority == null)
                return RedirectToAction("Error", "Home");

            if (Status == "OK")
            {
                var qkhrid = db.pardakhtHotels
                   .Where(a => a.Pardakh_Marjaa == Authority && a.Pardakh_Vazeiat == false)
                   .SingleOrDefault();

                if (qkhrid == null)
                    return RedirectToAction("Error", "Home");


                ServicePointManager.Expect100Continue = false;
                PaymentGatewayImplementationServicePortTypeClient zp = new PaymentGatewayImplementationServicePortTypeClient();
                String MerchantID = "a01e03e4-00a2-11e7-93c3-005056a205be";
                string Authoritys = Authority;
                int Amount = Convert.ToInt32(qkhrid.Pardakh_Mablagh) / 10;
                var x = await zp.PaymentVerificationAsync(MerchantID, Authoritys, Amount);
                if (x.Body.Status == 100)
                {
                    qkhrid.Pardakh_Trakonesh = Convert.ToString(x.Body.RefID);
                    qkhrid.Pardakh_Vazeiat = true;
                    db.Update(qkhrid);
                    db.SaveChanges();

                    var q = db.rezervHotels.Where(a => a.Rezerv_ID == qkhrid.Pardakh_Rezerv && a.Rezerv_Vazeyt == false).FirstOrDefault();

                    q.Rezerv_Vazeyt = true;
                    db.rezervHotels.Update(q);
                    db.SaveChanges();

                    var qjozeyat = db.jozeyatHotels.Where(a => a.Jozeyat_HotelID == qkhrid.Pardakh_IDHotel && a.Jozeyat_ZarfiatOtaghID == q.Rezerv_TeadadOthagh).FirstOrDefault();
                    qjozeyat.Jozeyat_Teadad = qjozeyat.Jozeyat_Teadad - 1;
                    db.jozeyatHotels.Update(qjozeyat);
                    db.SaveChanges();

                    PersianCalendar pc = new PersianCalendar();
                    string zaman = string.Format("{0}/{1}/{2}", pc.GetYear(qkhrid.Pardakh_ZamanVariz), pc.GetMonth(qkhrid.Pardakh_ZamanVariz), pc.GetDayOfMonth(qkhrid.Pardakh_ZamanVariz));

                    string matn = "رزرو شما با موفقیت انجام شد. شماره پیگیری" + qkhrid.Pardakh_Pigiry + ". تاریخ رزرو " + zaman + "";
                    await _emailSender.SendEmailAsync(q.Rezerv_Email, "رزرو با موفقیت انجام شد", matn);
                    //SendEmail2.Send(q.Rezerv_Email, "رزرو با موفقیت انجام شد", matn);


                    return RedirectToAction(nameof(Sorathesab), new { id = qkhrid.Pardakh_ID });

                }


            }
            return View();
        }

        public IActionResult Sorathesab(int id)
        {
            if (id > 0)
            {
                var qkharid = db.pardakhtHotels.Where(a => a.Pardakh_ID == id && a.Pardakh_Vazeiat == true).FirstOrDefault();
                var q = db.rezervHotels.Where(a => a.Rezerv_ID == qkharid.Pardakh_Rezerv).FirstOrDefault();

                Sorathesabs s = new Sorathesabs();
                s.Codepigiry = qkharid.Pardakh_Pigiry;
                s.Gheymat = qkharid.Pardakh_Mablagh;
                s.IDHotel = q.Rezerv_IDHotel;
                s.NameNameKhanevadgy = q.Rezerv_Name + " " + q.Rezerv_NameKhanevadgi;
                s.Tarikhvariz = qkharid.Pardakh_ZamanVariz;
                s.Trakonesh = qkharid.Pardakh_Trakonesh;

                return View(s);

            }
            else
            {
                return View(null);
            }

        }
    }
}
