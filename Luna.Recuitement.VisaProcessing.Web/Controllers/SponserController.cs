using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SponserController : Controller
    {
        private readonly lunaContext _context;

        public SponserController(lunaContext context)
        {
            _context = context;
        }

        // GET: Sponser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sponser.ToListAsync());
        }

        // GET: Sponser/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponser = await _context.Sponser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponser == null)
            {
                return NotFound();
            }

            return View(sponser);
        }

        // GET: Sponser/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();

            return View();
        }

        // POST: Sponser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sponser sponser)
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();

            if (!CheckUniqueSponserID(sponser))
            {
                ModelState.AddModelError("Code", "SponserID already exist.");
            }
            if (ModelState.IsValid)
            {
                _context.Add(sponser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sponser);
        }

        // GET: Sponser/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();

            if (id == null)
            {
                return NotFound();
            }

            var sponser = await _context.Sponser.FindAsync(id);
            if (sponser == null)
            {
                return NotFound();
            }
            return View(sponser);
        }

        // POST: Sponser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Sponser sponser)
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();
            if (!CheckUniqueSponserID(sponser))
            {
                ModelState.AddModelError("Code", "SponserID already exist.");
            }
            if (id != sponser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sponser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SponserExists(sponser.Id))
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
            return View(sponser);
        }

        // GET: Sponser/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sponser = await _context.Sponser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sponser == null)
            {
                return NotFound();
            }

            return View(sponser);
        }

        // POST: Sponser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var sponser = await _context.Sponser.FindAsync(id);
            _context.Sponser.Remove(sponser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SponserExists(long id)
        {
            return _context.Sponser.Any(e => e.Id == id);
        }

        private bool CheckUniqueSponserID(Sponser sponser)
        {
            bool status = false;
            var spon = _context.Sponser.Where(t => t.Code == sponser.Code && t.Id != sponser.Id);
            if (spon.Count() > 0) 
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }
    }
}
