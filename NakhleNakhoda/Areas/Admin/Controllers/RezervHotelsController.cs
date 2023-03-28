using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Data;
using NakhleNakhoda.Data.Models.DB;

namespace NakhleNakhoda.Web.Areas.Admin.Controllers
{
    [Authorize/*(Roles = "Admin")*/]
    [Area("Admin")]
    public class RezervHotelsController : Controller
    {
        
        private readonly ApplicationDbContext db;
        public RezervHotelsController(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.rezervHotels.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await db.rezervHotels
                .FirstOrDefaultAsync(m => m.Rezerv_ID == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezerv = await db.rezervHotels
                .FirstOrDefaultAsync(m => m.Rezerv_ID == id);
            if (rezerv
                == null)
            {
                return NotFound();
            }

            return View(rezerv);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezervs = await db.rezervHotels.FindAsync(id);
            db.rezervHotels.Remove(rezervs);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
