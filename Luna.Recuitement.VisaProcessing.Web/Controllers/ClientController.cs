using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Luna.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientController : Controller
    {
        private readonly lunaContext _context;
        private readonly ILoggerManager _logger;

        public ClientController(lunaContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Client
        public async Task<IActionResult> Index()
        {
            _logger.LogInfo($"{DateTime.UtcNow.ToString()}");
            var lunaContext = _context.Client.Include(c => c.City).Include(c => c.Country).Include(c => c.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", client.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", client.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", client.CityId);
            return View(client);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", client.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", client.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", client.CityId);
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", client.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", client.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", client.CityId);
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(long id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
