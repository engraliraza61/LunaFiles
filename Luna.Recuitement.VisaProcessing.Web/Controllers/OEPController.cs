using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class OEPController : Controller
    {
        private readonly lunaContext _context;

        public OEPController(lunaContext context)
        {
            _context = context;
        }

        // GET: OEP
        public async Task<IActionResult> Index()
        {
            var lunaContext = _context.Oep.Include(o => o.City).Include(o => o.Country).Include(o => o.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: OEP/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oep = await _context.Oep
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oep == null)
            {
                return NotFound();
            }

            return View(oep);
        }

        // GET: OEP/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name");
            return View();
        }

        // POST: OEP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] Oep oep)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oep.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oep.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oep.CityId);
            return View(oep);
        }

        // GET: OEP/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oep = await _context.Oep.FindAsync(id);
            if (oep == null)
            {
                return NotFound();
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oep.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oep.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oep.CityId);
            return View(oep);
        }

        // POST: OEP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] Oep oep)
        {
            if (id != oep.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OepExists(oep.Id))
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
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oep.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oep.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oep.CityId);
            return View(oep);
        }

        // GET: OEP/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oep = await _context.Oep
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oep == null)
            {
                return NotFound();
            }

            return View(oep);
        }

        // POST: OEP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var oep = await _context.Oep.FindAsync(id);
            _context.Oep.Remove(oep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OepExists(long id)
        {
            return _context.Oep.Any(e => e.Id == id);
        }
    }
}
