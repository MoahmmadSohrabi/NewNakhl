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
    public class TedadTakhtHotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TedadTakhtHotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TedadTakhtHotels
        public async Task<IActionResult> Index()
        {
            return View(await _context.tedadTakhtHotels.ToListAsync());
        }

        // GET: TedadTakhtHotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadTakhtHotel = await _context.tedadTakhtHotels
                .FirstOrDefaultAsync(m => m.TedadTakh_ID == id);
            if (tedadTakhtHotel == null)
            {
                return NotFound();
            }

            return View(tedadTakhtHotel);
        }

        // GET: TedadTakhtHotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TedadTakhtHotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedadTakh_ID,TedadTakh_Name")] TedadTakhtHotel tedadTakhtHotel)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(tedadTakhtHotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(tedadTakhtHotel);
        }

        // GET: TedadTakhtHotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadTakhtHotel = await _context.tedadTakhtHotels.FindAsync(id);
            if (tedadTakhtHotel == null)
            {
                return NotFound();
            }
            return View(tedadTakhtHotel);
        }

        // POST: TedadTakhtHotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedadTakh_ID,TedadTakh_Name")] TedadTakhtHotel tedadTakhtHotel)
        {
            if (id != tedadTakhtHotel.TedadTakh_ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tedadTakhtHotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedadTakhtHotelExists(tedadTakhtHotel.TedadTakh_ID))
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
            //return View(tedadTakhtHotel);
        }

        // GET: TedadTakhtHotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadTakhtHotel = await _context.tedadTakhtHotels
                .FirstOrDefaultAsync(m => m.TedadTakh_ID == id);
            if (tedadTakhtHotel == null)
            {
                return NotFound();
            }

            return View(tedadTakhtHotel);
        }

        // POST: TedadTakhtHotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tedadTakhtHotel = await _context.tedadTakhtHotels.FindAsync(id);
            _context.tedadTakhtHotels.Remove(tedadTakhtHotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedadTakhtHotelExists(int id)
        {
            return _context.tedadTakhtHotels.Any(e => e.TedadTakh_ID == id);
        }
    }
}
