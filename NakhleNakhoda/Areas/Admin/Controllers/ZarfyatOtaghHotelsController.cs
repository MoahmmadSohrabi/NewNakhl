using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Models.DB;

namespace NakhleNakhoda.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ZarfyatOtaghHotelsController : Controller
    {
        private readonly ApplicationDbContext db;

        public ZarfyatOtaghHotelsController(ApplicationDbContext context)
        {
            db = context;
        }

        // GET: ZarfyatOtaghHotels
        public async Task<IActionResult> Index()
        {
            return View(await db.zarfyatOtaghHotels.ToListAsync());
        }

        // GET: ZarfyatOtaghHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarfyatOtaghHotel = await db.zarfyatOtaghHotels
                .FirstOrDefaultAsync(m => m.ZarfyatOtagh_ID == id);
            if (zarfyatOtaghHotel == null)
            {
                return NotFound();
            }

            return View(zarfyatOtaghHotel);
        }

        // GET: ZarfyatOtaghHotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZarfyatOtaghHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZarfyatOtagh_ID,ZarfyatOtagh_Name")] ZarfyatOtaghHotel zarfyatOtaghHotel)
        {
            if (ModelState.IsValid)
            {
                db.Add(zarfyatOtaghHotel);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zarfyatOtaghHotel);
        }

        // GET: ZarfyatOtaghHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarfyatOtaghHotel = await db.zarfyatOtaghHotels.FindAsync(id);
            if (zarfyatOtaghHotel == null)
            {
                return NotFound();
            }
            return View(zarfyatOtaghHotel);
        }

        // POST: ZarfyatOtaghHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZarfyatOtagh_ID,ZarfyatOtagh_Name")] ZarfyatOtaghHotel zarfyatOtaghHotel)
        {
            if (id != zarfyatOtaghHotel.ZarfyatOtagh_ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    db.Update(zarfyatOtaghHotel);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZarfyatOtaghHotelExists(zarfyatOtaghHotel.ZarfyatOtagh_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //return View(zarfyatOtaghHotel);
        }

        // GET: ZarfyatOtaghHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarfyatOtaghHotel = await db.zarfyatOtaghHotels
                .FirstOrDefaultAsync(m => m.ZarfyatOtagh_ID == id);
            if (zarfyatOtaghHotel == null)
            {
                return NotFound();
            }

            return View(zarfyatOtaghHotel);
        }

        // POST: ZarfyatOtaghHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zarfyatOtaghHotel = await db.zarfyatOtaghHotels.FindAsync(id);
            db.zarfyatOtaghHotels.Remove(zarfyatOtaghHotel);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZarfyatOtaghHotelExists(int id)
        {
            return db.zarfyatOtaghHotels.Any(e => e.ZarfyatOtagh_ID == id);
        }
    }
}
