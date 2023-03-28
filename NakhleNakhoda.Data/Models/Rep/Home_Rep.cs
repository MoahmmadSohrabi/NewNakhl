using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NakhleNakhoda.Data.Models.Rep
{
    public class Home_Rep :IDisposable
    {
        private readonly ApplicationDbContext db=null;
        public Home_Rep(ApplicationDbContext _db)
        {
            db = _db;

        }
        public List<AllHotels> GetAllNew()
        {
            var qnew = db.hotels.OrderByDescending(a => a.Hotel_ID)
                .Include(a => a.TasavirHotels)
                .ToList();

            List<AllHotels> lstnew = new List<AllHotels>();

            if (qnew == null)
                return null;

            foreach (var item in qnew)
            {
                var qtasvir = db.tasavirHotels
                    .Where(a => a.Tasavir_IDHotel == item.Hotel_ID && a.Tasavir_Asli == true)
                    .FirstOrDefault();

                if (qtasvir != null)
                {
                    AllHotels hotel = new AllHotels();
                    hotel.ID = item.Hotel_ID;
                    hotel.GheymatYekShab = item.Jozeyat_Gheymat;
                    hotel.name = item.Name_Hotel;
                    hotel.urltasvir = "/Ghaleb/img/img/" + qtasvir.Tasavir_Name + "";



                    lstnew.Add(hotel);
                }
            }

            if (lstnew == null)
            {
                return null;
            }
            else
            {
                return lstnew;
            }

        }
        public List<HotelJadid> GetNew()
        {
            var qnew = db.hotels.OrderByDescending(a => a.Hotel_ID)
                .Include(a => a.TasavirHotels)
                .ToList().Take(6);

            List<HotelJadid> lstnew = new List<HotelJadid>();

            if (qnew == null)
                return null;

            foreach (var item in qnew)
            {
                var qtasvir = db.tasavirHotels
                    .Where(a => a.Tasavir_IDHotel == item.Hotel_ID && a.Tasavir_Asli == true)
                    .FirstOrDefault();

                if (qtasvir != null)
                {
                    HotelJadid hotel = new HotelJadid();
                    hotel.ID = item.Hotel_ID;
                    hotel.name = item.Name_Hotel;
                    hotel.urltasvir = "/Ghaleb/img/img/" + qtasvir.Tasavir_Name+"";
                    
                    

                    lstnew.Add(hotel);
                }
            }

            if (lstnew == null)
            {
                return null;
            }
            else
            {
                return lstnew;
            }

        }


        public List<HotelMahbob> GetMahbob()
        {
            var qnazar = db.nazarats.ToList();

            var results = (from p in qnazar
                           group p.Nazarat_Emtyaz by p.Nazarat_HotelID into g
                           select new { HotelID = g.Key, Sum = g.ToList() });

            List<HotelMahbob> lstint = new List<HotelMahbob>();
            foreach (var item in results)
            {
                var qhotel = db.hotels.Where(a => a.Hotel_ID == item.HotelID).FirstOrDefault();
                var qtasvir = db.tasavirHotels.Where(a => a.Tasavir_Asli == true && a.Tasavir_IDHotel == item.HotelID).FirstOrDefault();
                HotelMahbob h = new HotelMahbob();
                h.ID = item.HotelID;
                h.sum = item.Sum.Sum();
                h.name = qhotel.Name_Hotel;
                h.urltasvir = "/Ghaleb/img/img/" + qtasvir.Tasavir_Name + "";

                lstint.Add(h);
            }

            List<HotelJadid> lstnew = new List<HotelJadid>();

            if (lstint == null)
                return null;

            var qorder = lstint.OrderByDescending(a => a.sum).Take(4);
            if (qorder == null)
            {
                return null;
            }
            else
            {
                return qorder.ToList();
            }
        }


       

        ~Home_Rep()
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
