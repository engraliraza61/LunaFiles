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
    public class CounslateController : Controller
    {
        private readonly lunaContext _context;

        public CounslateController(lunaContext context)
        {
            _context = context;
        }

        // GET: Counslates
        public async Task<IActionResult> Index()
        {
            string outputTyepe = "1";
            var counslateList = _context.Counslate
                                .Include(c => c.City)
                                .Include(c => c.Country)
                                .Include(c => c.State);
            if (outputTyepe == "1")
                return View(counslateList.ToList());
            else
            {
                JsonResult jsonResult = Json(counslateList.ToList());
                return jsonResult;
            }
        }
        
        // GET: Counslates/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counslate = await _context.Counslate
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counslate == null)
            {
                return NotFound();
            }

            return View(counslate);
        }

        // GET: Counslates/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t=>t.Id).ToArray().Contains(t.StateId)), "Id", "Name");

            return View();
        }

        // POST: Counslates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Address,Phone,CountryId,StateId,CityId")] Counslate counslate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(counslate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", counslate.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", counslate.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name",counslate.CityId);
            return View(counslate);
        }

        // GET: Counslates/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counslate = await _context.Counslate.FindAsync(id);
            if (counslate == null)
            {
                return NotFound();
            }
            //ViewData["CityId"] = new SelectList(_context.City.Take(50), "Id", "Name", counslate.CityId);
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(50), "Id", "Code", counslate.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(50), "Id", "Name", counslate.StateId);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", counslate.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId));
            ViewData["StateId"] = new SelectList(states, "Id", "Name", counslate.StateId).ToList();
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", counslate.CityId);
            return View(counslate);
        }

        // POST: Counslates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,Address,Phone,CountryId,StateId,CityId")] Counslate counslate)
        {
            if (id != counslate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counslate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounslateExists(counslate.Id))
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
            //ViewData["CityId"] = new SelectList(_context.City.Take(50), "Id", "Name", counslate.CityId);
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(50), "Id", "Code", counslate.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(50), "Id", "Name", counslate.StateId);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", counslate.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", counslate.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", counslate.CityId);
            return View(counslate);
        }

        // GET: Counslates/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var counslate = await _context.Counslate
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (counslate == null)
            {
                return NotFound();
            }

            return View(counslate);
        }

        // POST: Counslates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var counslate = await _context.Counslate.FindAsync(id);
            _context.Counslate.Remove(counslate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounslateExists(long id)
        {
            return _context.Counslate.Any(e => e.Id == id);
        }
        public JsonResult GetCounslateByCountry(long CountryId = 2)
        {
            List<Counslate> counslates = new List<Counslate>();
            if (CountryId == 0)
            {
                counslates = null;
            }
            else
            {
                counslates = _context.Counslate.Where(s => s.CountryId == CountryId).ToList();
            }
            return Json(counslates);
        }
    }
}
