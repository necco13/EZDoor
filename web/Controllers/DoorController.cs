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
    public class DoorController : Controller
    {
        private readonly EZdb _context;

        public DoorController(EZdb context)
        {
            _context = context;
        }

        // GET: Door
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doors.ToListAsync());
        }

        // GET: Door/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // GET: Door/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Door/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Notes")] Door door)
        {
            if (ModelState.IsValid)
            {
                _context.Add(door);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(door);
        }

        // GET: Door/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors.FindAsync(id);
            if (door == null)
            {
                return NotFound();
            }
            return View(door);
        }

        // POST: Door/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Notes")] Door door)
        {
            if (id != door.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(door);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoorExists(door.ID))
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
            return View(door);
        }

        // GET: Door/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var door = await _context.Doors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (door == null)
            {
                return NotFound();
            }

            return View(door);
        }

        // POST: Door/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var door = await _context.Doors.FindAsync(id);
            if (door != null)
            {
                _context.Doors.Remove(door);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoorExists(int id)
        {
            return _context.Doors.Any(e => e.ID == id);
        }
    }
}
