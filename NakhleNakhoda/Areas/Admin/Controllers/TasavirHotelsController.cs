using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using NakhleNakhoda.Data;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using NakhleNakhoda.Data.Models.ViewModel;
using NakhleNakhoda.Data.Models.DB;
using Microsoft.AspNetCore.Authorization;

namespace NakhleNakhoda.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TasavirHotelsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;
        public TasavirHotelsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
            db = context;
        }
        [HttpGet("TasavirHotels/Index")]
        // GET: TasavirHotels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.tasavirHotels
                .Include(t => t.Hoteltasvir);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TasavirHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasavirHotel = await db.tasavirHotels
                .Include(t => t.Hoteltasvir)
                .FirstOrDefaultAsync(m => m.Tasavir_ID == id);
            if (tasavirHotel == null)
            {
                return NotFound();
            }

            return View(tasavirHotel);
        }

        // GET: TasavirHotels/Create
        public IActionResult Create(/*int? id*/)
        {
            //var q = db.hotels.Find(id);
            //ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, q.Hotel_ID.ToString(), q.Name_Hotel);
            ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel");
            return View();
        }

        // POST: TasavirHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tasavir_ID,Tasavir_IDHotel,Tasavir_Asli,Tasavir")] Tasvir_VM tasavirHotel)
        {
            if (ModelState.IsValid)
            {
                if (tasavirHotel.Tasavir.Length > 0)
                {
                    var upload = Path.Combine(hostingEnvironment.WebRootPath, "upload/img");
                    var file = Path.Combine(upload, tasavirHotel.Tasavir.FileName);
                    using (var f = new FileStream(file, FileMode.Create))
                    {
                        await tasavirHotel.Tasavir.CopyToAsync(f).ConfigureAwait(false);
                    }
                    TasvirHotel t = new TasvirHotel();
                    t.Tasavir_Asli = tasavirHotel.Tasavir_Asli;
                    t.Tasavir_IDHotel = tasavirHotel.Tasavir_IDHotel;
                    t.Tasavir_Name = tasavirHotel.Tasavir.FileName;
                    db.Add(t);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", tasavirHotel.Tasavir_IDHotel);
                    return View(tasavirHotel);
                }
            }
            ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", tasavirHotel.Tasavir_IDHotel);
            return View(tasavirHotel);
            
        }

        // GET: TasavirHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasavirHotel = await db.tasavirHotels.FindAsync(id);
            if (tasavirHotel == null)
            {
                return NotFound();
            }
            ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", tasavirHotel.Tasavir_IDHotel);
            return View(tasavirHotel);
        }

        // POST: TasavirHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Tasavir_ID,Tasavir_Name,Tasavir_IDHotel,Tasavir_Asli")] TasvirHotel tasavirHotel, IFormFile Tasvir)
        {
            if (id != tasavirHotel.Tasavir_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Tasvir.Length > 0)
                    {
                        var upload = Path.Combine(hostingEnvironment.WebRootPath, "upload/img");
                        var file = Path.Combine(upload, Tasvir.FileName);
                        using (var f = new FileStream(file, FileMode.Create))
                        {
                            await Tasvir.CopyToAsync(f).ConfigureAwait(false);
                        }

                        var q = db.tasavirHotels
                            .Where(a => a.Tasavir_ID == tasavirHotel.Tasavir_ID)
                            .FirstOrDefault();
                        q.Tasavir_Asli = tasavirHotel.Tasavir_Asli;
                        q.Tasavir_ID = tasavirHotel.Tasavir_ID;
                        q.Tasavir_IDHotel = tasavirHotel.Tasavir_IDHotel;
                        q.Tasavir_Name = Tasvir.FileName;
                        db.Update(q);
                        await db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var q = db.tasavirHotels
                           .Where(a => a.Tasavir_ID == tasavirHotel.Tasavir_ID)
                           .FirstOrDefault();
                        q.Tasavir_Asli = tasavirHotel.Tasavir_Asli;
                        q.Tasavir_ID = tasavirHotel.Tasavir_ID;
                        q.Tasavir_IDHotel = tasavirHotel.Tasavir_IDHotel;

                        db.Update(q);
                        await db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasavirHotelExists(tasavirHotel.Tasavir_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Tasavir_IDHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", tasavirHotel.Tasavir_IDHotel);
            return View(tasavirHotel);
            
        }

        // GET: TasavirHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasavirHotel = await db.tasavirHotels
                .Include(t => t.Hoteltasvir)
                .FirstOrDefaultAsync(m => m.Tasavir_ID == id);
            if (tasavirHotel == null)
            {
                return NotFound();
            }

            return View(tasavirHotel);
        }

        // POST: TasavirHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasavirHotel = await db.tasavirHotels.FindAsync(id);

            var upload = Path.Combine(hostingEnvironment.WebRootPath, "upload/img");
            var file = Path.Combine(upload, tasavirHotel.Tasavir_Name);
            FileInfo f = new FileInfo(file);
            if (f.Exists)
            {
                f.Delete();
            }

            db.tasavirHotels.Remove(tasavirHotel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasavirHotelExists(int id)
        {
            return db.tasavirHotels.Any(e => e.Tasavir_ID == id);
        }
    }
}
