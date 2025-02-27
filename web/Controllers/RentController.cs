using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace web.Controllers
{
    [Authorize]
    public class RentController : Controller
    {
        private readonly Azuredb _context;

        public RentController(Azuredb context)
        {
            _context = context;
        }

        // GET: Rent
        public async Task<IActionResult> Index()
        {
            var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(_context.UserRoles.Where(r => r.UserId == userId && r.RoleId == "0").Any()){
                return View(await _context.Rent.Include(r => r.Property).ToListAsync());//admin loh dela kr hoce
            }
            return View(await _context.Rent.Include(r => r.Property).Where(r => r.Property.Landlord.Id == userId).ToListAsync());
            // _context.Properties.Where(p => p.Landlord.Id == userId).ToListAsync()
        }

        // GET: Rent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rent/Create
public IActionResult Create()
{
    var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
    // Populate dropdown for Users
    ViewBag.Users = new SelectList(_context.Users, "Id", "UserName");

    // Populate dropdown for Properties
    ViewBag.Properties = new SelectList(_context.Properties.Where(p => p.Landlord.Id == userId), "ID", "Name");

    return View();
}

        // POST: Rent/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Start,End,User,Property")] Rent rent)
        {
            Property property = await _context.Properties.FindAsync(rent.Property.ID);
            rent.Property = property;

            User user = await _context.Users.FindAsync(rent.User.Id);
            rent.User = user;
                _context.Add(rent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // POST: Rent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Start,End")] Rent rent)
        {
            if (id != rent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.ID))
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
            return View(rent);
        }

        // GET: Rent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rent = await _context.Rent
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rent = await _context.Rent.FindAsync(id);
            if (rent != null)
            {
                _context.Rent.Remove(rent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentExists(int id)
        {
            return _context.Rent.Any(e => e.ID == id);
        }
    }
}
