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
    public class JobController : Controller
    {
        private readonly lunaContext _context;

        public JobController(lunaContext context)
        {
            _context = context;
        }

        // GET: Job
        public async Task<IActionResult> Index(long Id)
        {
            ViewBag.OEPVisaDemandId = Id;
            var lunaContext = _context.OepvisaDemandDetail.Where(t=>t.OepvisaDemandId==Id)
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemand)
                .Include(o => o.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: Job/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oepvisaDemandDetail = await _context.OepvisaDemandDetail
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemand)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oepvisaDemandDetail == null)
            {
                return NotFound();
            }

            return View(oepvisaDemandDetail);
        }

        // GET: Job/Create
        public IActionResult Create(long OEPVisaDemandId)
        {
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name");
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name");
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name");
            var model = new OepvisaDemandDetail();
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 5), "Id", "Name");
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber",OEPVisaDemandId);

            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name");
            return View(model);
        }

        // POST: Job/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OepvisaDemandId,JobTypeEntitySetupId,Quantity,CountryId,StateId,CityId,MinSalary,MaxSalary,OfferExpiryDate,ContractDuration,BenefitsNeedToDiscuss,IsAccommodation,AccommodationDetail,IsMedical,MedicalDetail,IsTransport,TransportDetail,IsAirTicket,AirTicketDetail,IsReturnTicket,ReturnTicketDetail,IsOverTime,OverTimeDetail,IsFood,FoodDetail,IsOthers,OthersDetail")] OepvisaDemandDetail oepvisaDemandDetail)
        {
            var totalQuantity = _context.OepvisaDemand.Find(oepvisaDemandDetail.OepvisaDemandId);
            var totalConsumed = _context.OepvisaDemandDetail.Where(t => t.OepvisaDemandId == oepvisaDemandDetail.OepvisaDemandId);
            int totalConsumedValue = 0;
            if (totalConsumed != null)
            {
                totalConsumedValue = totalConsumed.Sum(t => t.Quantity).Value;

            }
            if (totalQuantity != null)
            {
                if ((totalConsumedValue + oepvisaDemandDetail.Quantity) > totalQuantity.Quantity)
                {
                    ModelState.AddModelError("Quantity", "Job quantity exceeds the total demand quantity.");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(oepvisaDemandDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Job",new { Id= oepvisaDemandDetail .OepvisaDemandId});
            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", oepvisaDemandDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", oepvisaDemandDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", oepvisaDemandDetail.CityId);

            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", oepvisaDemandDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", oepvisaDemandDetail.OepvisaDemandId);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemandDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oepvisaDemandDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oepvisaDemandDetail.CityId);
            return View(oepvisaDemandDetail);
        }

        // GET: Job/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var oepvisaDemandDetail = await _context.OepvisaDemandDetail.FindAsync(id);
            var oepvisaDemandDetail = await _context.OepvisaDemandDetail.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (oepvisaDemandDetail == null)
            {
                return NotFound();
            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", oepvisaDemandDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", oepvisaDemandDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", oepvisaDemandDetail.CityId);
            
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", oepvisaDemandDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", oepvisaDemandDetail.OepvisaDemandId);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemandDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oepvisaDemandDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oepvisaDemandDetail.CityId);
            return View(oepvisaDemandDetail);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OepvisaDemandId,JobTypeEntitySetupId,Quantity,CountryId,StateId,CityId,MinSalary,MaxSalary,OfferExpiryDate,ContractDuration,BenefitsNeedToDiscuss,IsAccommodation,AccommodationDetail,IsMedical,MedicalDetail,IsTransport,TransportDetail,IsAirTicket,AirTicketDetail,IsReturnTicket,ReturnTicketDetail,IsOverTime,OverTimeDetail,IsFood,FoodDetail,IsOthers,OthersDetail")] OepvisaDemandDetail oepvisaDemandDetail)
        {
            if (id != oepvisaDemandDetail.Id)
            {
                return NotFound();
            }

            //TODO: GET ERROR
            var demandDetail = await _context.OepvisaDemandDetail.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            var totalQuantity = await _context.OepvisaDemand.AsNoTracking().FirstOrDefaultAsync(od => od.Id == oepvisaDemandDetail.OepvisaDemandId);
            var totalConsumed =  _context.OepvisaDemandDetail.AsNoTracking().Where(t => t.OepvisaDemandId == oepvisaDemandDetail.OepvisaDemandId);

            int totalConsumedValue = 0;
            if (totalConsumed != null)
            {
                totalConsumedValue = totalConsumed.Sum(t => t.Quantity).Value;
            }
            if (totalQuantity != null)
            {
                if (demandDetail.Quantity!=oepvisaDemandDetail.Quantity && ((totalConsumedValue + oepvisaDemandDetail.Quantity) > totalQuantity.Quantity))
                {
                    ModelState.AddModelError("Quantity", "Job quantity exceeds the total demand quantity.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oepvisaDemandDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OepvisaDemandDetailExists(oepvisaDemandDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                };
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Job", new { Id = oepvisaDemandDetail.OepvisaDemandId });

            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", oepvisaDemandDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", oepvisaDemandDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", oepvisaDemandDetail.CityId);

            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", oepvisaDemandDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "BatchNumber", oepvisaDemandDetail.OepvisaDemandId);

            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemandDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", oepvisaDemandDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oepvisaDemandDetail.CityId);
            return View(oepvisaDemandDetail);
        }

        // GET: Job/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oepvisaDemandDetail = await _context.OepvisaDemandDetail
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemand)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oepvisaDemandDetail == null)
            {
                return NotFound();
            }

            return View(oepvisaDemandDetail);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var oepvisaDemandDetail = await _context.OepvisaDemandDetail.FindAsync(id);
            _context.OepvisaDemandDetail.Remove(oepvisaDemandDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = oepvisaDemandDetail.OepvisaDemandId });
        }

        private bool OepvisaDemandDetailExists(long id)
        {
            return _context.OepvisaDemandDetail.Any(e => e.Id == id);
        }
    }
}
