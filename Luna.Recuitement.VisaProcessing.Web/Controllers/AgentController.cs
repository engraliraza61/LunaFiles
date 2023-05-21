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
using Luna.Recruitment.VisaProcessing.DTO;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AgentController : Controller
    {
        private readonly lunaContext _context;
        private readonly ILoggerManager _logger;
        public AgentController(lunaContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Agent
        public async Task<IActionResult> Index()
        {
            _logger.LogInfo($"{DateTime.UtcNow.ToString()}");
            var lunaContext = _context.Agent.Include(c => c.City).Include(c => c.Country).Include(c => c.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: Agent/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Agent = await _context.Agent
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Agent == null)
            {
                return NotFound();
            }

            return View(Agent);
        }

        // GET: Agent/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name").Take(100).ToList();
            return View();
        }

        // POST: Agent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,ArabicName,Address,Phone,Phone2,Email,AgentGLCode,CountryId,StateId,CityId,IsActive,IsDeleted")] Agent Agent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", Agent.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", Agent.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", Agent.CityId);
            return View(Agent);
        }

        // GET: Agent/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Agent = await _context.Agent.FindAsync(id);
            if (Agent == null)
            {
                return NotFound();
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", Agent.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", Agent.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", Agent.CityId);
            return View(Agent);
        }

        // POST: Agent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,ArabicName,Address,Phone,Phone2,Email,AgentGLCode,CountryId,StateId,CityId,IsActive,IsDeleted")] Agent Agent)
        {
            if (id != Agent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(Agent.Id))
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
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", Agent.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", Agent.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", Agent.CityId);
            return View(Agent);
        }

        // GET: Agent/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Agent = await _context.Agent
                .Include(c => c.City)
                .Include(c => c.Country)
                .Include(c => c.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Agent == null)
            {
                return NotFound();
            }

            return View(Agent);
        }

        // POST: Agent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var Agent = await _context.Agent.FindAsync(id);
            _context.Agent.Remove(Agent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(long id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }
        public JsonResult GetAgent()
        {
            var agent = _context.Agent.Where(e => e.Name != null).ToList();
            return Json(agent);
        }
        public JsonResult GetFileNoByAgentId(int agentId)
        {
            var candidate = _context.CandidateProfile.Where(c => c.AgentId == agentId).ToList();
            return Json(candidate);

        }
        public JsonResult GetDataByPassportId(int passportId)
        {
            var passport = _context.Passport.Where(c => c.Id == passportId).FirstOrDefault();
            var candidate = _context.CandidateProfile.Where(c => c.Id == passport.CandidateProfileId).FirstOrDefault();
            var receipt=_context.Receipt.Where(c => c.CandidateProfileId == passport.CandidateProfileId).FirstOrDefault();
            ReceiptAgentJV agentJV = new ReceiptAgentJV();
            agentJV.CandidateName = candidate.FirstName + candidate.LastName ;
            if (receipt != null)
            {

                agentJV.TotalReceivable = receipt.Amount;
            }
            else
            {

                agentJV.TotalReceivable = 0;
            }
            return Json(agentJV);
        }
    }
}
