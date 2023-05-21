using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class EntitySetupController : Controller
    {
        private readonly lunaContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EntitySetupController(lunaContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: EntitySetup
 
        public async Task<IActionResult> Index(long? entityTypeId)
        {
            if (entityTypeId == null)
            {
                return NotFound();
            }
            ViewBag.EntityTypeName = _context.EntityType.Where(t => t.Id == entityTypeId).FirstOrDefault().Description;
            ViewBag.EntityTypeId = entityTypeId;
            var lunaContext = _context.EntitySetup.Include(e => e.EntityType).Where(e=>e.EntityTypeId== entityTypeId);
            return View(await lunaContext.ToListAsync());
        }

        // GET: EntitySetup/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entitySetup = await _context.EntitySetup
                .Include(e => e.EntityType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entitySetup == null)
            {
                return NotFound();
            }

            return View(entitySetup);
        }

        // GET: EntitySetup/Create
        public IActionResult Create(long entityTypeId,string entityTypeName)
        {
            var model = new EntitySetup();
            model.EntityTypeId = entityTypeId;
            ViewData["EntityTypeName"] = entityTypeName;
            ViewData["EntityTypeId"] = new SelectList(_context.EntityType, "Id", "Name",entityTypeId);
            return View(model);
        }

        // POST: EntitySetup/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EntityTypeId,Code,Name,ArabicName,Description,IsActive,IsDeleted")] EntitySetup entitySetup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entitySetup);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { EntityTypeId = entitySetup.EntityTypeId });
            }
            ViewData["EntityTypeId"] = new SelectList(_context.EntityType, "Id", "Name", entitySetup.EntityTypeId);
            return View(entitySetup);
        }

        // GET: EntitySetup/Edit/5
        public async Task<IActionResult> Edit(long? id,string entityTypeName)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["EntityTypeName"] = entityTypeName;
            var entitySetup = await _context.EntitySetup.FindAsync(id);
            if (entitySetup == null)
            {
                return NotFound();
            }
            ViewData["EntityTypeId"] = new SelectList(_context.EntityType, "Id", "Id", entitySetup.EntityTypeId);
            return View(entitySetup);

        }

        // POST: EntitySetup/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,EntityTypeId,Code,Name,ArabicName,Description,IsActive,IsDeleted")] EntitySetup entitySetup)
        {
            if (id != entitySetup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entitySetup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntitySetupExists(entitySetup.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", new { EntityTypeId = entitySetup.EntityTypeId });
            }
            ViewData["EntityTypeId"] = new SelectList(_context.EntityType, "Id", "Id", entitySetup.EntityTypeId);
            return View(entitySetup);
        }

        // GET: EntitySetup/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entitySetup = await _context.EntitySetup
                .Include(e => e.EntityType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entitySetup == null)
            {
                return NotFound();
            }

            return View(entitySetup);
        }

        // POST: EntitySetup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var entitySetup = await _context.EntitySetup.FindAsync(id);
            _context.EntitySetup.Remove(entitySetup);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { EntityTypeId = entitySetup.EntityTypeId });
        }

        private bool EntitySetupExists(long id)
        {
            return _context.EntitySetup.Any(e => e.Id == id);
        }
    }
}
