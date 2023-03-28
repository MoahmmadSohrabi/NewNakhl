using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using NakhleNakhoda.Data.Models.DB;
using NakhleNakhoda.Data.Models.ViewModel;

namespace NakhleNakhoda.Data.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EmkanatHotelsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;
        public EmkanatHotelsController(ApplicationDbContext context , IHostingEnvironment hosting)
        {
            db = context;
            hostingEnvironment = hosting;
        }

        // GET: EmkanatHotels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.emkanatHotels.Include(e => e.HotelEmkanat);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmkanatHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emkanatHotel = await db.emkanatHotels
                .Include(e => e.HotelEmkanat)
                .FirstOrDefaultAsync(m => m.Emkanat_ID == id);
            if (emkanatHotel == null)
            {
                return NotFound();
            }

            return View(emkanatHotel);
        }

        // GET: EmkanatHotels/Create
        public IActionResult Create()
        {
            ViewData["Emkanat_IdHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel");
            return View();
        }

        // POST: EmkanatHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Emkanat_ID,Emkanat_Name,Emkanat_Icon,Emkanat_IdHotel")] Emkanat_VM emkanatHotel)
        {
            if (ModelState.IsValid)
            {
                var upload = Path.Combine(hostingEnvironment.WebRootPath, "upload/emkan");
                var file = Path.Combine(upload, emkanatHotel.Emkanat_Icon.FileName);
                using (var f = new FileStream(file,FileMode.Create))
                {
                    await emkanatHotel.Emkanat_Icon.CopyToAsync(f).ConfigureAwait(false);
                }

                EmkanatHotel e = new EmkanatHotel();
                e.Emkanat_Icon = emkanatHotel.Emkanat_Icon.FileName;
                e.Emkanat_IdHotel = emkanatHotel.Emkanat_IdHotel;
                e.Emkanat_Name = emkanatHotel.Emkanat_Name;
                db.Add(e);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Emkanat_IdHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", emkanatHotel.Emkanat_IdHotel);
            return View(emkanatHotel);
        }

        // GET: EmkanatHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emkanatHotel = await db.emkanatHotels.FindAsync(id);
            if (emkanatHotel == null)
            {
                return NotFound();
            }
            ViewData["Emkanat_IdHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", emkanatHotel.Emkanat_IdHotel);
            return View(emkanatHotel);
        }

        // POST: EmkanatHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Emkanat_ID,Emkanat_Name,Emkanat_Icon,Emkanat_IdHotel")] EmkanatHotel emkanatHotel , IFormFile Icon)
        {
            if (id != emkanatHotel.Emkanat_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Icon.Length > 0)
                    {
                        var upload = Path.Combine(hostingEnvironment.WebRootPath, "upload/emkan");
                        var file = Path.Combine(upload, Icon.FileName);
                        using (var f = new FileStream(file, FileMode.Create))
                        {
                            await Icon.CopyToAsync(f).ConfigureAwait(false);
                        }

                    }

                    var q = db.emkanatHotels.Find(id);
                    q.Emkanat_Icon = Icon.FileName;
                    q.Emkanat_IdHotel = emkanatHotel.Emkanat_IdHotel;
                    q.Emkanat_Name = emkanatHotel.Emkanat_Name;
                    db.Update(q);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmkanatHotelExists(emkanatHotel.Emkanat_ID))
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
            ViewData["Emkanat_IdHotel"] = new SelectList(db.hotels, "Hotel_ID", "Name_Hotel", emkanatHotel.Emkanat_IdHotel);
            return View(emkanatHotel);
        }

        // GET: EmkanatHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emkanatHotel = await db.emkanatHotels
                .Include(e => e.HotelEmkanat)
                .FirstOrDefaultAsync(m => m.Emkanat_ID == id);
            if (emkanatHotel == null)
            {
                return NotFound();
            }

            return View(emkanatHotel);
        }

        // POST: EmkanatHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emkanatHotel = await db.emkanatHotels.FindAsync(id);

            var uplaod = Path.Combine(hostingEnvironment.WebRootPath, "upload/emkan");
            var file = Path.Combine(uplaod, emkanatHotel.Emkanat_Icon);
            FileInfo f = new FileInfo(file);
            if (f.Exists)
            {
                f.Delete();
            }
            db.emkanatHotels.Remove(emkanatHotel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmkanatHotelExists(int id)
        {
            return db.emkanatHotels.Any(e => e.Emkanat_ID == id);
        }
    }
}
