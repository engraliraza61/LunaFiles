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
    public class TestCenterController : Controller
    {
        private readonly lunaContext _context;

        public TestCenterController(lunaContext context)
        {
            _context = context;
        }

        // GET: TestCenter
        public async Task<IActionResult> Index()
        {
            var lunaContext = _context.TestCenter.Include(t => t.City).Include(t => t.Country).Include(t => t.State);
            return View(await lunaContext.ToListAsync());
        }

        // GET: TestCenter/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCenter = await _context.TestCenter
                .Include(t => t.City)
                .Include(t => t.Country)
                .Include(t => t.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCenter == null)
            {
                return NotFound();
            }

            return View(testCenter);
        }

        // GET: TestCenter/Create
        public IActionResult Create()
        {
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name");
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name");
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name");
            return View();
        }

        // POST: TestCenter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] TestCenter testCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", testCenter.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", testCenter.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", testCenter.CityId);
            return View(testCenter);
        }

        // GET: TestCenter/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCenter = await _context.TestCenter.FindAsync(id);
            if (testCenter == null)
            {
                return NotFound();
            }
            var countries = _context.Country.Where(t => t.IsActive == true).ToList();
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", testCenter.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", testCenter.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", testCenter.CityId);
            return View(testCenter);
        }

        // POST: TestCenter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Code,Name,ArabicName,Address,Phone,Email,CountryId,StateId,CityId,IsActive,IsDeleted")] TestCenter testCenter)
        {
            if (id != testCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestCenterExists(testCenter.Id))
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
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", testCenter.CountryId);
            var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId)).ToList();
            ViewData["StateId"] = new SelectList(states, "Id", "Name", testCenter.StateId);
            ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", testCenter.CityId);
            return View(testCenter);
        }

        // GET: TestCenter/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testCenter = await _context.TestCenter
                .Include(t => t.City)
                .Include(t => t.Country)
                .Include(t => t.State)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testCenter == null)
            {
                return NotFound();
            }

            return View(testCenter);
        }

        // POST: TestCenter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var testCenter = await _context.TestCenter.FindAsync(id);
            _context.TestCenter.Remove(testCenter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestCenterExists(long id)
        {
            return _context.TestCenter.Any(e => e.Id == id);
        }
        public JsonResult GetTestCenter()
        {
            var testCenter = _context.TestCenter.Where(e => e.Id != 0).OrderBy(c => c.Name).Distinct().ToList();
            return Json(testCenter);
        }
        public JsonResult AddTestCenter(AssignTestCenterToPermission testCenterToPermission)
        {
            bool status = true;
            long lastId = 0;
            try
            {
                var exist = _context.AssignTestCenterToPermission.Where(c => c.PermissionId == testCenterToPermission.PermissionId && c.TestCenterId == testCenterToPermission.TestCenterId && c.AssignDate==testCenterToPermission.AssignDate).FirstOrDefault();
                if (exist == null)
                {
                    var addTestCenter = _context.AssignTestCenterToPermission.Add(testCenterToPermission);
                    _context.SaveChanges();
                    var last = _context.AssignTestCenterToPermission.Where(c => c.PermissionId == testCenterToPermission.PermissionId && c.TestCenterId == testCenterToPermission.TestCenterId).FirstOrDefault();
                    lastId = last.Id;
                    status = true;
                }
                else
                {
                    status = false;
                }

                
            }
            catch(Exception ex)
            {
                status = false;
            }

            return Json(new {status=status,lastId=lastId });
        }
        public List<AssignTestCenterToPermission> GetAssignTestCenter(int id)
        {
            
                var addTestCenter = _context.AssignTestCenterToPermission.Where(c=>c.PermissionId==id).ToList();
                return addTestCenter;
            
        }
        public JsonResult GetAssignName(int id)
        {
            var permission = _context.AssignTestCenterToPermission.Where(c => c.Id == id).FirstOrDefault();
            var CenterName = _context.TestCenter.Where(c => c.Id == permission.TestCenterId).FirstOrDefault();
            var PermissionName = _context.PermissionRequest.Where(c => c.Id == permission.PermissionId).FirstOrDefault();
            var TestDate = _context.PermissionRequest.Where(c => c.PermissionNumber == PermissionName.PermissionNumber).FirstOrDefault();
            return Json(new {permssionId=permission.Id,centerName=CenterName.Name,permissionNo= PermissionName.PermissionNumber,assignDate=permission.AssignDate });

        }
        public JsonResult DeleteAssignTestCenter(int id)
        {
            bool status = true;
            try
            {
                var permission = _context.AssignTestCenterToPermission.Where(c => c.Id == id).FirstOrDefault();
                _context.AssignTestCenterToPermission.Remove(permission);
                _context.SaveChanges();
                status = true;
                return Json(new { status = status });
            }
            catch(Exception ex)
            {
                status = false;
                return null;
            }
           

        }
    }
}
