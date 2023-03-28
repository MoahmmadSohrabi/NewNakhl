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
    [Authorize/*(Roles = "Admin")*/]
    [Area("Admin")]
    public class JozeyatHotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JozeyatHotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JozeyatHotels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.jozeyatHotels
                .Include(j => j.Hoteljoz)
                .Include(j => j.TedadStareh)
                .Include(j => j.TedadTakhtHotel)
                .Include(j => j.ZarfyatOtagh);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JozeyatHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jozeyatHotel = await _context.jozeyatHotels
                .Include(j => j.Hoteljoz)
                .Include(j => j.TedadStareh)
                .Include(j => j.TedadTakhtHotel)
                .Include(j => j.ZarfyatOtagh)
                .FirstOrDefaultAsync(m => m.Jozeyat_ID == id);
            if (jozeyatHotel == null)
            {
                return NotFound();
            }

            return View(jozeyatHotel);
        }

        // GET: JozeyatHotels/Create
        public IActionResult Create()
        {
            ViewData["Jozeyat_HotelID"] = new SelectList(_context.hotels, "Hotel_ID", "Name_Hotel");
            ViewData["Jozeyat_TedadStareID"] = new SelectList(_context.tedadStarehs, "TedadStareh_ID", "TedadStareh_Name");
            ViewData["Jozeyat_TedadTakhtID"] = new SelectList(_context.tedadTakhtHotels, "TedadTakh_ID", "TedadTakh_Name");
            ViewData["Jozeyat_ZarfiatOtaghID"] = new SelectList(_context.zarfyatOtaghHotels, "ZarfyatOtagh_ID", "ZarfyatOtagh_Name");
            return View();
        }

        // POST: JozeyatHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Jozeyat_ID,Jozeyat_HotelID,Jozeyat_ZarfiatOtaghID,Jozeyat_TedadTakhtID,Jozeyat_TedadStareID,Jozeyat_Teadad")] JozeyatHotel jozeyatHotel)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(jozeyatHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["Jozeyat_HotelID"] = new SelectList(_context.hotels, "Hotel_ID", "Name_Hotel", jozeyatHotel.Jozeyat_HotelID);
            //ViewData["Jozeyat_TedadStareID"] = new SelectList(_context.tedadStarehs, "TedadStareh_ID", "TedadStareh_Name", jozeyatHotel.Jozeyat_TedadStareID);
            //ViewData["Jozeyat_TedadTakhtID"] = new SelectList(_context.tedadTakhtHotels, "TedadTakh_ID", "TedadTakh_Name", jozeyatHotel.Jozeyat_TedadTakhtID);
            //ViewData["Jozeyat_ZarfiatOtaghID"] = new SelectList(_context.zarfyatOtaghHotels, "ZarfyatOtagh_ID", "ZarfyatOtagh_Name", jozeyatHotel.Jozeyat_ZarfiatOtaghID);
            //return View(jozeyatHotel);
        }

        // GET: JozeyatHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jozeyatHotel = await _context.jozeyatHotels.FindAsync(id);
            if (jozeyatHotel == null)
            {
                return NotFound();
            }
            ViewData["Jozeyat_HotelID"] = new SelectList(_context.hotels, "Hotel_ID", "Name_Hotel", jozeyatHotel.Jozeyat_HotelID);
            ViewData["Jozeyat_TedadStareID"] = new SelectList(_context.tedadStarehs, "TedadStareh_ID", "TedadStareh_Name", jozeyatHotel.Jozeyat_TedadStareID);
            ViewData["Jozeyat_TedadTakhtID"] = new SelectList(_context.tedadTakhtHotels, "TedadTakh_ID", "TedadTakh_Name", jozeyatHotel.Jozeyat_TedadTakhtID);
            ViewData["Jozeyat_ZarfiatOtaghID"] = new SelectList(_context.zarfyatOtaghHotels, "ZarfyatOtagh_ID", "ZarfyatOtagh_Name", jozeyatHotel.Jozeyat_ZarfiatOtaghID);
            return View(jozeyatHotel);
        }

        // POST: JozeyatHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Jozeyat_ID,Jozeyat_HotelID,Jozeyat_ZarfiatOtaghID,Jozeyat_TedadTakhtID,Jozeyat_TedadStareID,Jozeyat_Teadad")] JozeyatHotel jozeyatHotel)
        {
            if (id != jozeyatHotel.Jozeyat_ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(jozeyatHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JozeyatHotelExists(jozeyatHotel.Jozeyat_ID))
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
            //ViewData["Jozeyat_HotelID"] = new SelectList(_context.hotels, "Hotel_ID", "Name_Hotel", jozeyatHotel.Jozeyat_HotelID);
            //ViewData["Jozeyat_TedadStareID"] = new SelectList(_context.tedadStarehs, "TedadStareh_ID", "TedadStareh_Name", jozeyatHotel.Jozeyat_TedadStareID);
            //ViewData["Jozeyat_TedadTakhtID"] = new SelectList(_context.tedadTakhtHotels, "TedadTakh_ID", "TedadTakh_Name", jozeyatHotel.Jozeyat_TedadTakhtID);
            //ViewData["Jozeyat_ZarfiatOtaghID"] = new SelectList(_context.zarfyatOtaghHotels, "ZarfyatOtagh_ID", "ZarfyatOtagh_Name", jozeyatHotel.Jozeyat_ZarfiatOtaghID);
            //return View(jozeyatHotel);
        }

        // GET: JozeyatHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jozeyatHotel = await _context.jozeyatHotels
                .Include(j => j.Hoteljoz)
                .Include(j => j.TedadStareh)
                .Include(j => j.TedadTakhtHotel)
                .Include(j => j.ZarfyatOtagh)
                .FirstOrDefaultAsync(m => m.Jozeyat_ID == id);
            if (jozeyatHotel == null)
            {
                return NotFound();
            }

            return View(jozeyatHotel);
        }

        // POST: JozeyatHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jozeyatHotel = await _context.jozeyatHotels.FindAsync(id);
            _context.jozeyatHotels.Remove(jozeyatHotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JozeyatHotelExists(int id)
        {
            return _context.jozeyatHotels.Any(e => e.Jozeyat_ID == id);
        }
    }
}
