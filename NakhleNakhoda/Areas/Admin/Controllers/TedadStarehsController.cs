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
    public class TedadStarehsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TedadStarehsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TedadStarehs
        public async Task<IActionResult> Index()
        {
            return View(await _context.tedadStarehs.ToListAsync());
        }

        // GET: TedadStarehs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadStareh = await _context.tedadStarehs
                .FirstOrDefaultAsync(m => m.TedadStareh_ID == id);
            if (tedadStareh == null)
            {
                return NotFound();
            }

            return View(tedadStareh);
        }

        // GET: TedadStarehs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TedadStarehs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TedadStareh_ID,TedadStareh_Name")] TedadStareh tedadStareh)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(tedadStareh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //return View(tedadStareh);
        }

        // GET: TedadStarehs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadStareh = await _context.tedadStarehs.FindAsync(id);
            if (tedadStareh == null)
            {
                return NotFound();
            }
            return View(tedadStareh);
        }

        // POST: TedadStarehs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TedadStareh_ID,TedadStareh_Name")] TedadStareh tedadStareh)
        {
            if (id != tedadStareh.TedadStareh_ID)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(tedadStareh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TedadStarehExists(tedadStareh.TedadStareh_ID))
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
            //return View(tedadStareh);
        }

        // GET: TedadStarehs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tedadStareh = await _context.tedadStarehs
                .FirstOrDefaultAsync(m => m.TedadStareh_ID == id);
            if (tedadStareh == null)
            {
                return NotFound();
            }

            return View(tedadStareh);
        }

        // POST: TedadStarehs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tedadStareh = await _context.tedadStarehs.FindAsync(id);
            _context.tedadStarehs.Remove(tedadStareh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TedadStarehExists(int id)
        {
            return _context.tedadStarehs.Any(e => e.TedadStareh_ID == id);
        }
    }
}
