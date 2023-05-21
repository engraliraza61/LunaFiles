using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Luna.Recruitment.VisaProcessing.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class OepvisaDemandPOController : Controller
    {
        private readonly lunaContext _context;

        public OepvisaDemandPOController(lunaContext context)
        {
            _context = context;
        }

        // GET: OepvisaDemandPo
        //[Authorize(Roles = "Administrator,HR")]
        public async Task<IActionResult> Index()
        {
            
            var lunaContext = _context.OepvisaDemandPo
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandPostatusEntitySetup)
                .Include(o=>o.Sponser)
                .Include(o=>o.OepvisaDemandPodetail);
            return View(await lunaContext.ToListAsync());
        }

        // GET: OepvisaDemandPo/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OepvisaDemandPo = await _context.OepvisaDemandPo
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandPostatusEntitySetup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (OepvisaDemandPo == null)
            {
                return NotFound();
            }

            return View(OepvisaDemandPo);
        }

        // GET: OepvisaDemandPo/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name");
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Country.Where(t=>t.IsActive==true), "Id", "Name");
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name");
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["Modes"] = new SelectList(AppLists.Modes, "Value", "Text");

            ViewData["OepvisaDemandPoStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t=>t.EntityType.Name==Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name");
            return View();
        }

        // POST: OepvisaDemandPo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OepvisaDemandPo OepvisaDemandPo)
        {
            if (ModelState.IsValid)
            {
                var nextSequence = GetNextSequence(OepvisaDemandPo.Oepid.Value);
                OepvisaDemandPo.Code = nextSequence;
                _context.Add(OepvisaDemandPo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit","OepvisaDemandPo",new {id=OepvisaDemandPo.Id });
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name",OepvisaDemandPo.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name",OepvisaDemandPo.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name",OepvisaDemandPo.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPo.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name",OepvisaDemandPo.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["Modes"] = new SelectList(AppLists.Modes, "Value", "Text");

            ViewData["OepvisaDemandPoStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name",OepvisaDemandPo.OepvisaDemandPostatusEntitySetupId);
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE), "Id", "Name", OepvisaDemandPo.OepvisaDemandPostatusEntitySetupId);

            return View(OepvisaDemandPo);
        }

        // GET: OepvisaDemandPo/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OepvisaDemandPo =  _context.OepvisaDemandPo
                .Include(t => t.OepvisaDemandPodetail)
                .ThenInclude(t=>t.JobTypeEntitySetup)
                .FirstOrDefault(t => t.Id == id);
            if (OepvisaDemandPo == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", OepvisaDemandPo.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name", OepvisaDemandPo.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name", OepvisaDemandPo.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPo.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name", OepvisaDemandPo.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["Modes"] = new SelectList(AppLists.Modes, "Value", "Text");
            ViewData["OepvisaDemandPoStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name", OepvisaDemandPo.OepvisaDemandPostatusEntitySetupId);
            ViewBag.JobTypeEntitySetupId = _context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE).ToList();

            return View(OepvisaDemandPo);
        }

        // POST: OepvisaDemandPo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, OepvisaDemandPo OepvisaDemandPo)
        {
            if (id != OepvisaDemandPo.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(OepvisaDemandPo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OepvisaDemandPoExists(OepvisaDemandPo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", OepvisaDemandPo.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name", OepvisaDemandPo.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name", OepvisaDemandPo.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", OepvisaDemandPo.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name", OepvisaDemandPo.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["Modes"] = new SelectList(AppLists.Modes, "Value", "Text");
            ViewData["OepvisaDemandPoStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name", OepvisaDemandPo.OepvisaDemandPostatusEntitySetupId);
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE), "Id", "Name", 1);
            return RedirectToAction("Index", new { id = OepvisaDemandPo.Id });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddDemandDetail(OepvisaDemandPodetail detail)
        {
            string status = "0";            
            if (ModelState.IsValid)
            {
                try
                {
                    var visaDemand = _context.OepvisaDemandPo.Find(detail.OepvisaDemandPoid);
                    var visaDemandDetail = _context.OepvisaDemandPodetail.Where(t => t.OepvisaDemandPoid == detail.OepvisaDemandPoid);
                    var totalQuantity = visaDemandDetail.Sum(t => t.Quantity);
                    totalQuantity = totalQuantity + detail.Quantity;
                    visaDemand.Quantity = totalQuantity;
                    _context.Update(visaDemand);
                    _context.OepvisaDemandPodetail.Add(detail);
                    _context.SaveChanges();
                    status = $"{detail.Id}";
                }
                catch (Exception ex)
                {
                    status = $"Error occured while saving demand. ({ex.Message})";
                    throw;
                }
                //return RedirectToAction(nameof(Index));
            }
            return Json(new { Message = status });
        }

        // GET: OepvisaDemandPo/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OepvisaDemandPo = await _context.OepvisaDemandPo
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandPostatusEntitySetup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (OepvisaDemandPo == null)
            {
                return NotFound();
            }

            return View(OepvisaDemandPo);
        }

        // POST: OepvisaDemandPo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var OepvisaDemandPo = await _context.OepvisaDemandPo.FindAsync(id);
            _context.OepvisaDemandPo.Remove(OepvisaDemandPo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OepvisaDemandPoExists(long id)
        {
            return _context.OepvisaDemandPo.Any(e => e.Id == id);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteDemandDetail(long? id)
        {
            string status = "0";
            try
            {
                var demandDetail = _context.OepvisaDemandPodetail.Find(id);
                if (demandDetail != null)
                {
                    var visaDemand = _context.OepvisaDemandPo.Find(demandDetail.OepvisaDemandPoid);
                    visaDemand.Quantity = visaDemand.Quantity - demandDetail.Quantity;
                    _context.Update(visaDemand);
                    _context.OepvisaDemandPodetail.Remove(demandDetail);
                    _context.SaveChanges();
                    status = $"Record deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                status = $"Error occured while saving demand. ({ex.Message})";
                throw;
            }
            return Json(new { Message = status });
        }
        private string GetNextSequence(long OEPId)
        {
            string sql = $"SELECT ISNULL(MAX(Code),1) From OepvisaDemandPo where OEPId={OEPId}";
            var nextSequence = _context.OepvisaDemandPo.Where(t => t.Oepid == OEPId).Max(t => t.Code);
            if(nextSequence!=null || Convert.ToInt32(nextSequence)>0)
            {
                nextSequence = Convert.ToString(Convert.ToInt32(nextSequence) + 1);
            }
            else
            {
                nextSequence = "1";
            }
            return nextSequence;
        }
        public async Task<IActionResult> Jobs(long id)
        {
            var demandJobs = _context.OepvisaDemandPodetail.Where(x => x.OepvisaDemandPoid ==  id);
            return View(await demandJobs.ToListAsync());
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetJobsByDemandId(long OepvisaDemandPoId)
        {
            var JobTypes = _context.OepvisaDemandPodetail.Include(d=>d.JobTypeEntitySetup)
                .Where(t => t.OepvisaDemandPoid == OepvisaDemandPoId && t.JobTypeEntitySetup.Id==t.JobTypeEntitySetupId)
                .Select(t => new { Id=t.JobTypeEntitySetupId,Name=t.JobTypeEntitySetup.Name }).ToList();
            return Json(JobTypes);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult VisaQuantity(long OepvisaDemandPoId, long visaTradeEntitySetupId)
        {
            var visaQuantity = _context.OepvisaDemandPodetail
                .FirstOrDefault(t => t.OepvisaDemandPoid == OepvisaDemandPoId && t.JobTypeEntitySetup.Id == visaTradeEntitySetupId);
            int totalVisaQuantity = 0;
            if (visaQuantity != null && visaQuantity.Quantity != null)
                totalVisaQuantity = visaQuantity.Quantity.Value;
            var visaIssuedQuantity = _context.VisaProcess
                .Where(t => t.OepvisaDemandId == OepvisaDemandPoId && t.VisaTradeEntitySetupId == visaTradeEntitySetupId).Count();
            int balance = totalVisaQuantity - visaIssuedQuantity;
            return Json(new VisaQuantityValidationDto() { Balance = balance, OEPVisaDemandId = OepvisaDemandPoId, OEPVisaDemandDetailId = 0 });
        }
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

    }
}
