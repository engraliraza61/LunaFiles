using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Luna.Recruitment.VisaProcessing.DTO;
using System.Text.RegularExpressions;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class CandidateProfileController : Controller
    {
        private readonly lunaContext _context;

        public CandidateProfileController(lunaContext context)
        {
            _context = context;
        }
        // GET: CandidateProfile
        public async Task<IActionResult> Index()
        {
            _context.Database.SetCommandTimeout(0);
            var lunacontext = _context.CandidateProfile
             .Include(c => c.PlaceOfBirthCity)
             .Include(c => c.PlaceOfBirthCountry)
             .Include(c => c.PreviousNationalityCountry)
             .Include(c => c.CandidateSelectionDetail)
             .ThenInclude(c => c.CandidateSelection)
             .Include(c => c.Passport);
            ViewBag.ActionName = "Index";
            ViewBag.visaPreocess = _context.VisaProcess.Select(c => c.CandidateSelectionDetailId).ToList();
            return View(lunacontext);
            //DataTable dt = new DataTable();
            //SqlCommand command = new SqlCommand();
            //command.CommandType = CommandType.StoredProcedure;
            //command.CommandText = "SP_CandidateProfile";
            //_context.Database.SetCommandTimeout(0);
            //command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(dt);
            //List<CandidateProfile> profileList = new List<CandidateProfile>();
            //foreach (DataRow row in dt.Rows)
            //{
            //    var profile = new CandidateProfile();
            //    profile.Id = row["id"] == DBNull.Value ? 0 :Convert.ToInt64(row["id"]);
            //    profile.FirstName= row["FirstName"] == DBNull.Value ? "" : Convert.ToString(row["FirstName"]);
            //    profile.LastName = row["LastName"] == DBNull.Value ? "" : Convert.ToString(row["LastName"]);
            //    profile.FatherName = row["FatherName"] == DBNull.Value ? "" : Convert.ToString(row["FatherName"]);
            //    profile.DateOfBirth = row["DateOfBirth"] == DBNull.Value ? Convert.ToDateTime(null) : Convert.ToDateTime(row["DateOfBirth"]);
            //    profile.Religion = row["Religion"] == DBNull.Value ? "" : Convert.ToString(row["Religion"]);
            //    profile.CellNumber = row["CellNumber"] == DBNull.Value ? "" : Convert.ToString(row["CellNumber"]);
            //    profile.Cnic = row["Cnic"] == DBNull.Value ? "" : Convert.ToString(row["Cnic"]);
            //    var itemA = profile.Passport;
            //    foreach(var item in row["PassportNo"] == DBNull.Value ? "" : Convert.ToString(row["PassportNo"]))
            //    {
            //        var i = profile.Passport;


            //    }

            //    profileList.Add(profile);
            //}
            //return View(profileList);
        }
        public async Task<IActionResult> Deployed()
        {
            _context.Database.SetCommandTimeout(0);
            var lunacontext = _context.CandidateProfile
             .Include(c => c.PlaceOfBirthCity)
             .Include(c => c.PlaceOfBirthCountry)
             .Include(c => c.PreviousNationalityCountry)
             .Include(c => c.CandidateSelectionDetail)
             .ThenInclude(c => c.CandidateSelection)
             .Include(c => c.Passport)
             .Where(c => c.CandidateSelectionDetail.Any(c => c.VisaProcess.Any(c => c.StatusEntitySetupId == 70)));
            ViewBag.ActionName = "Deployed";
            ViewBag.visaPreocess = _context.VisaProcess.Select(c => c.CandidateSelectionDetailId).ToList();
            return View(await lunacontext.ToListAsync());
        }
        public async Task<IActionResult> UnderProcessed()
        {
            _context.Database.SetCommandTimeout(0);
            var lunacontext =await _context.CandidateProfile
          .Include(c => c.PlaceOfBirthCity)
          .Include(c => c.PlaceOfBirthCountry)
          .Include(c => c.PreviousNationalityCountry)
          .Include(c => c.CandidateSelectionDetail)
          .ThenInclude(c => c.CandidateSelection)
          .Include(c => c.Passport).ToListAsync();
            var lunacontextA =await _context.CandidateProfile
          .Include(c => c.PlaceOfBirthCity)
          .Include(c => c.PlaceOfBirthCountry)
          .Include(c => c.PreviousNationalityCountry)
          .Include(c => c.CandidateSelectionDetail)
          .ThenInclude(c => c.CandidateSelection)
          .Include(c => c.Passport).Where(c => c.CandidateSelectionDetail.Any(c => c.VisaProcess.Any(c => c.StatusEntitySetupId == 70))).ToListAsync();
            var data = lunacontext.Except(lunacontextA).ToList();
            ViewBag.ActionName = "UnderProcessed";
            ViewBag.visaPreocess = _context.VisaProcess.Select(c => c.CandidateSelectionDetailId).ToList();

            var present=_context.CandidateSelectionDetail.Select(x => x.Id).Equals(ViewBag.visaPreocess);
            return View(data);
        }

        public async Task<IActionResult> IndexA(int PageNumber,int pageRow,string fullNameSearch="")
            {
            Regex rgx = new Regex("[^A-Za-z0-9]");

            await Task.Delay(0);
            if (rgx.IsMatch(fullNameSearch.ToString()))
            {
                fullNameSearch = Regex.Replace(fullNameSearch, "[^a-zA-Z0-9_]+", " ");
            }
            fullNameSearch = fullNameSearch.Trim().ToLower();
            fullNameSearch = fullNameSearch.Replace(" ", "");
            Models.ListClass ResponseObject = new Models.ListClass();
            if (PageNumber == 0 && pageRow==0)
            {
                ResponseObject.CurrentPage = 1;
                pageRow = 10;
            }
            else
            {
                ResponseObject.CurrentPage = PageNumber;
            }
            if (fullNameSearch != "")
            {
                int record = (ResponseObject.CurrentPage - 1) * pageRow;
                var passportNo = _context.Passport.Where(c => c.PassportNo == fullNameSearch).FirstOrDefault();
                var candidateData = _context.CandidateProfile.Where(c => c.Id == passportNo.CandidateProfileId).FirstOrDefault();
                var lunaContextA = _context.CandidateProfile
                    .Include(c => c.PlaceOfBirthCity)
                    .Include(c => c.PlaceOfBirthCountry)
                    .Include(c => c.PreviousNationalityCountry)
                      .Include(c => c.CandidateSelectionDetail).ThenInclude(c => c.CandidateSelection).Include(c => c.Passport).
                      Where(c=>(c.Cnic).Contains(candidateData.Cnic)).
                    ToList();
                var count = _context.CandidateProfile.Count();
                ResponseObject.TotalRecords = lunaContextA.Count;
                ResponseObject.Data = lunaContextA;
                ViewBag.StartPage = ResponseObject.CurrentPage;
                ViewBag.pageRow = pageRow;
                ViewBag.totalRecord = count;
                return View(ResponseObject.Data);
            }
            else
            {
                int record = (ResponseObject.CurrentPage - 1) * pageRow;
                var lunaContextA = _context.CandidateProfile
                    .Include(c => c.PlaceOfBirthCity)
                    .Include(c => c.PlaceOfBirthCountry)
                    .Include(c => c.PreviousNationalityCountry)
                      .Include(c => c.CandidateSelectionDetail).ThenInclude(c => c.CandidateSelection)
                    .Include(c => c.Passport).Skip(record).Take(pageRow).ToList();
                var count = _context.CandidateProfile.Count();
                ResponseObject.TotalRecords = lunaContextA.Count;
                ResponseObject.Data = lunaContextA;
                ViewBag.StartPage = ResponseObject.CurrentPage;
                ViewBag.pageRow = pageRow;
                ViewBag.totalRecord = count;
                return View(ResponseObject.Data);
            }
        }
        public async Task<JsonResult> IndexB(int PageNumber)
        {
            return Json(new {PageNumber=PageNumber });
        }
        // GET: CandidateProfile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = await _context.CandidateProfile
                .Include(c => c.PlaceOfBirthCity)
                .Include(c => c.PlaceOfBirthCountry)
                .Include(c => c.PreviousNationalityCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidateProfile == null)
            {
                return NotFound();
            }

            return View(candidateProfile);
        }


        // GET: CandidateProfile/Details/5
        public List<CandidateProfile> ForUpdate(long? id)
        {
            var candidateProfile = _context.CandidateProfile
               .Include(c => c.PlaceOfBirthCity)
               .Include(c => c.PlaceOfBirthCountry)
               .Include(c => c.PreviousNationalityCountry)
               .Include(c => c.Nominee)
               .Include(C => C.Medical).ThenInclude(m => m.Sponser)
               .Include(c => c.Vaccine)
               .ThenInclude(t => t.VaccineTypeEntitySetup)
               .ThenInclude(v => v.VaccineVaccineVariantEntitySetup)
               .Include(c => c.Passport).ThenInclude(p => p.PlaceOfIssueCountry)
               .ThenInclude(b => b.PassportPlaceOfBirthCountry)
               .ThenInclude(b2 => b2.PlaceOfBirthCity)
               .Where(m => m.Id == id).ToList();



            //var candidateProfile = _context.CandidateProfile
            //    .Include(c => c.PlaceOfBirthCity)
            //    .Include(c => c.PlaceOfBirthCountry)
            //    .Include(c => c.PreviousNationalityCountry)
            //    .Where(m => m.Id == id).ToList();
            //ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name", candidateProfile.AgentId);
            //ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            //ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PlaceOfBirthCountryId);
            //ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PreviousNationalityCountryId);

            return candidateProfile;
        }

        // GET: CandidateProfile/Create
        public IActionResult Create()
        {
            var model = new CandidateProfile();
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name");
            ViewData["PlaceOfBirthCityId"] = new SelectList(_context.City.Take(1), "Id", "Name");
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name");
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name");
            //var countries = _context.Country.Where(t => t.IsActive == true);
            //ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oep.CountryId);
            //var states = _context.State.Where(t => countries.Select(t => t.Id).ToArray().Contains(t.CountryId));
            //ViewData["StateId"] = new SelectList(states, "Id", "Name", oep.StateId);
            //ViewData["CityId"] = new SelectList(_context.City.Where(t => states.Select(t => t.Id).ToArray().Contains(t.StateId)), "Id", "Name", oep.CityId);
            return View(model);
        }

        public async Task<IActionResult> Profile(int id = 1)
        {
            if (id == null)
            {
                return NotFound();
            }
            _context.Database.SetCommandTimeout(0);
            var candidateProfile = await _context.CandidateProfile
                .Include(c => c.PlaceOfBirthCity)
                .Include(c => c.PlaceOfBirthCountry)
                .Include(c => c.PreviousNationalityCountry)
                .Include(c => c.Nominee)
                .Include(C => C.Medical).ThenInclude(m=>m.Sponser)
                .Include(c=>c.Vaccine)
                .ThenInclude(t=>t.VaccineTypeEntitySetup)
                .ThenInclude(v=>v.VaccineVaccineVariantEntitySetup)                
                .Include(c => c.Passport).ThenInclude(p=>p.PlaceOfIssueCountry)
                .ThenInclude(b=>b.PassportPlaceOfBirthCountry)
                .ThenInclude(b2=>b2.PlaceOfBirthCity)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name");
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PreviousNationalityCountryId);
            
            if (candidateProfile == null)
            {
                return NotFound();
            }

            if(candidateProfile.Passport!=null && candidateProfile.Passport.Count() <= 0)
            {
                candidateProfile.Passport = new List<Passport>();
            }
            return View(candidateProfile);
        }

        public async Task<ActionResult> ProfileUpdate(int id = 1)
        {

            var candidateProfile = await _context.CandidateProfile
                .Include(c => c.PlaceOfBirthCity)
                .Include(c => c.PlaceOfBirthCountry)
                .Include(c => c.PreviousNationalityCountry)
                .Include(c => c.Nominee)
                .Include(C => C.Medical).ThenInclude(m => m.Sponser)
                .Include(c => c.Vaccine)
                .ThenInclude(t => t.VaccineTypeEntitySetup)
                .ThenInclude(v => v.VaccineVaccineVariantEntitySetup)
                .Include(c => c.Passport).ThenInclude(p => p.PlaceOfIssueCountry)
                .ThenInclude(b => b.PassportPlaceOfBirthCountry)
                .ThenInclude(b2 => b2.PlaceOfBirthCity)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name");
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PreviousNationalityCountryId);

            if (candidateProfile.Passport != null && candidateProfile.Passport.Count() <= 0)
            {
                candidateProfile.Passport = new List<Passport>();
            }
            return View(candidateProfile);
        }
        // POST: CandidateProfile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,FirstName,LastName,ArabicName,DateOfBirth,PlaceOfBirthCountryId,PlaceOfBirthCityId,PreviousNationalityCountryId,MaritalStatus,Gender,Religion,Sect,HomeAddress,PermanentAddress,PhoneNumber,BusinessAddress,BusinessPhone,Domicile,Email,CellNumber,Cnic,FatherName,FatherArabicName,Qualification,HusbandName,HusbandArabicName,Reference,AgentId,CompanyName,IsActive,IsDeleted")] CandidateProfile candidateProfile)
        {
            if (!candidateProfile.DateOfBirth.HasValue)
            {
                ModelState.AddModelError("DateOfBirth", "Enter a valid date of birth.");
            }
            if (!candidateProfile.PlaceOfBirthCountryId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCountryId", "Enter a valid country.");
            }
            if (!candidateProfile.PlaceOfBirthCityId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCityId", "Enter a valid city.");
            }
            if (!String.IsNullOrWhiteSpace(candidateProfile.Cnic))
            {
                var checkCNIC = _context.CandidateProfile.Where(c => c.Cnic == candidateProfile.Cnic);
                if (checkCNIC != null && checkCNIC.Count() > 0)
                {
                    ModelState.AddModelError("Cnic", "duplicate CNIC was found.");
                }
            }
            if (ModelState.IsValid)
            {
                candidateProfile.CreatedBy = "ali";
                candidateProfile.CreatedDate = DateTime.Now;
                _context.Add(candidateProfile);
                await _context.SaveChangesAsync();
                var candidateId = _context.CandidateProfile.Where(c => c.Cnic == candidateProfile.Cnic).FirstOrDefault();
                return RedirectToAction("Profile", "CandidateProfile", new { Id = candidateId.Id });
            }
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name");
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country, "Id", "Code", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country, "Id", "Code", candidateProfile.PreviousNationalityCountryId);
            //ViewData["PlaceOfBirthCityId"] = new SelectList(_context.City.Take(10), "Id", "Name");
            //ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name");
            //ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name");
            return View(candidateProfile);
        }

        // GET: CandidateProfile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = await _context.CandidateProfile.FindAsync(id);

            if (candidateProfile == null)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name", candidateProfile.AgentId);
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name", candidateProfile.PreviousNationalityCountryId);
            return View(candidateProfile);
        }


       
        //// GET: CandidateProfile/Edit/5
        //public List<CandidateProfile> ForUpdate(long? id)
        //{

        //    List<CandidateProfile> RoomList = _context.CandidateProfile.Where(r => r.Id == id).ToList();

        //    //var candidateProfile = _context.CandidateProfile.Find(id);

        //    //ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name",candidateProfile.AgentId);
        //    //ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
        //    //ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name",candidateProfile.PlaceOfBirthCountryId);
        //    //ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name",candidateProfile.PreviousNationalityCountryId);
        //    //List<CandidateProfile> RoomListA = candidateProfile;
        //    return RoomList;
        //}
        // POST: CandidateProfile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, string Title, CandidateProfile candidateProfile)
        {
            if (id != candidateProfile.Id)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name",candidateProfile.AgentId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Code", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Code", candidateProfile.PreviousNationalityCountryId);
            if (!candidateProfile.DateOfBirth.HasValue)
            {
                ModelState.AddModelError("DateOfBirth", "Enter a valid date of birth.");
            }
            if (!candidateProfile.PlaceOfBirthCountryId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCountryId", "Enter a valid country.");
            }
            if (!candidateProfile.PlaceOfBirthCityId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCityId", "Enter a valid city.");
            }
            if (!String.IsNullOrWhiteSpace(candidateProfile.Cnic))
            {
                var checkCNIC = _context.CandidateProfile.Where(c => c.Cnic == candidateProfile.Cnic && c.Id != candidateProfile.Id);
                if (checkCNIC != null && checkCNIC.Count()>0)
                {
                    ModelState.AddModelError("Cnic", "duplicate CNIC was found.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    candidateProfile.ModifiedBy = "ali";
                    candidateProfile.ModifiedDate = DateTime.Now;
                    _context.Update(candidateProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateProfileExists(candidateProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction("Index", "CandidateProfile");
                return RedirectToAction("Profile", "CandidateProfile", new { Id = candidateProfile.Id });

            }

            return RedirectToAction("Profile","CandidateProfile", new { Id = candidateProfile.Id });
            //return RedirectToAction("TabReport", "Reports");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult updateCandidateInfo(long id, CandidateProfile candidateProfile)
        {
            if (id != candidateProfile.Id)
            {
                return NotFound();
            }
            ViewData["AgentId"] = new SelectList(_context.Agent, "Id", "Name", candidateProfile.AgentId);
            ViewData["PlaceOfBirthCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Code", candidateProfile.PlaceOfBirthCountryId);
            ViewData["PlaceOfBirthCityId"] = new SelectList(GetCitiesByCountry(candidateProfile.PlaceOfBirthCountryId.Value), "Id", "Name", candidateProfile.PlaceOfBirthCityId);
            ViewData["PreviousNationalityCountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Code", candidateProfile.PreviousNationalityCountryId);
            if (!candidateProfile.DateOfBirth.HasValue)
            {
                ModelState.AddModelError("DateOfBirth", "Enter a valid date of birth.");
            }
            if (!candidateProfile.PlaceOfBirthCountryId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCountryId", "Enter a valid country.");
            }
            if (!candidateProfile.PlaceOfBirthCityId.HasValue)
            {
                ModelState.AddModelError("PlaceOfBirthCityId", "Enter a valid city.");
            }
            if (!String.IsNullOrWhiteSpace(candidateProfile.Cnic))
            {
                var checkCNIC = _context.CandidateProfile.Where(c => c.Cnic == candidateProfile.Cnic && c.Id != candidateProfile.Id);
                if (checkCNIC != null && checkCNIC.Count() > 0)
                {
                    ModelState.AddModelError("Cnic", "duplicate CNIC was found.");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidateProfile);
                        _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidateProfileExists(candidateProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return View("TabReport", "Reports");
                //return View();

            }

            //return View();
            return View("TabReport", "Reports");
        }


        // GET: CandidateProfile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidateProfile = await _context.CandidateProfile
                .Include(c => c.PlaceOfBirthCity)
                .Include(c => c.PlaceOfBirthCountry)
                .Include(c => c.PreviousNationalityCountry)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidateProfile == null)
            {
                return NotFound();
            }

            return View(candidateProfile);
        }

        // POST: CandidateProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var candidateProfile = await _context.CandidateProfile.FindAsync(id);
            _context.CandidateProfile.Remove(candidateProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CandidateProfileExists(long id)
        {
            return _context.CandidateProfile.Any(e => e.Id == id);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateDependent(Dependent dependent)
        {
            string message = "SUCCESS";
            if (dependent != null)
            {
                if (dependent.Id > 0)
                {
                    _context.Dependent.Update(dependent);
                }
                else
                {
                    _context.Dependent.Add(dependent);
                }
                _context.SaveChanges();
            }      
            return Json(new { Id=dependent.Id, Message = message});
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateMehrum(Mehrum mehrum)
        {
            string message = "SUCCESS";
            if (mehrum != null)
            {
                if (mehrum.Id > 0)
                {
                    _context.Mehrum.Update(mehrum);
                }
                else
                {
                    _context.Mehrum.Add(mehrum);
                }
                _context.SaveChanges();
            }
            return Json(new { massage = message });
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateNominee(Nominee nominee)
        {
            string message = "SUCCESS";
            if (nominee != null)
            {
                if (nominee.Id > 0)
                {
                    _context.Nominee.Update(nominee);
                }
                else
                {
                    _context.Nominee.Add(nominee);
                }
                _context.SaveChanges();
            }
            return Json(new { Message = message });
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateMedical(Medical medical)
        {
            string message = "SUCCESS";
            if (medical.MedicalCenterName.Any())
            {
                var med = _context.MedicalCenter.Where(c => c.Id.ToString() == medical.MedicalCenterName).FirstOrDefault();
                medical.MedicalCenterName = med.Name;
            }
            if (medical != null)
            {
                if (medical.Id > 0)
                {
                    _context.Medical.Update(medical);
                }
                else
                {
                    _context.Medical.Add(medical);
                }
                _context.SaveChanges();
            }
            return Json(new { Id=medical.Id, medicalCenterName=medical.MedicalCenterName, ghccodeNo=medical.GhccodeNo, gccslipNo=medical.GccslipNo, dateExamined=medical.DateExamined, reportExpiryDate=medical.ReportExpiryDate, Message = message });
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetVarientType(EntitySetup entitySetup)
        {
            var vaccineVarient = _context.EntitySetup.Where(c => c.Name == entitySetup.Name).FirstOrDefault();
            return Json(new { vaccineId=vaccineVarient.Id });
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdateVaccine(Vaccine vaccine)
        {
            string message = "SUCCESS";
            if (vaccine != null)
                {
                if (vaccine.Id > 0)
                {
                    _context.Vaccine.Update(vaccine);
                }
                else
                {
                    _context.Vaccine.Add(vaccine);
                }
                _context.SaveChanges();
            }
            var vaccineVarient = _context.EntitySetup.Where(c => c.Id == vaccine.VaccineTypeEntitySetupId).FirstOrDefault();

            return Json(new { Id = vaccine.Id, Message = message, VaccineType=vaccineVarient.Name,Dose=vaccine.VaccineDose,Date=vaccine.VaccineDate,cid=vaccine.CandidateProfileId });
        }[AllowAnonymous]
        [HttpPost]
        public ActionResult showDialog(Vaccine vaccine)
        {
            var vaccineVarient = _context.Vaccine.Where(c => c.Id == vaccine.Id).FirstOrDefault();
            var vaccineVarientA = _context.EntitySetup.Where(c => c.Id == vaccineVarient.VaccineTypeEntitySetupId).FirstOrDefault();

            return Json(new { Id = vaccineVarient.Id,vaccineType= vaccineVarient.VaccineTypeEntitySetupId,dose= vaccineVarient.VaccineDose,vaccineDate= vaccineVarient.VaccineDate,vaccineName=vaccineVarientA.Name});
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult UpdatePassport(Passport passport)
        {
            string message = "SUCCESS";
            if (passport != null)
            {
                if (passport.Id > 0)
                {


                    var checkPassports = _context.Passport.Where(c => c.CandidateProfileId == passport.CandidateProfileId).FirstOrDefault();
                    var checkPassport = _context.Passport.Where(c => c.IssueDate == passport.IssueDate && c.ExpiryDate == passport.ExpiryDate).FirstOrDefault();
                    if (checkPassports != null && checkPassport != null && passport.Id==null)
                    {
                        ViewBag.passportExist = "Passport Exist";
                        return Json(new { passportExist = "Passport Exist" });
                    }
                    else
                    {
                        checkPassports.PlaceOfBirthCountryId = 1;
                        checkPassports.PlaceOfBirthCityId = 1;
                        checkPassports.PlaceOfBirthStateId = 1;
                        checkPassports.PlaceOfBirthCityId = 1;
                        checkPassports.PlaceOfIssueCountryId = 1;
                        checkPassports.IssueDate = passport.IssueDate;
                        checkPassports.ExpiryDate = passport.ExpiryDate;
                        checkPassports.PassportNo = passport.PassportNo;
                        _context.Passport.Update(checkPassports);
                    }
                }
                else
                {
                    var checkPassport = _context.Passport.Where(c => c.PassportNo == passport.PassportNo);
                    if (checkPassport != null && checkPassport.Count() > 0)
                    {
                        ViewBag.passportExist = "Passport Exist";
                        return Json(new { passportExist = "Passport Exist" });
                    }
                    _context.Passport.Add(passport);
                }
                _context.SaveChanges();
            }
            return Json(new { Id = passport.Id, Message = message, passport = passport.PassportNo, issueDate = passport.IssueDate, expiryDate = passport.ExpiryDate });
        }
        public ActionResult DeletePassport(long id)
        {
            string message = "N/A";
            var passport = _context.Nominee.Find(id);
            if (passport != null)
            {
                _context.Nominee.Remove(passport);
                message = "Nominee deleted.";
            }
            else
            {
                message = "Nominee deletion error.";
            }
            _context.SaveChanges();
            return Json(new { Message = message });
        }

        public ActionResult DeletePassportA(long id)
        {
            string message = "N/A";
            var passport = _context.Passport.Find(id);
            if (passport != null)
            {
                _context.Passport.Remove(passport);
                message = "Passport deleted.";
            }
            else
            {
                message = "Passport deletion error.";
            }
            _context.SaveChanges();
            return Json(new { Message = message });
        }
        public ActionResult DeleteMedical(long id)
        {
            string message = "N/A";
            var medical = _context.Medical.Find(id);
            if (medical != null)
            {
                _context.Medical.Remove(medical);
                message = "Medical record deleted.";
            }
            else
            {
                message = "Medical record deletion error.";
            }
            _context.SaveChanges();
            return Json(new { Message = message });
        }
        public ActionResult DeleteVaccine(long id)
        {
            string message = "N/A";
            var vaccine = _context.Vaccine.Find(id);
            if (vaccine != null)
            {
                _context.Vaccine.Remove(vaccine);
                message = "vaccine record deleted.";
            }
            else
            {
                message = "vaccine record deletion error.";
            }
            _context.SaveChanges();
            return Json(new { Message = message,id=id});
        }
        public ActionResult DeleteNominee(long id)
        {
            string message = "N/A";
            var vaccine = _context.Nominee.Find(id);
            if (vaccine != null)
            {
                _context.Nominee.Remove(vaccine);
                message = "Nominee record deleted.";
            }
            else
            {
                message = "Nominee record deletion error.";
            }
            _context.SaveChanges();
            return Json(new { Message = message, id = id });
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetCountry()
        {
            var countries = _context.Country.Where(c => c.IsActive == true).OrderBy(c => c.Name).ToList();
            return Json(countries);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetMedicalCenter()
        {
            var medicalCenters = _context.MedicalCenter.OrderBy(c => c.Name).ToList();
            return Json(medicalCenters);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAgent()
        {
            var Agent = _context.Agent.Where(c => c.IsActive == true).OrderBy(c => c.Name).ToList();
            return Json(Agent);
        }
        [HttpPost]
        public JsonResult getMedicalCenterValue(string medicalCenterName)
        {
            var MedicalCenter = _context.MedicalCenter.Where(c => c.Name == medicalCenterName).FirstOrDefault();
            return Json(MedicalCenter);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetAgnet()
        {
            var Agent = _context.Agent.Where(c => c.IsActive == true).OrderBy(c => c.Name).ToList();
            return Json(Agent);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetState(int CountryId = 0)
        {
            List<State> states = new List<State>();
            if (CountryId == 0)
            {
                states = _context.State.OrderBy(s => s.Name).ToList();
            }
            else
            {
                states = _context.State.Where(s => s.CountryId == CountryId).OrderBy(s => s.Name).ToList();
            }
            return Json(states);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetCityByCountry(long CountryId = 2)
        {
            List<City> cities = new List<City>();
            List<State> states = new List<State>();
            //if (CountryId == 0)
            //{
            //    states = null;
            //    cities = null;
            //}
            //else
            //{
            states = _context.State.Where(s => s.CountryId == CountryId || CountryId == 0).ToList();
            List<long> stateIds = states.Select(st => st.Id).ToList();
            cities = _context.City.Where(c => stateIds.Contains(c.StateId)).Take(1000).ToList();
            //}
            return Json(cities);
        }
        [AllowAnonymous]
        [HttpGet]
        public List<City> GetCitiesByCountry(long CountryId = 2)
        {
            List<City> cities = new List<City>();
            List<State> states = new List<State>();
            if (CountryId == 0)
            {
                states = null;
                cities = null;
            }
            else
            {
                states = _context.State.Where(s => s.CountryId == CountryId).ToList();
                List<long> stateIds = states.Select(st => st.Id).ToList();
                cities = _context.City.Where(c => stateIds.Contains(c.StateId)).Take(1000).OrderBy(c => c.Name).ToList();
            }
            return cities;
        }
      
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetMedicalCenters()
        {
            List<DropdownModel> medicalCenters = null;
            //medicalCenters = new List<MedicalCenter>() { 
            //    new MedicalCenter() { Id=1,Name="Center 1",CityId=1,GHCCode="GHC 1" },
            //new MedicalCenter() { Id=2,Name="Center 2",CityId=1,GHCCode="GHC 2" },
            //new MedicalCenter() { Id=3,Name="Center 3",CityId=1,GHCCode="GHC 3" },
            //new MedicalCenter() { Id=4,Name="Center 4",CityId=1,GHCCode="GHC 4" }};
            medicalCenters = _context.MedicalCenter
                .Include(t=>t.City)
                .Select(t=>new DropdownModel() { Id=t.Id,Name=t.Name, City=t.City.Name })
                .OrderBy(t => t.Name).ToList();            

            return Json(medicalCenters);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetVaccineTypes()
        {
            List<DropdownModel> vaccineTypes = null;

            vaccineTypes = _context.EntitySetup.Where(e=>e.EntityType.Code=="08")
                .Select(t => new DropdownModel() { Id = t.Id, Name = t.Name})
                .OrderBy(t => t.Name).ToList();
            return Json(vaccineTypes);
        }
        public async Task<JsonResult> SaveSelection(List<long> SelectedCandidates, long candidateSelectionId)
        {
            Console.WriteLine("methodStart");
            bool status = false;
            var result = new List<long>();
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
                        .Where(c => c.CandidateSelectionId == candidateSelectionId && !SelectedCandidates.Contains(c.CandidateProfileId));
                    foreach (var item in selectionDetailToDelete)
                    {
                        _context.FollowUp.RemoveRange(item.FollowUp);
                        _context.CandidatejobDetail.RemoveRange(item.CandidatejobDetail);
                        _context.VisaProcess.RemoveRange(item.VisaProcess);
                    }
                    _context.CandidateSelectionDetail.RemoveRange(selectionDetailToDelete);

                    //Save new records for IDs which are in parameter list but not in Selection Details DB list
                    foreach (var candidateProfileId in SelectedCandidates)
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
                    foreach (var candidateId in SelectedCandidates)
                    {
                        CandidateSelectionDetail newSelection = new CandidateSelectionDetail();
                        newSelection.CandidateProfileId = candidateId;
                        newSelection.CandidateSelectionId = candidateSelectionId;
                        _context.CandidateSelectionDetail.Add(newSelection);

                    }
                }
                await _context.SaveChangesAsync();
                ViewBag.SelectedList = SelectedCandidates;
                result = selectionDetail.Select(c => c.CandidateProfileId).ToList();
                ViewBag.SelectionDetail = result;
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status, SelectedCandidates = SelectedCandidates, selectionDetail = result, candidateSelectionId = candidateSelectionId });

        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetVaccineVariants()
        {
            List<DropdownModel> vaccineVariants = null;

            vaccineVariants = _context.EntitySetup.Where(e => e.EntityType.Code == "09")
                .Select(t => new DropdownModel() { Id = t.Id, Name = t.Name })
                .OrderBy(t => t.Name).ToList();
            return Json(vaccineVariants);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult IsValidCNIC(string Cnic,long Id)
        {
            bool status = true;
            if (!String.IsNullOrWhiteSpace(Cnic))
            {
                var checkCNIC = _context.CandidateProfile.Where(c => c.Cnic == Cnic && c.Id != Id);
                if (checkCNIC != null && checkCNIC.Count() > 0)
                {
                    return Json(false);
                }
            }
            return Json(status);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetPassportInformation(Passport passport)
        {
            List<Passport> passports = null;

            passports = _context.Passport.Where(e => e.PassportNo == passport.PassportNo).ToList();
            return Json(passports);
        }
        
        public JsonResult GetSelection()
        {
            var CandidateSelection = _context.CandidateSelection.Where(e => e.SelectionGroup != null).OrderBy(c => c.SelectionGroup).ToList();
            return Json(CandidateSelection);
        }
        public JsonResult GetSelectionWithSelectionId(long  id)
        {
            var CandidateSelection = _context.CandidateSelection.Where(e => e.SelectionGroup != null).OrderBy(c => c.SelectionGroup).ToList();
            var SelectionForCandidate = _context.CandidateSelectionDetail.Where(c => c.CandidateProfileId == id).FirstOrDefault();
            ViewBag.SelectionForCandidate = SelectionForCandidate.CandidateSelectionId;
            return Json(new { CandidateSelection = CandidateSelection, SelectForId = SelectionForCandidate.CandidateSelectionId });
        }

        public JsonResult GetSponsor(long selectionId)
        {
            var CandidateSelection = _context.Sponser.Where(e => e.Name != null).OrderBy(c => c.Name).ToList();
            var sponsorId = _context.CandidateSelection.Where(c => c.Id == selectionId).Select(c=>c.SponserId);
            return Json(new { CandidateSelection = CandidateSelection, SponsorId = sponsorId });
        }



        public JsonResult GetFileNo()
        {
            var OepvisaDemand = _context.OepvisaDemand.Where(e=>e.Code!=null).ToList();
            return Json(OepvisaDemand);
        }
        public JsonResult GetPassport()
        {
            var OepvisaDemand = _context.Passport.Where(e=>e.PassportNo!=null).ToList();
            return Json(OepvisaDemand);
        }
        public JsonResult GetAgents()
        {
            var Agents = _context.Agent.Include(c => c.City).ToList();
            return Json(Agents);
        }
    }

}
