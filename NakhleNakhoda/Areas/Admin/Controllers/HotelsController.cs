using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Models.DB;

namespace NakhleNakhoda.Controllers
{
    [Authorize/*(Roles = "Admin")*/]
    [Area("Admin")]
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext db;

        public HotelsController(ApplicationDbContext context)
        {
            db = context;
        }
        [HttpGet("Hotels/Index")]
        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            return View(await db.hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await db.hotels
                .FirstOrDefaultAsync(m => m.Hotel_ID == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Hotel_ID,Name_Hotel,Adres,Jozeyat_Gheymat,ZamanShoroa,ZamanPayan,Faal,Tozihat")] Hotels hotels)
        {
            //if (ModelState.IsValid)
            //{
                db.Add(hotels);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(hotels);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await db.hotels.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Hotel_ID,Name_Hotel,Adres,Jozeyat_Gheymat,ZamanShoroa,ZamanPayan,Faal,Tozihat")] Hotels hotels)
        {
            if (id != hotels.Hotel_ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    db.Update(hotels);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsExists(hotels.Hotel_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                //}
                //return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await db.hotels
                .FirstOrDefaultAsync(m => m.Hotel_ID == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotels = await db.hotels.FindAsync(id);
            db.hotels.Remove(hotels);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task< IActionResult> Faal(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotels = await db.hotels.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }

            if (hotels.Faal ==true)
            {
                hotels.Faal = false;
                db.hotels.Update(hotels);
                await db.SaveChangesAsync();
            }
            else
            {
                hotels.Faal = true;
                db.hotels.Update(hotels);
                await db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(int id)
        {
            return db.hotels.Any(e => e.Hotel_ID == id);
        }
    }
}
