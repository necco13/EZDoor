using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class LandlordController : Controller
    {
        private readonly EZdb _context;

        public LandlordController(EZdb context)
        {
            _context = context;
        }

        // GET: Landlord
        public async Task<IActionResult> Index()
        {
            return View(await _context.Landlords.ToListAsync());
        }

        // GET: Landlord/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landlord = await _context.Landlords
                .FirstOrDefaultAsync(m => m.ID == id);
            if (landlord == null)
            {
                return NotFound();
            }

            return View(landlord);
        }

        // GET: Landlord/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Landlord/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Surname,EMail,Password")] Landlord landlord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(landlord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(landlord);
        }

        // GET: Landlord/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landlord = await _context.Landlords.FindAsync(id);
            if (landlord == null)
            {
                return NotFound();
            }
            return View(landlord);
        }

        // POST: Landlord/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Surname,EMail,Password")] Landlord landlord)
        {
            if (id != landlord.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(landlord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandlordExists(landlord.ID))
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
            return View(landlord);
        }

        // GET: Landlord/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landlord = await _context.Landlords
                .FirstOrDefaultAsync(m => m.ID == id);
            if (landlord == null)
            {
                return NotFound();
            }

            return View(landlord);
        }

        // POST: Landlord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var landlord = await _context.Landlords.FindAsync(id);
            if (landlord != null)
            {
                _context.Landlords.Remove(landlord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LandlordExists(int id)
        {
            return _context.Landlords.Any(e => e.ID == id);
        }
    }
}
