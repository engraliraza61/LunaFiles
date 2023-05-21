    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Luna.Recruitment.VisaProcessing.Data.DTO;
using Luna.Recruitment.VisaProcessing.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Grpc.Core;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class CandidateSelectionController : Controller
    {
        private readonly lunaContext _context;
        private string protectorStatusCodes = "";
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;


        public CandidateSelectionController(lunaContext context, IWebHostEnvironment Environment, IConfiguration Configuration)
        {
            Configuration = this.Configuration;
            Environment = this.Environment;
            _context = context;
        }

        // GET: Client
        //[Authorize(Roles = "Administrator,Selection")]
        public async Task<IActionResult> Index()
        {
            if (TempData["DeleteMessage"] != null)
                ViewBag.DeleteMessage = TempData["DeleteMessage"];
            var selectionList =await _context.CandidateSelection.Include(c => c.Sponser).ToListAsync();
            //ViewBag.QtyList = _context.SelectionQtyDetail.Include(c=>c.CandidateSelection).ToList();
            return View(selectionList);

        }
        public async Task<ActionResult> DeactivateSelectionGroup(int id)
        {
            var candidateSelection = await _context.CandidateSelection.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (candidateSelection.Deactivate == false)
            {
                candidateSelection.Deactivate = true;
                _context.CandidateSelection.Update(candidateSelection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                candidateSelection.Deactivate = false;
                _context.CandidateSelection.Update(candidateSelection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
        }
        // GET: Client/Create
        public IActionResult Create()
        {
            var sponsers = _context.Sponser.Where(t => t.IsActive == true).ToList();
            ViewData["SponserId"] = new SelectList(sponsers, "Id", "Name");
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidateSelection candidateSelection)
        {

            if (ModelState.IsValid)
            {
                _context.Add(candidateSelection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var sponsers = _context.Sponser.Where(t => t.IsActive == true).ToList();
            ViewData["SponserId"] = new SelectList(sponsers, "Id", "Name");
            return View(candidateSelection);
        }
        public async Task<IActionResult> ShowSelection(long candidateProfileId)
        {
            if (candidateProfileId == 0)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = "<div style='position:fixed;top:50%;left:50%;margin-top:-50px;margin-left:-100px'><h1>Invalid candidate selection.</h1><div>"
                };
            }
            var candidateSelection = _context.CandidateSelectionDetail.FirstOrDefault(c => c.CandidateProfileId == candidateProfileId);
            if (candidateSelection == null)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = "<div style='position:fixed;top:50%;left:50%;margin-top:-50px;margin-left:-100px'><h1>No selection was found.</h1><div>"
                };
            }
            else
            {
                return RedirectToAction("Edit", new { id = candidateSelection.CandidateSelectionId });
            }
        }
        [HttpPost]
        public JsonResult AddSelectionAndFile(VisaProcess visa,CandidateSelectionDetail selectionDetail,long SponsorId)
        {
            try
            {
                var selectionSponsor = _context.CandidateSelection.Where(c => c.Id == selectionDetail.CandidateSelectionId).FirstOrDefault();
                var jobselectionPresent = _context.CandidatejobDetail.Where(c => c.CandidateSelectionDetailId == selectionDetail.CandidateSelectionId).FirstOrDefault();
                var candidatePresent = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                if (candidatePresent != null)
                {
                    //if (jobselectionPresent == null)
                    //{
                    //    ViewBag.jobselectionPresent = "Selection present in job selection";
                    //    return RedirectToAction("Index", "CandidateProfile");
                    //}
                    //else
                    {
                        var candidatePresentS = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                        CandidateSelectionDetail candidateSelectionDetail = new CandidateSelectionDetail();
                        VisaProcess visaProcess = new VisaProcess();
                        candidatePresentS.CandidateSelectionId = selectionDetail.CandidateSelectionId;
                        _context.UpdateRange(candidatePresentS);
                        _context.SaveChanges();
                        selectionSponsor.SponserId = SponsorId;
                        _context.CandidateSelection.Update(selectionSponsor);
                        _context.SaveChanges();
                        var candidate = _context.CandidateSelectionDetail.Where(e => e.CandidateSelectionId == selectionDetail.CandidateSelectionId && e.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                    var visaprocesss = _context.VisaProcess.Where(c => c.CandidateSelectionDetailId == candidatePresent.Id).FirstOrDefault();
                    if (visaprocesss == null)
                    {
                        VisaProcess process = new VisaProcess();
                        process.OepvisaDemandId = visa.OepvisaDemandId;
                        process.VisaTradeEntitySetupId = visa.VisaTradeEntitySetupId;
                        process.CandidateSelectionDetailId = candidate.Id;
                        var StatusRemarksA = _context.EntitySetup.Where(c => c.Id == visa.StatusEntitySetupId).FirstOrDefault();
                        process.StatusEntitySetupId = visa.StatusEntitySetupId;
                        _context.VisaProcess.Add(process);
                        _context.SaveChanges();
                    }
                    else
                    {
                        visaprocesss.OepvisaDemandId = visa.OepvisaDemandId;
                        visaprocesss.VisaTradeEntitySetupId = visa.VisaTradeEntitySetupId;
                        visaprocesss.CandidateSelectionDetailId = candidate.Id;
                        var StatusRemarks = _context.EntitySetup.Where(c => c.Id == visa.StatusEntitySetupId).FirstOrDefault();
                        visaprocesss.StatusEntitySetupId = visa.StatusEntitySetupId;
                        _context.VisaProcess.Update(visaprocesss);
                        _context.SaveChanges();
                    }    var FileNumber = _context.OepvisaDemand.Where(e => e.Id == visa.OepvisaDemandId).FirstOrDefault();
                        //return RedirectToAction("Deployed","CandidateProfile");
                        return Json(new { status=true});
                    }
                    
                }
                else
                {
                    CandidateSelectionDetail candidateSelectionDetail = new CandidateSelectionDetail();
                    VisaProcess visaProcess = new VisaProcess();
                    candidateSelectionDetail.CandidateProfileId = selectionDetail.CandidateProfileId;
                    candidateSelectionDetail.CandidateSelectionId = selectionDetail.CandidateSelectionId;
                    _context.Add(candidateSelectionDetail);
                    _context.SaveChanges();
                    selectionSponsor.SponserId = SponsorId;
                    _context.CandidateSelection.Add(selectionSponsor);
                    _context.SaveChanges();
                    var candidate = _context.CandidateSelectionDetail.Where(e => e.CandidateSelectionId == selectionDetail.CandidateSelectionId && e.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                    visaProcess.OepvisaDemandId = visa.OepvisaDemandId;
                    visaProcess.VisaTradeEntitySetupId = visa.VisaTradeEntitySetupId;
                    visaProcess.CandidateSelectionDetailId = candidate.Id;
                    var StatusRemarks = _context.EntitySetup.Where(c => c.Id.ToString() == visa.StatusRemarks).FirstOrDefault();
                    visaProcess.StatusRemarks = StatusRemarks.Name;
                    visaProcess.StatusEntitySetupId = visa.StatusEntitySetupId;
                    _context.VisaProcess.Add(visaProcess);
                    _context.SaveChanges();
                    var FileNumber = _context.OepvisaDemand.Where(e => e.Id == visa.OepvisaDemandId).FirstOrDefault();
                    //return RedirectToAction("Deployed","CandidateProfile");
                    return Json(new { status = true });
                }
               
            }
            catch(Exception ex)
            {
                throw;
            }
           
            //return Json(candidate);
        }

        [HttpPost]
        public JsonResult ReprocessCandidate(VisaProcess visa, CandidateSelectionDetail selectionDetail)
        {
            try
            {
                var jobselectionPresent = _context.CandidatejobDetail.Where(c => c.CandidateSelectionDetailId == selectionDetail.CandidateSelectionId).FirstOrDefault();
                var candidatePresent = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                CandidateSelectionDetail candidateSelectionDetail = new CandidateSelectionDetail();
                VisaProcess visaProcess = new VisaProcess();
                candidateSelectionDetail.CandidateProfileId = selectionDetail.CandidateProfileId;
                candidateSelectionDetail.CandidateSelectionId = selectionDetail.CandidateSelectionId;
                _context.Add(candidateSelectionDetail);
                _context.SaveChanges();

                var candidate = _context.CandidateSelectionDetail.Where(e => e.CandidateSelectionId == selectionDetail.CandidateSelectionId && e.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
                visaProcess.OepvisaDemandId = visa.OepvisaDemandId;
                visaProcess.VisaTradeEntitySetupId = visa.VisaTradeEntitySetupId;
                visaProcess.CandidateSelectionDetailId = candidate.Id;
                var StatusRemarks = _context.EntitySetup.Where(c => c.Id == visa.StatusEntitySetupId).FirstOrDefault();
                visaProcess.StatusRemarks = StatusRemarks.Name;
                visaProcess.StatusEntitySetupId = visa.StatusEntitySetupId;
                _context.VisaProcess.Add(visaProcess);
                _context.SaveChanges();
                var FileNumber = _context.OepvisaDemand.Where(e => e.Id == visa.OepvisaDemandId).FirstOrDefault();
                //return RedirectToAction("TabReport","Reports",new {fileNumber=FileNumber.Id });
                return Json(new { STATUS = true });
            }
            catch (Exception ex)
            {
                throw;
            }

            //return Json(candidate);
        }

        [HttpPost]
        public JsonResult ForEditSelection(int id)
        {
            var selectionDetail = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == id).Include(t=>t.VisaProcess).FirstOrDefault();
            var candidateProfile = _context.CandidateProfile.Where(c => c.Id == selectionDetail.CandidateProfileId).FirstOrDefault();
            var firstName = candidateProfile.FirstName;
            var lastName = candidateProfile.LastName;
            var candidate = _context.CandidateSelectionDetail.Where(e => e.CandidateSelectionId == selectionDetail.CandidateSelectionId && e.CandidateProfileId == selectionDetail.CandidateProfileId).FirstOrDefault();
            var visaprocess = _context.VisaProcess.Where(c => c.CandidateSelectionDetailId == candidate.Id).FirstOrDefault();
            ViewData["oepVisaDemandId"] = new SelectList(_context.OepvisaDemand, "Id", "Code", visaprocess.OepvisaDemandId);
            ViewData["entitySetupId"] = new SelectList(_context.EntitySetup, "Id", "Name", visaprocess.VisaTradeEntitySetupId);
            var selectLists = _context.CandidateSelection.Where(c => c.Id == selectionDetail.CandidateSelectionId).FirstOrDefault();
            ViewData["selectionId"] = new SelectList(_context.CandidateSelection, "Id", "SelectionGroup", selectLists.Id);
            var visaprocessvisa = candidate.VisaProcess.FirstOrDefault();
            var entityTypeA = _context.EntitySetup.Where(c => c.Id == visaprocessvisa.StatusEntitySetupId).FirstOrDefault();
            ViewData["status"] = new SelectList(_context.EntitySetup.Where(c => c.EntityTypeId == 6), "Id", "Name", entityTypeA.Id);
            var visaDemand = ViewData["oepVisaDemandId"];
            var EntitySetupName = ViewData["entitySetupId"];
            var selectionId = ViewData["selectionId"];
            var status = ViewData["status"];
            var profileId = id;
            var result = new { visaDemand, EntitySetupName,selectionId,status, lastName ,firstName, profileId };
            return Json(result);
        }

        public async Task<IActionResult> ShowVisaProcess(long candidateProfileId,string calledFrom="")
        {
            if (candidateProfileId == 0)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = "<div style='position:fixed;top:50%;left:50%;margin-top:-50px;margin-left:-100px'><h1>Invalid candidate selection.</h1><div>"
                };
            }
            var candidateSelectionDetail = _context.CandidateSelectionDetail.FirstOrDefault(c => c.CandidateProfileId == candidateProfileId);
            if (candidateSelectionDetail == null)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    Content = "<div style='position:fixed;top:50%;left:50%;margin-top:-50px;margin-left:-100px'><h1>No selection was found.</h1><div>"
                };
            }
            else
            {
                ViewBag.ActionName = calledFrom;
                return RedirectToAction("VisaProcess", new { candidateSelectionDetailId = candidateSelectionDetail.Id,calledFrom=calledFrom });
            }
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateSelection = await _context.CandidateSelection
                .Include(c => c.CandidateSelectionDetail)
                .ThenInclude(c => c.CandidateProfile)
                .ThenInclude(c => c.Passport)
                .FirstOrDefaultAsync(c => c.Id == id);
            ViewBag.ActionName = "CandidateSelectionEdit";
            TempData["CandidateSelectionId"] = id;
            if (candidateSelection == null)
            {
                return NotFound();
            }
            var selectedCandidates = _context.CandidateSelectionDetail.Where(s => s.CandidateSelectionId == id).ToList();
            var allSelectedCandidates = _context.CandidateSelectionDetail;
            List<long> selectedCandidatesIds = new List<long>();
            foreach (var item in allSelectedCandidates)
            {
                selectedCandidatesIds.Add(item.CandidateProfileId);
            }
            _context.Database.SetCommandTimeout(0);
            ViewBag.CandidateProfiles =await _context.CandidateProfile
                .Include(p => p.Passport)
                .Where(p => !selectedCandidatesIds.Contains(p.Id)).ToListAsync();
            //ViewBag.SelectedCandidates = _context.CandidateSelectionDetail.Include(p => p.CandidateProfile).ToList();
            //.Select(t=>new {Id=t.Id,FirstName= t.FirstName, LastName=t.LastName,FatherName=t.FatherName,PassportNo=t.Passport.FirstOrDefault().PassportNo,Cnic=t.Cnic})
            var sponsers = _context.Sponser.Where(t => t.IsActive == true).ToList();
            ViewData["SponserId"] = new SelectList(sponsers, "Id", "Name", candidateSelection.SponserId);
            return View(candidateSelection);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CandidateSelection candidateSelection)
        {
            if (id != candidateSelection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateSelection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateSelectionExists(candidateSelection.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", new { id = id });
            }
            var sponsers = _context.Sponser.Where(t => t.IsActive == true).ToList();
            ViewData["SponserId"] = new SelectList(sponsers, "Id", "Name", candidateSelection.SponserId);
            return View(candidateSelection);
        }
        public async Task<IActionResult> ProtectorList()
        {
            var visaStamped = await _context.VisaProcess
                .Include(v => v.CandidateSelectionDetail)
                .ThenInclude(cs => cs.CandidateProfile)
                .ThenInclude(cp => cp.Passport)
                .Where(v => v.StatusEntitySetup.Code == "08" && v.StatusEntitySetup.Id==67).ToListAsync();
            if (visaStamped == null)
            {
                return NotFound();
            }
            return View(visaStamped);
        }
        public async Task<IActionResult> TicketingList()
        {
            var visaStamped = _context.VisaProcess
                .Include(v => v.CandidateSelectionDetail)
                .ThenInclude(cs => cs.CandidateProfile)
                .ThenInclude(cp => cp.Passport)
                .Where(v => v.StatusEntitySetup.Code == "22" && v.StatusEntitySetup.Id==140).ToList();
            if (visaStamped == null)
            {
                return NotFound();
            }
            ViewData["TicketList"] = visaStamped;
            return View(visaStamped);
        }
        // GET: Client/Delete/5
        public async Task<IActionResult> DeleteSelection(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var selection = await _context.CandidateSelection.Include(c => c.CandidateSelectionDetail).FirstOrDefaultAsync(m => m.Id == id);
            if (selection == null)
            {
                return NotFound();
            }
            var selectionDetail = selection.CandidateSelectionDetail;
            if (selectionDetail != null && selectionDetail.Any() && selectionDetail.Count() > 0)
            {
                TempData["DeleteMessage"] = "Selection cannot be deleted as it has details.";
                return RedirectToAction("Index", "CandidateSelection");
            }
            return View(selection);
        }
        // POST: Client/Delete/5
        [HttpPost, ActionName("DeleteSelection")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelectionConfirmed(long id)
        {
            var candidateSelection = await _context.CandidateSelection.FindAsync(id);
            _context.CandidateSelection.Remove(candidateSelection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CandidateSelection/JobDetail/5
        public async Task<IActionResult> JobDetail(long candidateSelectionDetailId,long candidateSelectionId)
        {
            CandidatejobDetail candidateJobDetail;

            if (CandidateJobDetailExists(candidateSelectionDetailId))
            {
                candidateJobDetail = await _context.CandidatejobDetail
                    .Where(c => c.CandidateSelectionDetailId == candidateSelectionDetailId).FirstOrDefaultAsync();
            }
            else
            {
                candidateJobDetail = new CandidatejobDetail();
                candidateJobDetail.CandidateSelectionDetailId = candidateSelectionDetailId;
                candidateJobDetail.Id = 0;
            }
            if (candidateJobDetail == null)
            {
                return NotFound();
            }
            var selectionTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.SelectionTradeEntitySetupId = new SelectList(selectionTrades, "Id", "Name");
            ViewBag.candidateSelectionDetailId = candidateSelectionDetailId;
            ViewBag.candidateSelectionId = candidateSelectionId;
            VisaProcess visa = _context.VisaProcess.Where(c => c.CandidateSelectionDetailId == 19).FirstOrDefault();
            ViewBag.FileNumber = _context.EntitySetup.Where(c => c.Id == visa.VisaTradeEntitySetupId).Select(c => c.Code).FirstOrDefault();
            return View(candidateJobDetail);
        }

        // POST:
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobDetail(CandidatejobDetail candidatejobDetail)
        {
            if (ModelState.IsValid)
            {
                long candidateSelectionId = 0;
                try
                {
                    if (candidatejobDetail.Id == null || candidatejobDetail.Id <= 0)
                    {
                        _context.Add(candidatejobDetail);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Update(candidatejobDetail);
                        await _context.SaveChangesAsync();
                    }
                    candidateSelectionId = _context.CandidateSelectionDetail.FirstOrDefault(c => c.Id == candidatejobDetail.CandidateSelectionDetailId).CandidateSelectionId;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return RedirectToAction("JobDetail", "CandidateSelection", new { candidateSelectionDetailId = candidatejobDetail.CandidateSelectionDetailId, candidateSelectionId= candidateSelectionId });
            }
            var selectionTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.SelectionTradeEntitySetupId = new SelectList(selectionTrades, "Id", "Name", candidatejobDetail.SelectionTradeEntitySetupId);
            return View(candidatejobDetail);
        }   
        // GET: CandidateSelection/VisaProcessing/5
        public async Task<IActionResult> VisaProcess(long candidateSelectionDetailId,string calledFrom = "")
        {
            VisaProcess protectorProcessing;

            if (VisaProcessingExists(candidateSelectionDetailId))
            {
                protectorProcessing = await _context.VisaProcess
                    .Include(c => c.CandidateSelectionDetail)
                    .ThenInclude(c => c.CandidateProfile)
                    .ThenInclude(p => p.Passport)
                    .Where(c => c.CandidateSelectionDetailId == candidateSelectionDetailId).FirstOrDefaultAsync();
                ViewBag.ProcessingStartDate = protectorProcessing.ProcessingStartDate;
                var PassportNo = protectorProcessing.CandidateSelectionDetail.CandidateProfile.Passport.FirstOrDefault().PassportNo;
                ViewBag.PassportNo = PassportNo;
            }
            else
            {
                var selectionDetail = _context.CandidateSelectionDetail
                    .Include(c => c.CandidateProfile)
                    .FirstOrDefault(c => c.Id == candidateSelectionDetailId);
                protectorProcessing = new VisaProcess();
                protectorProcessing.CandidateSelectionDetail = selectionDetail;
                protectorProcessing.CandidateSelectionDetailId = candidateSelectionDetailId;
                protectorProcessing.OepvisaDemandId = 0;
                protectorProcessing.Id = 0;
            }
            if (protectorProcessing == null)
            {
                return NotFound();
            }

            var OepvisaDemands = _context.OepvisaDemand.ToList();
            ViewBag.OepvisaDemandId = new SelectList(OepvisaDemands, "Id", "Code", protectorProcessing.OepvisaDemandId);

            var visaTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.VisaTradeEntitySetupId = new SelectList(visaTrades, "Id", "Name", protectorProcessing.VisaTradeEntitySetupId ?? visaTrades.FirstOrDefault().Id);

            var VisaStatus = _context.EntitySetup.Where(t => t.EntityType.Code == "06").ToList();
            ViewBag.StatusEntitySetupId = new SelectList(VisaStatus, "Id", "Name", protectorProcessing.StatusEntitySetupId ?? VisaStatus.FirstOrDefault().Id);
            ViewBag.ActionName = calledFrom;
            
            return View(protectorProcessing);
        }

        // GET: CandidateSelection/Protector/5
        public async Task<IActionResult> Protector(long candidateSelectionDetailId)
        {
            VisaProcess visaProcessing;

            if (VisaProcessingExists(candidateSelectionDetailId))
            {
                visaProcessing = await _context.VisaProcess
                    .Include(c => c.CandidateSelectionDetail)
                    .ThenInclude(c => c.CandidateProfile)
                    .Where(c => c.CandidateSelectionDetailId == candidateSelectionDetailId).FirstOrDefaultAsync();
            }
            else
            {
                var selectionDetail = _context.CandidateSelectionDetail
                    .Include(c => c.CandidateProfile)
                    .FirstOrDefault(c => c.Id == candidateSelectionDetailId);
                visaProcessing = new VisaProcess();
                visaProcessing.CandidateSelectionDetail = selectionDetail;
                visaProcessing.CandidateSelectionDetailId = candidateSelectionDetailId;
                visaProcessing.OepvisaDemandId = 0;
                visaProcessing.Id = 0;
            }
            if (visaProcessing == null)
            {
                return NotFound();
            }

            var OepvisaDemands = _context.OepvisaDemand.ToList();
            ViewBag.OepvisaDemandId = new SelectList(OepvisaDemands, "Id", "Code", visaProcessing.OepvisaDemandId);

            var visaTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.VisaTradeEntitySetupId = new SelectList(visaTrades, "Id", "Name", visaProcessing.VisaTradeEntitySetupId ?? visaTrades.FirstOrDefault().Id);

            var VisaStatus = _context.EntitySetup.Where(t => t.EntityType.Code == "06" && (t.Code == "08" || t.Code == "22")).ToList();
            ViewBag.StatusEntitySetupId = new SelectList(VisaStatus, "Id", "Name", visaProcessing.StatusEntitySetupId ?? VisaStatus.FirstOrDefault().Id);
            var SectorInfo = _context.EntitySetup.Where(t => t.EntityTypeId == 7).ToList();
            ViewBag.Sector1From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1From);
            ViewBag.Sector1To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1To);
            ViewBag.Sector2From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2From);
            ViewBag.Sector2To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2To);
            ViewBag.Sector3From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3From);
            ViewBag.Sector3To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3To);

            return View(visaProcessing);
        }
        // GET: CandidateSelection/Protector/5
        public async Task<IActionResult> Ticketing(long candidateSelectionDetailId)
        {
            VisaProcess visaProcessing;

            if (VisaProcessingExists(candidateSelectionDetailId))
            {
                visaProcessing = await _context.VisaProcess
                    .Include(c => c.CandidateSelectionDetail)
                    .ThenInclude(c => c.CandidateProfile)
                    .Where(c => c.CandidateSelectionDetailId == candidateSelectionDetailId).FirstOrDefaultAsync();
            }
            else
            {
                var selectionDetail = _context.CandidateSelectionDetail
                    .Include(c => c.CandidateProfile)
                    .FirstOrDefault(c => c.Id == candidateSelectionDetailId);
                visaProcessing = new VisaProcess();
                visaProcessing.CandidateSelectionDetail = selectionDetail;
                visaProcessing.CandidateSelectionDetailId = candidateSelectionDetailId;
                visaProcessing.OepvisaDemandId = 0;
                visaProcessing.Id = 0;
            }
            if (visaProcessing == null)
            {
                return NotFound();
            }

            var OepvisaDemands = _context.OepvisaDemand.ToList();
            ViewBag.OepvisaDemandId = new SelectList(OepvisaDemands, "Id", "Code", visaProcessing.OepvisaDemandId);

            var visaTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.VisaTradeEntitySetupId = new SelectList(visaTrades, "Id", "Name", visaProcessing.VisaTradeEntitySetupId ?? visaTrades.FirstOrDefault().Id);

            var VisaStatus = _context.EntitySetup.Where(t => t.EntityType.Code == "06" && (t.Code == "11" || t.Code == "22" || t.Code == "23" || t.Code == "24")).ToList();
            ViewBag.StatusEntitySetupId = new SelectList(VisaStatus, "Id", "Name", visaProcessing.StatusEntitySetupId ?? VisaStatus.FirstOrDefault().Id);

            var SectorInfo = _context.EntitySetup.Where(t => t.EntityTypeId == 7).ToList();
            ViewBag.Sector1From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1From);
            ViewBag.Sector1To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1To);
            ViewBag.Sector2From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2From);
            ViewBag.Sector2To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2To);
            ViewBag.Sector3From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3From);
            ViewBag.Sector3To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3To);
            return View(visaProcessing);
        }
        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> VisaProcess(VisaProcess visaProcessing, string calledFrom = "")
        {
            if (ModelState.IsValid)
            {
                if (visaProcessing.Id == null || visaProcessing.Id <= 0)
                {
                    _context.Add(visaProcessing);
                    await _context.SaveChangesAsync();
                    ViewBag.VisaProsessResponse = "Submitted Successfully";
                }
                else
                {
                    _context.Update(visaProcessing);
                    await _context.SaveChangesAsync();
                    ViewBag.VisaProsessResponse = "Submitted Successfully";
                }
                var candidateSelectionId = _context.CandidateSelectionDetail.FirstOrDefault(c => c.Id == visaProcessing.CandidateSelectionDetailId);
                if (calledFrom.Equals("Ticketing"))
                {
                    return RedirectToAction("TicketingList");
                    //return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });
                }
                else if (calledFrom.Equals("Protector"))
                {
                    return RedirectToAction("ProtectorList");
                    //return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });
                }
                else if (calledFrom.Equals("UnderProcessed"))
                {
                    /*return RedirectToAction("VisaProcess", new { candidateSelectionDetailId =visaProcessing.CandidateSelectionDetailId, calledFrom = "UnderProcessed" });*/
                    return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });

                }
                else if (calledFrom.Equals("Deployed"))
                {
                    //return RedirectToAction("VisaProcess", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId, calledFrom = "Deployed" });
                    return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });
                }
                else if (calledFrom.Equals("Index"))
                {
                    //return RedirectToAction("VisaProcess", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId, calledFrom = "Index" });
                    return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });
                }
                else if (calledFrom.Equals("CandidateSelectionEdit"))
                {
                    //return RedirectToAction("Edit", "CandidateSelection", new { id = candidateSelectionId.CandidateSelectionId });
                    return RedirectToAction("VisaProcess", "CandidateSelection", new { candidateSelectionDetailId = visaProcessing.CandidateSelectionDetailId });
                }
                else
                    ViewBag.VisaProsessResponse = "Submitted Successfully";
                return View();
            }
            var OepvisaDemands = _context.OepvisaDemand.ToList();
            ViewBag.OepvisaDemandId = new SelectList(OepvisaDemands, "Id", "BatchNumber", visaProcessing.OepvisaDemandId);

            var visaTrades = _context.EntitySetup.Where(t => t.EntityTypeId == 5).ToList();
            ViewBag.VisaTradeEntitySetupId = new SelectList(visaTrades, "Id", "Name", visaProcessing.VisaTradeEntitySetupId);

            var VisaStatus = _context.EntitySetup.Where(t => t.EntityType.Code == "06").ToList();
            ViewBag.StatusEntitySetupId = new SelectList(VisaStatus, "Id", "Name", visaProcessing.StatusEntitySetupId);
            var SectorInfo = _context.EntitySetup.Where(t => t.EntityTypeId == 7).ToList();
            ViewBag.Sector1From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1From);
            ViewBag.Sector1To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector1To);
            ViewBag.Sector2From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2From);
            ViewBag.Sector2To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector2To);
            ViewBag.Sector3From = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3From);
            ViewBag.Sector3To = new SelectList(SectorInfo, "Id", "Name", visaProcessing.Sector3To);

            if (calledFrom.Equals("Ticketing"))
            {
                return View("TicketingList");
            }
            else if (calledFrom.Equals("Protector"))
            {
                return View("ProtectorList");
            }
            else
                return View(visaProcessing);
        }

        public bool SaveSelection(string SelectedCandidates, long candidateSelectionId)
        {
            bool status = false;
            List<long> numbers = new List<long>(Array.ConvertAll(SelectedCandidates.Split(','), long.Parse));
            try
            {
                //Get Current SelectionDetails against SelectionID
                var selectionDetail = _context.CandidateSelectionDetail.Where(c => c.CandidateSelectionId == candidateSelectionId);
                if (selectionDetail != null && selectionDetail.Any())
                {
                    // Delete from SelectionSelectionDetail Where Id not in SelectedCandiates list paramter
                    var selectionDetailToDelete = _context.CandidateSelectionDetail
                        .Include(f => f.FollowUp)
                        .Include(f => f.CandidatejobDetail)
                        .Include(f => f.VisaProcess)
                        .Where(c => c.CandidateSelectionId == candidateSelectionId && !numbers.Contains(c.CandidateProfileId));
                    foreach (var item in selectionDetailToDelete)
                    {
                        _context.FollowUp.RemoveRange(item.FollowUp);
                        _context.CandidatejobDetail.RemoveRange(item.CandidatejobDetail);
                        _context.VisaProcess.RemoveRange(item.VisaProcess);
                    }
                    _context.CandidateSelectionDetail.RemoveRange(selectionDetailToDelete);
                    //Save new records for IDs which are in parameter list but not in Selection Details DB list
                    foreach (var candidateProfileId in numbers)
                    {
                        if (!selectionDetail.Where(s => s.CandidateProfileId == candidateProfileId).Any())
                        {
                            CandidateSelectionDetail newSelection = new CandidateSelectionDetail();
                            newSelection.CandidateProfileId = candidateProfileId;
                            newSelection.CandidateSelectionId = candidateSelectionId;
                            _context.CandidateSelectionDetail.Add(newSelection);
                        }
                    }
                }
                else
                {
                    foreach (var candidateId in numbers)
                    {
                        CandidateSelectionDetail newSelection = new CandidateSelectionDetail();
                        newSelection.CandidateProfileId = candidateId;
                        newSelection.CandidateSelectionId = candidateSelectionId;
                        _context.CandidateSelectionDetail.Add(newSelection);
                    }
                }
                var restult = _context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }
        [Authorize(Roles = "Administrator,Selection")]
        [HttpGet]
        public async Task<IActionResult> WalkInSelection()
        {
            ViewBag.FileType = AppLists.FileTypes;
            var cityList = _context.City.Where(t => t.StateId == 2 || t.StateId == 3 || t.StateId == 4 || t.StateId == 5 || t.StateId == 6 || t.StateId == 8).ToList();
            var country = _context.Country.ToList();
            ViewBag.DomicileCity = new SelectList(AppLists.Domicile, "Value", "Text", 9);
            ViewBag.Religion = new SelectList(AppLists.Religion, "Value", "Text", "Islam");
            ViewBag.Gender = new SelectList(AppLists.Gender, "Value", "Text", "Islam");
            ViewBag.MaritalStatus = new SelectList(AppLists.MaritalStatus, "Value", "Text", "Married");
            ViewBag.BirthCountryId = new SelectList(country, "Id", "Name", 165);
            ViewBag.OEPId = new SelectList(_context.Oep.ToList(), "Id", "Name", 1);
            ViewBag.BirthCityId = new SelectList(cityList, "Id", "Name", 9);
            ViewBag.NationalityCountryId = new SelectList(country, "Id", "Name", 165);
            ViewBag.AlternateNationalityCountryId = new SelectList(country, "Id", "Name", 165);
            ViewBag.JobType_EntitySetupId = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Code == "05").ToList(), "Id", "Name", 1);
            ViewBag.SponserId = new SelectList(_context.Sponser.ToList(), "Id", "Name", 1);
            return View();
        }
        //[Authorize(Roles = "Administrator,Selection")]
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public IActionResult SelectionCandidate(CandidateBulkSelection selectionAdd,int SponsorId,string batchNumber)
        {
            if (SponsorId != 0)
            {
                List<CandidateBulkSelection> candidateBulk = _context.CandidateBulkSelection.Where(c => c.BatchNumber == batchNumber).ToList();
                ViewBag.candidateBulk = candidateBulk;
                ViewBag.SponsorSelectionId = SponsorId;
                var c = (from x in _context.CandidateBulkSelection.OrderByDescending(u => u.Id)
                         select new CandidateBulkSelection()
                         {
                             BatchNumber = x.BatchNumber,
                         }).FirstOrDefault();
                ViewBag.BatchNumber = c.BatchNumber;
                ViewData["SelectionList"] = _context.CandidateBulkSelection
                .Where(p => p.CnicNumber != null).ToList();
                return View();
            }
            else
            {
                List<CandidateBulkSelection> candidateBulk = _context.CandidateBulkSelection.Where(c => c.BatchNumber == batchNumber).ToList();
                ViewBag.candidateBulk = candidateBulk;
                ViewData["SelectionList"] = _context.CandidateBulkSelection
                   .Where(p => p.CnicNumber != null).ToList();
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSelectionCandidate(SelectionAddCandidateMaster selectionAdd,[FromForm] CandidateDocumentDto candidateDocumentDto,int SelectionBatchNumber,int SponserId)
        {
            OepvisaDemand oepvisa = _context.OepvisaDemand.Where(c => c.Id == SelectionBatchNumber).FirstOrDefault();
            
            var uniqueFileName = System.IO.Path.GetFileNameWithoutExtension(candidateDocumentDto.File.FileName) + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(candidateDocumentDto.File.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\Excel", uniqueFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                candidateDocumentDto.File.CopyTo(stream);
            }
            List<CandidateBulkSelection> candidates = new List<CandidateBulkSelection>();
            var file = path;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage package = new ExcelPackage(file))
            {

                List<CandidateBulkSelection> customerList = new List<CandidateBulkSelection>();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                DataTable dt = new DataTable();
                foreach (var firstRowCel in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCel.Text);
                }
                for (var rowNumber = 2; rowNumber < worksheet.Dimension.End.Row+1; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Column];
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(newRow);
                }
                List<CandidateBulkSelection> studentListA = new List<CandidateBulkSelection>();
                var SpnosorName = _context.Sponser.Where(c => c.Id == SponserId).FirstOrDefault();
                var SelectionName = _context.CandidateSelection.Where(c => c.Id == Convert.ToInt64(selectionAdd.SelectionGroup)).FirstOrDefault();
                var obj = DateTime.Now.ToString("MM/dd/yyyy") + SpnosorName.CompanyShortName + SelectionName.SelectionGroup;
                obj = obj.Replace(" ", "");
                obj=RemoveSpecialCharacters(obj);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CandidateBulkSelection student = new CandidateBulkSelection();
                    //student.Id = Convert.ToInt32(dt.Rows[i]["Sr#"]);
                    student.Reference = dt.Rows[i]["Reference"].ToString();
                    student.CandidateName = dt.Rows[i]["Candidate Name"].ToString();
                    student.FatherName = dt.Rows[i]["Father / Husband Name"].ToString();
                    student.MaritalStatus = dt.Rows[i]["Marital Status"].ToString();
                    student.Gender = dt.Rows[i]["Gender"].ToString();
                    student.CnicNumber = dt.Rows[i]["Cnic Number"].ToString();
                    student.CnicIssueDate = dt.Rows[i]["CNCI Issue Date"].ToString();
                    student.ExpiryDate = dt.Rows[i]["Expiry Date"].ToString();
                    student.DateOfBirth = dt.Rows[i]["Date of Birth"].ToString();
                    student.BirthCountry = dt.Rows[i]["Birth Country"].ToString();
                    student.PermanentAddress = dt.Rows[i]["Permanent Address"].ToString();
                    student.CurrentAddress = dt.Rows[i]["Current Address"].ToString();
                    student.Religion = dt.Rows[i]["Religion"].ToString();
                    student.CellNo = dt.Rows[i]["Cell #"].ToString();
                    student.PassportNo = dt.Rows[i]["Passport #"].ToString();
                    student.PassportIssueDate = dt.Rows[i]["Passport Issue Date"].ToString();
                    student.passportExpiryDate = dt.Rows[i]["passport Expiry Date"].ToString();
                    student.SelectionTrade = dt.Rows[i]["Selection Trade"].ToString();
                    student.Salary = dt.Rows[i]["Salary"].ToString();
                    student.Food = dt.Rows[i]["Food"].ToString();
                    student.AgentName = dt.Rows[i]["Agent Name"].ToString();
                    student.ContrctDurationType = dt.Rows[i]["Contrct Duration Type"].ToString();
                    student.ContrctDuration = dt.Rows[i]["Contrct Duration"].ToString();
                    student.BatchNumber = obj.ToString();
                    customerList.Add(student);
                }
                _context.CandidateBulkSelection.AddRange(customerList);
                _context.SaveChanges();
                if (candidateDocumentDto.File != null)
                {
                    SelectionAddCandidateMaster selectionAdd1 = new SelectionAddCandidateMaster();
                    selectionAdd1.ListFile = candidateDocumentDto.File.FileName;
                    var group = _context.CandidateSelection.Where(c => c.Id.ToString() == selectionAdd.SelectionGroup).FirstOrDefault();
                    selectionAdd1.SelectionGroup = group.SelectionGroup;
                    selectionAdd1.DateOfInterview = selectionAdd.DateOfInterview;
                    selectionAdd1.PlaceOfInterview = selectionAdd.PlaceOfInterview;
                    selectionAdd1.DateOfUploading = selectionAdd.DateOfUploading;
                    selectionAdd1.SponserId = selectionAdd.SponserId;
                    _context.SelectionAddCandidateMaster.Add(selectionAdd1);
                    _context.SaveChanges();

                }
                return RedirectToAction("SelectionCandidate", new { SponsorId = selectionAdd.SponserId });
            }
            using (ExcelPackage package = new ExcelPackage(file))
            {

                List<CandidateBulkSelection> customerList = new List<CandidateBulkSelection>();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.First();
                DataTable dt = new DataTable();
               foreach (var firstRowCel in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCel.Text);
                }
                for (var rowNumber = 2; rowNumber < worksheet.Dimension.End.Row+1; rowNumber++)
                {
                    var row = worksheet.Cells[rowNumber, 1, rowNumber, worksheet.Dimension.End.Row];
                    var newRow = dt.NewRow();
                    foreach (var cell in row)
                    {
                        newRow[cell.Start.Column - 1] = cell.Text;
                    }
                    dt.Rows.Add(newRow);
                }
                
            }
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= '0' && str[i] <= '9')
                    || (str[i] >= 'A' && str[i] <= 'z')
                        || (str[i] == '.' || str[i] == '_'
                        || (str[i] == '-')))
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WalkInSelection(WalkInSelection ws)
        {
            if (String.IsNullOrWhiteSpace(ws.SelectionGroup) || ws.SponserId == null || ws.SponserId <= 0 ||
                String.IsNullOrWhiteSpace(Convert.ToString(ws.DateOfBirth)) || string.IsNullOrWhiteSpace(ws.CNICNumber))
            {
                ModelState.AddModelError("SelectionGroup", "Please make sure all the mandatory feilds are entered.");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.FileType = AppLists.FileTypes;
                var cityList = _context.City.Where(t => t.StateId == 2 || t.StateId == 3 || t.StateId == 4 || t.StateId == 5 || t.StateId == 6 || t.StateId == 8).ToList();
                var country = _context.Country.ToList();
                ViewBag.DomicileCityId = new SelectList(cityList, "Id", "Name", 9);
                ViewBag.Religion = new SelectList(AppLists.Religion, "Value", "Text", "Islam");
                ViewBag.Gender = AppLists.Gender;
                ViewBag.BirthCountryId = new SelectList(country, "Id", "Name", 165);
                ViewBag.OEPId = new SelectList(_context.Oep.ToList(), "Id", "Name", 1);
                ViewBag.BirthCityId = new SelectList(cityList, "Id", "Name", 9);
                ViewBag.NationalityCountryId = new SelectList(country, "Id", "Name", 165);
                ViewBag.AlternateNationalityCountryId = new SelectList(country, "Id", "Name", 165);
                ViewBag.JobType_EntitySetupId = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Code == "05").ToList(), "Id", "Name", 1);
                ViewBag.SponserId = new SelectList(_context.Sponser.ToList(), "Id", "Name", 1);
                return View(ws);
            }
            var cp = new CandidateProfile();
            cp.FirstName = ws.FirstName;
            cp.LastName = ws.LastName;
            cp.FatherName = ws.FatherName;
            cp.Cnic = ws.CNICNumber;
            cp.IssueDate = ws.IssueDate;
            cp.ExpiryDate = ws.ExpiryDate;
            cp.DateOfBirth = ws.DateOfBirth;
            cp.Domicile = ws.DomicileCity;
            cp.Religion = ws.Religion;
            cp.MaritalStatus = ws.MaritalStatus;
            cp.Gender = ws.Gender;
            cp.PlaceOfBirthCountryId = ws.BirthCountryId;
            cp.PlaceOfBirthCityId = ws.BirthCityId;
            cp.CellNumber = ws.CellNumber;
            cp.BusinessPhone = ws.CellNumber2;
            cp.ReferencePhone = ws.CellNumber3;
            cp.PreviousNationalityCountryId = ws.NationalityCountryId;
            cp.PermanentAddress = ws.PermanentAddress;
            cp.HomeAddress = ws.CurrentAddress;
            _context.CandidateProfile.Add(cp);
            var cpAddStatus = _context.SaveChanges();
            if (cpAddStatus > 0)
            {
                var cs = new CandidateSelection() { SelectionDate = DateTime.Now, SponserId = ws.SponserId, SelectionGroup = ws.SelectionGroup };
                _context.CandidateSelection.Add(cs);
                _context.SaveChanges();
                var csd = new CandidateSelectionDetail() { CandidateProfileId = cp.Id, CandidateSelectionId = cs.Id };
                _context.CandidateSelectionDetail.Add(csd);
                _context.SaveChanges();
                var demand = new OepvisaDemand();
                demand.Oepid = ws.OEPId;
                demand.SponserId = ws.SponserId;
                demand.EntryDate = DateTime.Now;
                demand.SponserGroup = ws.SelectionGroup;
                demand.BatchNumber = "WalkIn";
                var newFileNumber = GetNextSequence(ws.OEPId);
                demand.Code = newFileNumber;
                demand.VisaNumberDate = "2021-01-04";
                demand.Quantity = 1;
                demand.IsActive = true;
                demand.IsDeleted = false;
                _context.OepvisaDemand.Add(demand);
                _context.SaveChanges();
                ViewBag.StatusMessage = $"Selection created with File Number:{newFileNumber}";
                return View(ws);
            }
            return View(ws);
        }
         
        
        public IActionResult UploadExcelFile()
        {
            return View();
        }
        public IActionResult GetSelectionCandidateByBatchNumber(string batchNumber)
        {
            return RedirectToAction("SelectionCandidate", new {batchNumber=batchNumber });
        }

        private bool CandidateSelectionExists(long id)
        {
            return _context.CandidateSelection.Any(e => e.Id == id);
        }
        private bool CandidateJobDetailExists(long candidateSelectionDetailId)
        {
            return _context.CandidatejobDetail.Any(e => e.CandidateSelectionDetailId == candidateSelectionDetailId);
        }
        private bool VisaProcessingExists(long candidateSelectionDetailId)
        {
            return _context.VisaProcess.Any(e => e.CandidateSelectionDetailId == candidateSelectionDetailId);
        }
        public JsonResult GetSelection()
        {
                var CandidateSelection = _context.Sponser.Where(e => e.Id !=0).OrderBy(c=>c.CompanyShortName).Distinct().ToList();
                return Json(CandidateSelection);
           
        }
        public JsonResult GetSelectionForBulkAdd()
        {
            var CandidateSelection = _context.CandidateBulkSelection.Where(e => e.Id != 0).OrderBy(c => c.BatchNumber).Distinct().ToList();
            return Json(CandidateSelection);

        }
        public JsonResult GetBatchNumber()
        {
                var CandidateSelection = _context.OepvisaDemand.Where(e => e.Id !=0).OrderBy(c=>c.BatchNumber).Distinct().ToList();
                return Json(CandidateSelection);
           
        }
        public JsonResult GetSelectionGroup(int sponsorId)
        {
            var CandidateSelection = _context.CandidateSelection.Where(e => e.SponserId == sponsorId).OrderBy(c => c.SelectionGroup).ToList();
            return Json(CandidateSelection);
        }
        private string GetNextSequence(long OEPId)
        {
            string sql = $"SELECT ISNULL(MAX(Code),1) From OEPVisaDemand where OEPId={OEPId}";
            var nextSequence = _context.OepvisaDemand.Where(t => t.Oepid == OEPId).Max(t => t.Code);
            if (nextSequence != null || Convert.ToInt32(nextSequence) > 0)
            {
                nextSequence = Convert.ToString(Convert.ToInt32(nextSequence) + 1);
            }
            else
            {
                nextSequence = "1";
            }
            return nextSequence;
        }
        public JsonResult SelectionQuantity(long sponsorId)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetSelectionQty";
                command.Parameters.Add(new SqlParameter("@selectionId", sponsorId));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                var visa = _context.VisaProcess.Where(c => c.CandidateSelectionDetailId == sponsorId).FirstOrDefault();
                var selection=_context.CandidateSelectionDetail.Where(c => c.Id == sponsorId).FirstOrDefault();
                var Job = _context.CandidatejobDetail.Where(c => c.CandidateSelectionDetailId == sponsorId).FirstOrDefault();
                var selectionQty=new Object();
                if (Job != null)
                {
                    selectionQty = _context.SelectionQtyDetail.Where(c => c.CandidateSelectionId == selection.CandidateSelectionId && c.EntitySetupId == Job.SelectionTradeEntitySetupId).FirstOrDefault();
                }
                
                return Json(new { selectionQtyCount = dt,selectionQty=selectionQty });
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
        public JsonResult SelectionQuantityA(long sponsorId)
        {
            try
            {
                var selected = _context.CandidateSelectionDetail.Where(c => c.CandidateSelectionId == sponsorId).FirstOrDefault();
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetSelectionQty";
                command.Parameters.Add(new SqlParameter("@selectionId", selected.Id));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                var selection = _context.CandidateSelectionDetail.Where(c => c.Id == selected.Id).FirstOrDefault();
                var selectionQty = _context.SelectionQtyDetail.Where(c => c.CandidateSelectionId == selection.CandidateSelectionId).FirstOrDefault();
                if (selectionQty == null)
                {
                    selectionQty =null ;
                }
                return Json(new { selectionQtyCount = dt, selectionQty = selectionQty });
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult AddQtyDetail(SelectionQtyDetail selectionQtyA)
        {
            try
            {
                var selection = _context.SelectionQtyDetail.Where(c => c.Id == selectionQtyA.Id).FirstOrDefault();
                if (selection == null)
                {
                    SelectionQtyDetail selectionQty = new SelectionQtyDetail();
                    selectionQty.SelectionQty = selectionQtyA.SelectionQty;
                    selectionQty.CandidateSelectionId = selectionQtyA.CandidateSelectionId;
                   selectionQty.EntitySetupId = selectionQtyA.EntitySetupId;
                    _context.SelectionQtyDetail.Add(selectionQty);
                    _context.SaveChanges();
                    return Json(new { status = true });
                }
                else
                {
                    selection.SelectionQty = selectionQtyA.SelectionQty;
                    selection.CandidateSelectionId = selectionQtyA.CandidateSelectionId;
                    selection.EntitySetupId = selectionQtyA.EntitySetupId;
                    _context.SelectionQtyDetail.Update(selection);
                    _context.SaveChanges();
                    return Json(new { status = true });
                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
        public JsonResult DeleteQtyDetail(long id)
        {
            try
            {
                var selection = _context.SelectionQtyDetail.Where(c => c.Id == id).FirstOrDefault();
                if (selection == null)
                {
                    return Json(new { });
                }
                else
                {
                    _context.SelectionQtyDetail.Remove(selection);
                    _context.SaveChanges();
                    return Json(new { status = true });
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult ForEditQty(long id)
        {
            try
            {
                var selection = _context.SelectionQtyDetail.Where(c => c.Id == id).FirstOrDefault();
                if (selection == null)
                {
                    return Json(new { });
                }
                else
                {
                    return Json(new { id=selection.Id,candidateSelectionId=selection.CandidateSelectionId,qty=selection.SelectionQty,visaTrade=selection.EntitySetupId });
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        public JsonResult ForSelectionQtyList(long CandidateSelectionId)
        {
            ViewBag.QtyList = _context.SelectionQtyDetail.Include(c => c.EntitySetup).Where(c=>c.CandidateSelectionId==CandidateSelectionId).ToList();
            return Json(ViewBag.QtyList);
        }
    }

}
