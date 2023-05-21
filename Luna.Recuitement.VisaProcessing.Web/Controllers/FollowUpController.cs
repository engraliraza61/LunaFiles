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
    public class FollowUpController : Controller
    {
        private readonly lunaContext _context;

        public FollowUpController(lunaContext context)
        {
            _context = context;
        }

        // GET: FollowUp
        public async Task<IActionResult> Index(long candidateSelectionDetailId)
        {
            ViewBag.CandidateSelectionId = _context.CandidateSelectionDetail
                .Include(c => c.CandidateSelection)
                .Where(c => c.Id == candidateSelectionDetailId).FirstOrDefault().CandidateSelection.Id;
            
            ViewBag.Candidate = _context.CandidateSelectionDetail
                .Include(c => c.CandidateProfile)
                .Where(c => c.Id == candidateSelectionDetailId)
                .FirstOrDefault().CandidateProfile;

            ViewBag.CandidateSelectionDetailId = candidateSelectionDetailId;
            return View(await _context.FollowUp.Where(f => f.CandidateSelectionDetailId == candidateSelectionDetailId).ToListAsync());
        }
        public async Task<IActionResult> forCandidateId(long candidateProfileId)
        {
            if (candidateProfileId == 0)
            {
                return null;
            }
            var candidateSelectionDetail = _context.CandidateSelectionDetail.FirstOrDefault(c => c.CandidateProfileId == candidateProfileId);
            if (candidateSelectionDetail == null)
            {
                return null;
            }
            else
            {
                return RedirectToAction("Index", new { candidateSelectionDetailId = candidateSelectionDetail.Id });
            }
        }


        // GET: FollowUp/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // GET: FollowUp/Create
        public IActionResult Create(long candidateSelectionDetailId)
        {
            var model = new FollowUp();
            model.CandidateSelectionDetailId = candidateSelectionDetailId;
            return View(model);
        }

        // POST: FollowUp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(followUp);
                await _context.SaveChangesAsync();                 
                return RedirectToAction("Index", "FollowUp", new { candidateSelectionDetailId = followUp.CandidateSelectionDetailId });
            }
            return View(followUp);
        }

        // GET: FollowUp/Edit/5
        public async Task<IActionResult> Edit(long? FollowUpId)
        {
            if (FollowUpId == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUp.FindAsync(FollowUpId);
            if (followUp == null)
            {
                return NotFound();
            }
            return View(followUp);
        }

        // POST: FollowUp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CandidateSelectionDetailId,Type,FollowUpDate,Comments,CreatedDate")] FollowUp followUp)
        {
            if (id != followUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(followUp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FollowUpExists(followUp.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "FollowUp", new { candidateSelectionDetailId = followUp.CandidateSelectionDetailId });

            }
            return View(followUp);
        }

        // GET: FollowUp/Delete/5
        public async Task<IActionResult> Delete(long? FollowUpId)
        {
            if (FollowUpId == null)
            {
                return NotFound();
            }

            var followUp = await _context.FollowUp
                .FirstOrDefaultAsync(m => m.Id == FollowUpId);
            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }

        // POST: FollowUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var followUp = await _context.FollowUp.FindAsync(id);
            _context.FollowUp.Remove(followUp);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "FollowUp", new { candidateSelectionDetailId = followUp.CandidateSelectionDetailId });

        }

        private bool FollowUpExists(long id)
        {
            return _context.FollowUp.Any(e => e.Id == id);
        }
    }
}
