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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Globalization;
using System.Threading;
using Luna.Recruitment.VisaProcessing.Data.DTO;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    //[Authorize(Roles = "Administrator,HR")]
    public class OEPVisaDemandController : Controller
    {
        private readonly lunaContext _context;

        public OEPVisaDemandController(lunaContext context)
        {
            _context = context;
        }
      

        // GET: OEPVisaDemand
        public async Task<IActionResult> Index()
        {
            var lunaContext = _context.OepvisaDemand
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandStatusEntitySetup)
                .Include(o => o.Sponser)
                .Include(o => o.OepvisaDemandDetail);
            ViewBag.StatusSummary = new List<FileStatusDTO>();
            return View(await lunaContext.ToListAsync());
        }

        // GET: OEPVisaDemand/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oepvisaDemand = await _context.OepvisaDemand
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandStatusEntitySetup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oepvisaDemand == null)
            {
                return NotFound();
            }

            return View(oepvisaDemand);
        }

        // GET: OEPVisaDemand/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name");
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Country.Where(t => t.IsActive == true), "Id", "Name");
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name");
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["OepvisaDemandStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name");
            return View();
        }

        // POST: OEPVisaDemand/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OepvisaDemand oepvisaDemand)
        {
            if (ModelState.IsValid)
            {
                var date = ConvertDateCalendar(Convert.ToDateTime(oepvisaDemand.VisaNumberDate), "Hijri", "en-US", (int)oepvisaDemand.ExpiryDays);
                oepvisaDemand.VisaExpire = date;
                var nextSequence = GetNextSequence(oepvisaDemand.Oepid.Value);
                oepvisaDemand.Code = nextSequence;
                _context.Add(oepvisaDemand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "OEPVisaDemand", new { id = oepvisaDemand.Id });
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", oepvisaDemand.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name", oepvisaDemand.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name", oepvisaDemand.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemand.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name", oepvisaDemand.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["OepvisaDemandStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name", oepvisaDemand.OepvisaDemandStatusEntitySetupId);
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE), "Id", "Name", oepvisaDemand.OepvisaDemandStatusEntitySetupId);

            return View(oepvisaDemand);
        }

        // GET: OEPVisaDemand/Edit/5
        public async Task<IActionResult> Edit(long? id,string date=null)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.expirydate = date;
            var oepvisaDemand = _context.OepvisaDemand
                .Include(t => t.OepvisaDemandDetail)
                .ThenInclude(t => t.JobTypeEntitySetup)
                .FirstOrDefault(t => t.Id == id);
            if (oepvisaDemand == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", oepvisaDemand.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name", oepvisaDemand.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name", oepvisaDemand.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemand.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name", oepvisaDemand.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["OepvisaDemandStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name", oepvisaDemand.OepvisaDemandStatusEntitySetupId);
            ViewBag.JobTypeEntitySetupId = _context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE).ToList();

            return View(oepvisaDemand);
        }

        // POST: OEPVisaDemand/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public static string ConvertDateCalendar(DateTime DateConv, string Calendar, string DateLangCulture,int ExpiryDays)
        {
            DateTimeFormatInfo DTFormat;
            DateLangCulture = DateLangCulture.ToLower();
            /// We can't have the hijri date writen in English. We will get a runtime error

            if (Calendar == "Hijri" && DateLangCulture.StartsWith("en-"))
            {
                DateLangCulture = "ar-sa";
            }

            /// Set the date time format to the given culture
            DTFormat = new System.Globalization.CultureInfo(DateLangCulture, false).DateTimeFormat;

            /// Set the calendar property of the date time format to the given calendar
            switch (Calendar)
            {
                case "Hijri":
                    DTFormat.Calendar = new System.Globalization.HijriCalendar();
                    break;

                case "Gregorian":
                    DTFormat.Calendar = new System.Globalization.GregorianCalendar();
                    break;
                default:
                    return "";
            }

            /// We format the date structure to whatever we wadnt
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            var Dates = DateConv.Date.AddDays(ExpiryDays);
            var Date = Dates.ToString("yyyy/MM/dd");
            //var Dates = DateConv.Date.ToString("f", DTFormat);
            return Date.ToString();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, OepvisaDemand oepvisaDemand)
        {
            if (id != oepvisaDemand.Id)
            {
                return NotFound();
            }
            string date = "";
            if (ModelState.IsValid)
            {
                try
                {
                    date = ConvertDateCalendar(Convert.ToDateTime(oepvisaDemand.VisaNumberDate), "Hijri", "en-US",(int)oepvisaDemand.ExpiryDays);
                    oepvisaDemand.VisaExpire = date;
                    _context.Update(oepvisaDemand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OepvisaDemandExists(oepvisaDemand.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", oepvisaDemand.ClientId);
            ViewData["Oepid"] = new SelectList(_context.Oep, "Id", "Name", oepvisaDemand.Oepid);
            ViewData["SponserId"] = new SelectList(_context.Sponser, "Id", "Name", oepvisaDemand.SponserId);
            var countries = _context.Country.Where(t => t.IsActive == true);
            ViewData["CountryId"] = new SelectList(countries, "Id", "Name", oepvisaDemand.CountryId);
            ViewData["CounslateId"] = new SelectList(_context.Counslate, "Id", "Name", oepvisaDemand.CounslateId);
            ViewData["FileType"] = new SelectList(AppLists.FileTypes, "Value", "Text");
            ViewData["VisaType"] = new SelectList(AppLists.VisaTypes, "Value", "Text");
            ViewData["OepvisaDemandStatusEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.VISA_DEMAND_STATUS_TEXT), "Id", "Name", oepvisaDemand.OepvisaDemandStatusEntitySetupId);
            ViewData["JobTypeEntitySetupId"] = new SelectList(_context.EntitySetup.Where(t => t.EntityType.Name == Common.JOB_TYPE), "Id", "Name", 1);
            return RedirectToAction("Edit", new { id = oepvisaDemand.Id, date= date });
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult AddDemandDetail(OepvisaDemandDetail detail)
        {
            string status = "0";
            if (ModelState.IsValid)
            {
                try
                {
                    var visaDemand = _context.OepvisaDemand.Find(detail.OepvisaDemandId);
                    var visaDemandDetail = _context.OepvisaDemandDetail.Where(t => t.OepvisaDemandId == detail.OepvisaDemandId);
                    var totalQuantity = visaDemandDetail.Sum(t => t.Quantity);
                    totalQuantity = totalQuantity + detail.Quantity;
                    visaDemand.Quantity = totalQuantity;
                    _context.Update(visaDemand);
                    _context.OepvisaDemandDetail.Add(detail);
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteDemandDetail(long? id)
        {

            string status = "0";
            try
            {
                var demandDetail = _context.OepvisaDemandDetail
                .Include(t => t.OepvisaDemand).Where(c => c.Id == 100)
                .FirstOrDefault();
                //var demandDetail = _context.OepvisaDemandDetail.Find(id);
                //if (demandDetail != null)
                //{
                //    var visaDemand = _context.OepvisaDemand.Find(demandDetail.OepvisaDemandId);
                //    visaDemand.Quantity = visaDemand.Quantity - demandDetail.Quantity;
                //    _context.Update(visaDemand);
                //    _context.OepvisaDemandDetail.Remove(demandDetail);
                //    _context.SaveChanges();
                //    status = $"Record deleted successfully.";
                //}
            }
            catch (Exception ex)
            {
                status = $"Error occured while saving demand. ({ex.Message})";
                throw;
            }
            return Json(new { Message = status });
        }[AllowAnonymous]
        [HttpPost]
        public ActionResult DeleteDemandDetails(long? id)
        {

            string status = "0";
            try
            {
                var demandDetail = _context.OepvisaDemandDetail.Where(c => c.Id == id).FirstOrDefault();
                if (demandDetail != null)
                {
                    var visaDemand = _context.OepvisaDemand.Where(c => c.Id == demandDetail.OepvisaDemandId).FirstOrDefault();
                    visaDemand.Quantity = visaDemand.Quantity - demandDetail.Quantity;
                    _context.Update(visaDemand);
                    _context.OepvisaDemandDetail.Remove(demandDetail);
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

        // GET: OEPVisaDemand/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oepvisaDemand = await _context.OepvisaDemand
                .Include(o => o.Counslate)
                .Include(o => o.OepvisaDemandStatusEntitySetup)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oepvisaDemand == null)
            {
                return NotFound();
            }

            return View(oepvisaDemand);
        }

        // POST: OEPVisaDemand/Delete/5
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var oepvisaDemand = await _context.OepvisaDemand.FindAsync(id);
            _context.OepvisaDemand.Remove(oepvisaDemand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OepvisaDemandExists(long id)
        {
            return _context.OepvisaDemand.Any(e => e.Id == id);
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
        public async Task<IActionResult> Jobs(long id)
        {
            var demandJobs = _context.OepvisaDemandDetail.Where(x => x.OepvisaDemandId == id);
            return View(await demandJobs.ToListAsync());
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetJobsByDemandId(long oepVisaDemandId)
        {
            var JobTypes = _context.OepvisaDemandDetail.Include(d => d.JobTypeEntitySetup)
                .Where(t => t.OepvisaDemandId == oepVisaDemandId && t.JobTypeEntitySetup.Id == t.JobTypeEntitySetupId)
                .Select(t => new { Id = t.JobTypeEntitySetupId, Name = t.JobTypeEntitySetup.Name }).ToList();
            var statusType = _context.VisaProcess.Where(c => c.OepvisaDemandId == oepVisaDemandId).Select(a => new { RemarksStatus = a.StatusRemarks }).ToList();
            return Json(JobTypes);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetConslate(long oepVisaDemandCode,string passportNo)
        {
            var oep=_context.OepvisaDemand.Where(c => c.Id == oepVisaDemandCode).FirstOrDefault();
            var conslateName = _context.Counslate.Where(c => c.Id == oep.CounslateId).Select(c => c.Name);
            var passportCandidate = _context.Passport.Where(c => c.PassportNo == passportNo).FirstOrDefault();
            var DateOfBirth = _context.CandidateProfile.Where(c => c.Id == passportCandidate.CandidateProfileId).FirstOrDefault().DateOfBirth;
            DateTime dateTimeToday = DateTime.UtcNow;
            var firstDay = new DateTime(1, 1, 1);
            TimeSpan difference = dateTimeToday.Subtract(DateOfBirth.Value);
            int totalYears = (firstDay + difference).Year - 1;
            return Json(new { conslateName= conslateName ,age= totalYears });
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetEntityType()
        {
            var JobTypes = _context.EntitySetup.Where(c => c.EntityTypeId == 6).Select(a => new { Id = a.Id, Name = a.Name }).ToList();
            return Json(JobTypes);
        }
        [AllowAnonymous]
        [HttpGet]
        public JsonResult GetEntityTypeForQty()
        {
            var JobTypes = _context.EntitySetup.Where(c => c.EntityTypeId == 5).Select(a => new { Id = a.Id, Name = a.Name }).ToList();
            return Json(JobTypes);
        }

        public JsonResult VisaQuantity(long oepVisaDemandId, long visaTradeEntitySetupId)
        {
            if (oepVisaDemandId != 0 || visaTradeEntitySetupId != 0)
            {
                var visaQuantity = new OepvisaDemandDetail();
                if (oepVisaDemandId != null)
                {
                    visaQuantity = _context.OepvisaDemandDetail
              .Where(t => t.OepvisaDemandId == oepVisaDemandId && t.JobTypeEntitySetup.Id == visaTradeEntitySetupId).FirstOrDefault();
                }
               if(oepVisaDemandId == null)
               {
                    visaQuantity = _context.OepvisaDemandDetail
               .Where(t =>  t.JobTypeEntitySetup.Id == visaTradeEntitySetupId).FirstOrDefault();
               }
                var expiryDays = _context.OepvisaDemand.Where(c => c.Id == oepVisaDemandId).FirstOrDefault();
                int totalVisaQuantity = 0;
                if (visaQuantity != null && visaQuantity.Quantity != null)
                    totalVisaQuantity = visaQuantity.Quantity.Value;
                var visaIssuedQuantity = _context.VisaProcess
                    .Where(t => t.OepvisaDemandId == oepVisaDemandId && t.VisaTradeEntitySetupId == visaTradeEntitySetupId).Count();
                int balance = totalVisaQuantity - visaIssuedQuantity;
                string date = "";
                if (expiryDays != null)
                {
                    if (expiryDays.ExpiryDays == null)
                    {
                        date = null;
                    }
                    else
                    {
                        date = ConvertDateCalendar(Convert.ToDateTime(expiryDays.VisaNumberDate), "Hijri", "en-US", (int)expiryDays.ExpiryDays);
                    }
                }
                

                return Json(new { Balance = balance, OEPVisaDemandId = oepVisaDemandId, OEPVisaDemandDetailId = 0, expiryDay = date, totalVisaQuantity= totalVisaQuantity, visaIssuedQuantity= visaIssuedQuantity });
            }
            return Json(new { });
            
        }
        public JsonResult tradePresent(long oepVisaDemandId, long visaTradeEntitySetupId)
        {
            if (oepVisaDemandId != 0 || visaTradeEntitySetupId != 0)
            {
                bool status = false;
                var visaProcess=_context.VisaProcess.Where(c => c.OepvisaDemandId == oepVisaDemandId && c.VisaTradeEntitySetupId == visaTradeEntitySetupId).FirstOrDefault();
                if (visaProcess != null)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                

                return Json(new { status=status });
            }
            return Json(new { });
            
        }
        public JsonResult selectionQuantity(long oepVisaDemandId, long visaTradeEntitySetupId)
        {
            var selectionDetail = _context.CandidateSelectionDetail.Where(c => c.Id == oepVisaDemandId).FirstOrDefault();
            if (oepVisaDemandId != 0 || visaTradeEntitySetupId != 0)
            {
                var visaQuantity = new SelectionQtyDetail();
                var visaQuantityA = new OepvisaDemand();
                if (oepVisaDemandId != null)
                {
                    visaQuantity = _context.SelectionQtyDetail
              .Where(t => t.CandidateSelectionId == selectionDetail.CandidateSelectionId && t.EntitySetupId == visaTradeEntitySetupId).FirstOrDefault();
                }
                var visaProcess = _context.VisaProcess.Where(c => c.VisaTradeEntitySetupId == visaTradeEntitySetupId && c.CandidateSelectionDetailId==oepVisaDemandId).Count();
                string selectionLimit=null;
                if (visaQuantity == null)
                {
                    selectionLimit = null;
                }
                else
                {
                    selectionLimit = (visaQuantity.SelectionQty - visaProcess).ToString();
                }
                return Json(new { selectionLimit = selectionLimit,selectedCandidate=visaProcess });
            }
            return Json(new { });
        }

        public JsonResult GetSelectedCandidate(long selectionDetailId, long visaTradeEntitySetupId)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "SP_SelectedCandidate";
            command.Parameters.Add(new SqlParameter("@CandidateSelectionId", selectionDetailId));
            command.Parameters.Add(new SqlParameter("@SelectionTrade_EntitySetupId", visaTradeEntitySetupId));
            //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
            _context.Database.SetCommandTimeout(0);
            command.Connection = (SqlConnection)_context.Database.GetDbConnection();
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dt);
            var visaQuantity = _context.SelectionQtyDetail
              .Where(t => t.CandidateSelectionId == selectionDetailId && t.EntitySetupId == visaTradeEntitySetupId).FirstOrDefault();
            var selectionLimit = "";
            var totalQuantity = "";
            if (visaQuantity == null)
            {
                selectionLimit = null;
                totalQuantity = null;
            }
            else
            {
                selectionLimit = (visaQuantity.SelectionQty - dt.Rows.Count).ToString();
                totalQuantity = visaQuantity.SelectionQty.ToString();
            }
          
            return Json(new { selectionLimit = selectionLimit,selectedCandidate=dt.Rows.Count,totalQuantity=totalQuantity });
        }
        [Authorize(Roles = "Management")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

        public JsonResult GetInformations(string visaNumber)
        {
            try
            {
                if (visaNumber == null)
                {
                    visaNumber = "0000";
                }
                List<FileStatusDTO> ENumberModel = new List<FileStatusDTO>();
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "FileSummary";
                command.Parameters.Add(new SqlParameter("@Code", visaNumber));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    var eNumber = new FileStatusDTO();

                    eNumber.FileID = row["FileID"] == DBNull.Value ? "" : Convert.ToString(row["FileID"]);
                    eNumber.Total = row["Total"] == DBNull.Value ? "" : Convert.ToString(row["Total"]);
                    eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                    eNumber.ArabicName = row["ArabicName"] == DBNull.Value ? "" : Convert.ToString(row["ArabicName"]);
                    eNumber.MedicallyUnfit = row["Medically Unfit"] == DBNull.Value ? "0" : Convert.ToString(row["Medically Unfit"]);
                    eNumber.MedicallyFIT = row["Medically FIT"] == DBNull.Value ? "0" : Convert.ToString(row["Medically FIT"]);
                    eNumber.MedicalUnderProcess = row["Medical Under Process"] == DBNull.Value ? "0" : Convert.ToString(row["Medical Under Process"]);
                    eNumber.Biometric_M_R_onlineProcess = row["Biometric/M.R online Process"] == DBNull.Value ? "0" : Convert.ToString(row["Biometric/M.R online Process"]);
                    eNumber.PendingDocuments = row["Pending Documents"] == DBNull.Value ? "0" : Convert.ToString(row["Pending Documents"]);
                    eNumber.ReadyforSubmission = row["Ready for Submission"] == DBNull.Value ? "0" : Convert.ToString(row["Ready for Submission"]);
                    eNumber.SubmittedforVisaStamping = row["Submitted for Visa Stamping"] == DBNull.Value ? "0" : Convert.ToString(row["Submitted for Visa Stamping"]);
                    eNumber.VisaStamped = row["Visa Stamped"] == DBNull.Value ? "0" : Convert.ToString(row["Visa Stamped"]);
                    eNumber.Declined = row["Declined"] == DBNull.Value ? "0" : Convert.ToString(row["Declined"]);
                    eNumber.VisaExpired = row["Visa Expired"] == DBNull.Value ? "0" : Convert.ToString(row["Visa Expired"]);
                    eNumber.Deployed = row["Deployed"] == DBNull.Value ? "0" : Convert.ToString(row["Deployed"]);
                    eNumber.DegreeAttestationUnderProcess = row["Degree Attestation Under Process"] == DBNull.Value ? "0" : Convert.ToString(row["Degree Attestation Under Process"]);
                    eNumber.PassportApplied = row["Passport Applied"] == DBNull.Value ? "0" : Convert.ToString(row["Passport Applied"]);
                    eNumber.PassportApplied = row["Passport Applied"] == DBNull.Value ? "0" : Convert.ToString(row["Passport Applied"]);
                    eNumber.PassportReturned = row["Passport Returned"] == DBNull.Value ? "0" : Convert.ToString(row["Passport Returned"]);
                    eNumber.Objection = row["Objection"] == DBNull.Value ? "0" : Convert.ToString(row["Objection"]);
                    eNumber.StoppedbyManagement = row["Stopped by Management"] == DBNull.Value ? "0" : Convert.ToString(row["Stopped by Management"]);
                    eNumber.StoppedbyCompany = row["Stopped by Company"] == DBNull.Value ? "0" : Convert.ToString(row["Stopped by Company"]);
                    eNumber.UnderSelection = row["Under Selection"] == DBNull.Value ? "0" : Convert.ToString(row["Under Selection"]);
                    eNumber.DemandRequired = row["Demand Required"] == DBNull.Value ? "0" : Convert.ToString(row["Demand Required"]);
                    eNumber.VisaCancelled = row["Visa Cancelled"] == DBNull.Value ? "0" : Convert.ToString(row["Visa Cancelled"]);
                    eNumber.WakalaAwaited = row["Wakala Awaited"] == DBNull.Value ? "0" : Convert.ToString(row["Wakala Awaited"]);
                    eNumber.Protector = row["Protector"] == DBNull.Value ? "0" : Convert.ToString(row["Protector"]);
                    eNumber.NoShow = row["No Show"] == DBNull.Value ? "0" : Convert.ToString(row["No Show"]);
                    eNumber.FlightCancelled = row["Flight Cancelled"] == DBNull.Value ? "0" : Convert.ToString(row["Flight Cancelled"]);
                    eNumber.ReadyToPrint = row["Ready To Print"] == DBNull.Value ? "0" : Convert.ToString(row["Ready To Print"]);
                    eNumber.Available = (Convert.ToInt32(eNumber.Total) - (Convert.ToInt32(eNumber.MedicallyUnfit) 
                        +Convert.ToInt32(eNumber.MedicallyFIT) + Convert.ToInt32(eNumber.MedicalUnderProcess) + Convert.ToInt32(eNumber.Biometric_M_R_onlineProcess) + Convert.ToInt32(eNumber.PendingDocuments) + Convert.ToInt32(eNumber.ReadyforSubmission) + Convert.ToInt32(eNumber.SubmittedforVisaStamping) + Convert.ToInt32(eNumber.VisaStamped) + Convert.ToInt32(eNumber.Declined) + Convert.ToInt32(eNumber.VisaExpired) + Convert.ToInt32(eNumber.Deployed) + Convert.ToInt32(eNumber.DegreeAttestationUnderProcess) + Convert.ToInt32(eNumber.PassportApplied) + Convert.ToInt32(eNumber.PassportReturned) + Convert.ToInt32(eNumber.Objection) + Convert.ToInt32(eNumber.StoppedbyCompany) + Convert.ToInt32(eNumber.StoppedbyManagement) + Convert.ToInt32(eNumber.UnderSelection) + Convert.ToInt32(eNumber.DemandRequired) + Convert.ToInt32(eNumber.VisaCancelled) + Convert.ToInt32(eNumber.WakalaAwaited) + Convert.ToInt32(eNumber.Protector) + Convert.ToInt32(eNumber.NoShow) - Convert.ToInt32(eNumber.ReadyToPrint)));
                    ENumberModel.Add(eNumber);
                }
                ViewBag.StatusSummary = ENumberModel;
                return Json(ENumberModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetInformation(string visaNumber)
        {
            try
            {
                if (visaNumber==null)
                {
                    visaNumber = "0000";
                }
                List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetInformation";
                command.Parameters.Add(new SqlParameter("@Code", visaNumber));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    var eNumber = new FinalProcessing();

                    eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                    eNumber.ArabicName = row["ArabicName"] == DBNull.Value ? "" : Convert.ToString(row["ArabicName"]);
                    eNumber.VisaQuantity = row["Quantity"] == DBNull.Value ? "" : Convert.ToString(row["Quantity"]);
                    eNumber.Used = row["Used"] == DBNull.Value ? "" : Convert.ToString(row["Used"]);
                    eNumber.Available =Convert.ToInt64(eNumber.VisaQuantity) - Convert.ToInt64(eNumber.Used);
                    ENumberModel.Add(eNumber);
                }
                return Json(ENumberModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult GetSummary(string visaNumber)
        {
            try
            {
                if (visaNumber==null)
                {
                    visaNumber = "0000";
                }
                List<FinalProcessing> ENumberModel = new List<FinalProcessing>();
                DataTable dt = new DataTable();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SP_GetSummary";
                command.Parameters.Add(new SqlParameter("@Code", visaNumber));
                //command.Parameters.Add(new SqlParameter("@ENumber", ENumber));
                _context.Database.SetCommandTimeout(0);
                command.Connection = (SqlConnection)_context.Database.GetDbConnection();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    var eNumber = new FinalProcessing();

                    eNumber.FileNo = row["fileNo"] == DBNull.Value ? "" : Convert.ToString(row["fileNo"]);
                    eNumber.Trade = row["Trade"] == DBNull.Value ? "" : Convert.ToString(row["Trade"]);
                    eNumber.VisaProcessingStatus = row["VisaProcessingStatus"] == DBNull.Value ? "" : Convert.ToString(row["VisaProcessingStatus"]);
                    eNumber.VisaQuantity = row["counts"] == DBNull.Value ? "" : Convert.ToString(row["counts"]);
                    ENumberModel.Add(eNumber);
                }
                return Json(ENumberModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public async Task<IActionResult> Barcode()
        //{
        //    //BarcodeResult Result = BarcodeReader.QuicklyReadOneBarcode("GetStarted.png");
        //    //if (Result != null && Result.Text == "https://ironsoftware.com/csharp/barcode/")
        //    //{
        //    //    Console.WriteLine("GetStarted was a success.  Read Value: " + Result.Text);
        //    //}
        //    string accountSid = Environment.GetEnvironmentVariable("ISfd4f42d4ebc6464f914a7aa7e50e68a9");
        //    string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");

        //    TwilioClient.Init(accountSid, authToken);

        //    var message = MessageResource.Create(
        //        body: "Hello there!",
        //        from: new Twilio.Types.PhoneNumber("whatsapp:+923065151566"),
        //        to: new Twilio.Types.PhoneNumber("whatsapp:+923075070470")
        //    );

        //    Console.WriteLine(message.Sid);
        //    return View();
        //}

    }
}