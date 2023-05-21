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
    public class JobPOController : Controller
    {
        private readonly lunaContext _context;

        public JobPOController(lunaContext context)
        {
            _context = context;
        }

        // GET: JobPO
        public async Task<IActionResult> Index(long Id)
        {
            ViewBag.OepvisaDemandPoId = Id;
            var lunaContext = _context.OepvisaDemandPodetail.Where(t=>t.OepvisaDemandPoid==Id)
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemandPo)
                .Include(o => o.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: JobPO/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OepvisaDemandPoDetail = await _context.OepvisaDemandPodetail
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemandPo)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (OepvisaDemandPoDetail == null)
            {
                return NotFound();
            }

            return View(OepvisaDemandPoDetail);
        }

        // GET: JobPO/Create
        public IActionResult Create(long OepvisaDemandPoId)
        {
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name");
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name");
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name");
            var model = new OepvisaDemandPodetail();
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityTypeId == 5), "Id", "Name");
            ViewData["OepvisaDemandPoId"] = new SelectList(_context.OepvisaDemandPo, "Id", "BatchNumber",OepvisaDemandPoId);

            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name");
            return View(model);
        }

        // POST: JobPO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OepvisaDemandPoId,JobTypeEntitySetupId,Quantity,CountryId,StateId,CityId,MinSalary,MaxSalary,OfferExpiryDate,ContractDuration,BenefitsNeedToDiscuss,IsAccommodation,AccommodationDetail,IsMedical,MedicalDetail,IsTransport,TransportDetail,IsAirTicket,AirTicketDetail,IsReturnTicket,ReturnTicketDetail,IsOverTime,OverTimeDetail,IsFood,FoodDetail,IsOthers,OthersDetail")] OepvisaDemandPodetail OepvisaDemandPoDetail)
        {
            var totalQuantity = _context.OepvisaDemandPo.Find(OepvisaDemandPoDetail.OepvisaDemandPoid);
            var totalConsumed = _context.OepvisaDemandPodetail.Where(t => t.OepvisaDemandPoid == OepvisaDemandPoDetail.OepvisaDemandPoid);
            int totalConsumedValue = 0;
            if (totalConsumed != null)
            {
                totalConsumedValue = totalConsumed.Sum(t => t.Quantity).Value;

            }
            if (totalQuantity != null)
            {
                if ((totalConsumedValue + OepvisaDemandPoDetail.Quantity) > totalQuantity.Quantity)
                {
                    ModelState.AddModelError("Quantity", "JobPO quantity exceeds the total demand quantity.");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(OepvisaDemandPoDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","JobPO",new { Id= OepvisaDemandPoDetail .OepvisaDemandPoid});
            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", OepvisaDemandPoDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", OepvisaDemandPoDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", OepvisaDemandPoDetail.CityId);

            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", OepvisaDemandPoDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandPoId"] = new SelectList(_context.OepvisaDemandPo, "Id", "BatchNumber", OepvisaDemandPoDetail.OepvisaDemandPoid);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPoDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", OepvisaDemandPoDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", OepvisaDemandPoDetail.CityId);
            return View(OepvisaDemandPoDetail);
        }

        // GET: JobPO/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var OepvisaDemandPoDetail = await _context.OepvisaDemandPoDetail.FindAsync(id);
            var OepvisaDemandPoDetail = await _context.OepvisaDemandPodetail.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (OepvisaDemandPoDetail == null)
            {
                return NotFound();
            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", OepvisaDemandPoDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", OepvisaDemandPoDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", OepvisaDemandPoDetail.CityId);
            
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", OepvisaDemandPoDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandPoId"] = new SelectList(_context.OepvisaDemandPo, "Id", "BatchNumber", OepvisaDemandPoDetail.OepvisaDemandPoid);
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPoDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", OepvisaDemandPoDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", OepvisaDemandPoDetail.CityId);
            return View(OepvisaDemandPoDetail);
        }

        // POST: JobPO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,OepvisaDemandPoId,JobTypeEntitySetupId,Quantity,CountryId,StateId,CityId,MinSalary,MaxSalary,OfferExpiryDate,ContractDuration,BenefitsNeedToDiscuss,IsAccommodation,AccommodationDetail,IsMedical,MedicalDetail,IsTransport,TransportDetail,IsAirTicket,AirTicketDetail,IsReturnTicket,ReturnTicketDetail,IsOverTime,OverTimeDetail,IsFood,FoodDetail,IsOthers,OthersDetail")] OepvisaDemandPodetail OepvisaDemandPoDetail)
        {
            if (id != OepvisaDemandPoDetail.Id)
            {
                return NotFound();
            }

            //TODO: GET ERROR
            var demandDetail = await _context.OepvisaDemandPodetail.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);

            var totalQuantity = await _context.OepvisaDemandPo.AsNoTracking().FirstOrDefaultAsync(od => od.Id == OepvisaDemandPoDetail.OepvisaDemandPoid);
            var totalConsumed =  _context.OepvisaDemandPodetail.AsNoTracking().Where(t => t.OepvisaDemandPoid == OepvisaDemandPoDetail.OepvisaDemandPoid);

            int totalConsumedValue = 0;
            if (totalConsumed != null)
            {
                totalConsumedValue = totalConsumed.Sum(t => t.Quantity).Value;
            }
            if (totalQuantity != null)
            {
                if (demandDetail.Quantity!=OepvisaDemandPoDetail.Quantity && ((totalConsumedValue + OepvisaDemandPoDetail.Quantity) > totalQuantity.Quantity))
                {
                    ModelState.AddModelError("Quantity", "JobPO quantity exceeds the total demand quantity.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(OepvisaDemandPoDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OepvisaDemandPoDetailExists(OepvisaDemandPoDetail.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                };
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "JobPO", new { Id = OepvisaDemandPoDetail.OepvisaDemandPoid });

            }
            //ViewData["CountryId"] = new SelectList(_context.Country.Take(10), "Id", "Name", OepvisaDemandPoDetail.CountryId);
            //ViewData["StateId"] = new SelectList(_context.State.Take(10), "Id", "Name", OepvisaDemandPoDetail.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Take(10), "Id", "Name", OepvisaDemandPoDetail.CityId);

            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityTypeId==5), "Id", "Name", OepvisaDemandPoDetail.JobTypeEntitySetupId);
            ViewData["OepvisaDemandPoId"] = new SelectList(_context.OepvisaDemandPo, "Id", "BatchNumber", OepvisaDemandPoDetail.OepvisaDemandPoid);

            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPoDetail.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", OepvisaDemandPoDetail.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", OepvisaDemandPoDetail.CityId);
            return View(OepvisaDemandPoDetail);
        }

        // GET: JobPO/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OepvisaDemandPoDetail = await _context.OepvisaDemandPodetail
                .Include(o => o.City)
                .Include(o => o.Country)
                .Include(o => o.JobTypeEntitySetup)
                .Include(o => o.OepvisaDemandPo)
                .Include(o => o.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (OepvisaDemandPoDetail == null)
            {
                return NotFound();
            }

            return View(OepvisaDemandPoDetail);
        }

        // POST: JobPO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var OepvisaDemandPoDetail = await _context.OepvisaDemandPodetail.FindAsync(id);
            _context.OepvisaDemandPodetail.Remove(OepvisaDemandPoDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { Id = OepvisaDemandPoDetail.OepvisaDemandPoid });
        }

        private bool OepvisaDemandPoDetailExists(long id)
        {
            return _context.OepvisaDemandPodetail.Any(e => e.Id == id);
        }
    }
}
