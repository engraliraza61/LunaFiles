using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Luna.Contracts;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Luna.Recruitment.VisaProcessing.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class CandidateDocumentController : Controller
    {
        private readonly lunaContext _context;
        private readonly ILoggerManager _logger;
        private readonly IOptions<Models.FilePaths> _filePaths;

        public CandidateDocumentController(lunaContext context, ILoggerManager logger, IOptions<Models.FilePaths> filePaths)
        {
            _context = context;
            _logger = logger;
            _filePaths = filePaths;
        }
        [HttpGet]
        public IActionResult ShowDocuments(string CNIC, string passportNumber)
        {
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name");
            CandidateProfile candidate = new CandidateProfile();
            if (!String.IsNullOrWhiteSpace(CNIC))
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Cnic == CNIC);
                ViewBag.CandidatesId = candidate.Id;
            }
            else if (!String.IsNullOrWhiteSpace(passportNumber))
            {
                var passport = _context.Passport.Where(t => t.PassportNo.Replace("-", "") == passportNumber.Replace("-", ""));
                if (passport.Any())
                {
                    var candidateProfileId = passport.First().CandidateProfileId;
                    candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateProfileId);
                }
            }
            if (candidate == null) candidate = new CandidateProfile();
            return View("Upload", candidate);
            return Ok(new { fileType = fileTypes, candidate = candidate });
        }
        [HttpGet]
        public IActionResult ShowDocument(string CNIC, string passportNumber)
        {
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name");
            CandidateProfile candidate = new CandidateProfile();
            if (!String.IsNullOrWhiteSpace(CNIC))
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Cnic == CNIC);
                ViewBag.CandidatesId = candidate.Id;
            }
            else if (!String.IsNullOrWhiteSpace(passportNumber))
            {
                var passport = _context.Passport.Where(t => t.PassportNo.Replace("-", "") == passportNumber.Replace("-", ""));
                if (passport.Any())
                {
                    var candidateProfileId = passport.First().CandidateProfileId;
                    candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateProfileId);
                }
            }
            if (candidate == null) candidate = new CandidateProfile();
            //return View("Upload", candidate=candidate,fileType=fileTypes);
            return Ok(new { fileType = fileTypes, candidate = candidate });
        }
        //[Authorize(Roles = "Administrator,HR")]
        [HttpGet]
        public IActionResult Upload(long candidateProfileId)
        {
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name");
            CandidateProfile candidate = new CandidateProfile();
            if (candidateProfileId > 0)
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateProfileId);
            }
            return View(candidate);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Delete(DocumentRequestDto deleteDocumentDto)
        {
            var doc = _context.CandidateDocument.Find(deleteDocumentDto.DocumentId);
            _context.CandidateDocument.Remove(doc);
            _context.SaveChanges();
            var candidate = _context.CandidateProfile
                .Include(t => t.CandidateDocument)
                .FirstOrDefault(t => t.Id == deleteDocumentDto.CandidateProfileId);
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name");
            return View("Upload", candidate);
        }
        [AllowAnonymous]
        public IActionResult Download(long documentId)
        {
            var doc = _context.CandidateDocument.Find(documentId);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\files", doc.FileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            FileInfo fileInfo = new FileInfo(path);
            var exten = fileInfo.Extension;
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(fileBytes);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline"); //opens in tab, use 'attachment' to download instead
            response.Content.Headers.ContentDisposition.FileName = $"{"document"}_{DateTime.Now.ToString("MMddyyyyHHmmss")}."+exten+"";
            return File(fileBytes, "application/octet-stream", doc.FileName);
            //return View("Upload", candidate);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Upload([FromForm] CandidateDocumentDto candidateDocumentDto)
        {
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name", candidateDocumentDto.FileTypeEntitySetupId);
            CandidateProfile candidate = new CandidateProfile();

            if (!ModelState.IsValid)
            {
                if (candidateDocumentDto.CandidateProfileId > 0)
                {
                    candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateDocumentDto.CandidateProfileId);
                }
                ViewBag.ErrorMessage = ModelState.Values.First().Errors.First().ErrorMessage;
                return View(candidate);
            }
            if (candidateDocumentDto.File == null || candidateDocumentDto.File.Length == 0)
                return Content("file not selected");
            var uniqueFileName = System.IO.Path.GetFileNameWithoutExtension(candidateDocumentDto.File.FileName) + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(candidateDocumentDto.File.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\files", uniqueFileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                candidateDocumentDto.File.CopyTo(stream);
            }
            var candidateDoc = new CandidateDocument();
            candidateDoc.CandidateProfileId = candidateDocumentDto.CandidateProfileId;
            candidateDoc.FileName = uniqueFileName;
            candidateDoc.FileTypeEntitySetupId = candidateDocumentDto.FileTypeEntitySetupId;
            candidateDoc.UploadDate = DateTime.Now;
            _context.CandidateDocument.Add(candidateDoc);
            _context.SaveChanges();
            if (candidateDoc.CandidateProfileId > 0)
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateDoc.CandidateProfileId);
            }
            return View(candidate);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GetUserDocuments(string Cnic, string passport)
        {
            var fileTypes = _context.EntitySetup.Where(t => t.EntityType.Code == "10");
            ViewBag.FileTypeEntitySetupId = new SelectList(fileTypes, "Id", "Name");
            var candidate = new CandidateProfile();
            if (!String.IsNullOrWhiteSpace(Cnic))
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Cnic == Cnic);
            }
            else if (!String.IsNullOrWhiteSpace(passport))
            {
                var passports = _context.Passport.Where(t => t.PassportNo.Replace("-", "") == passport.Replace("-", ""));
                if (passports.Any())
                {
                    var candidateProfileId = passports.First().CandidateProfileId;
                    candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Id == candidateProfileId);
                }
            }
            if (candidate == null) candidate = new CandidateProfile();
            return Ok(new { fileType = fileTypes, candidate = candidate });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UploadDocuments(IList<IFormFile> files, string cnic, string passport, int fileType)
        {
            CandidateProfile candidate = new CandidateProfile();
            long candidateProfileId = 0;
            if (!String.IsNullOrWhiteSpace(cnic))
            {
                candidate = _context.CandidateProfile.Include(t => t.CandidateDocument).FirstOrDefault(t => t.Cnic == cnic);
                candidateProfileId = candidate.Id;
            }
            else if (!String.IsNullOrWhiteSpace(passport))
            {
                var passports = _context.Passport.Where(t => t.PassportNo.Replace("-", "") == passport.Replace("-", ""));
                if (passports.Any())
                {
                    candidateProfileId = passports.First().CandidateProfileId;
                }
            }
            if (candidateProfileId > 0)
            {
                string filename = "";
                string originalFileName = "";
                foreach (IFormFile source in files)
                {
                    filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
                    originalFileName = source.FileName; //TODO:
                    filename = this.EnsureCorrectFilename(filename) ;

                    //using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                    //    await source.CopyToAsync(output);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\files", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                    }
                }

                var candidateDoc = new CandidateDocument();
                candidateDoc.CandidateProfileId = candidateProfileId;
                candidateDoc.FileName = filename;
                candidateDoc.FileTypeEntitySetupId = fileType;
                candidateDoc.UploadDate = DateTime.Now;
                _context.CandidateDocument.Add(candidateDoc);
                _context.SaveChanges(); 
                return Ok(true);
            }
            else
            {
                return NotFound();
            }
            

            
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DeleteDocument(long documentId)
        {
            var doc = _context.CandidateDocument.Find(documentId);
            _context.CandidateDocument.Remove(doc);
            _context.SaveChanges();
            return Ok(true);
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            var uniqueFileName = System.IO.Path.GetFileNameWithoutExtension(filename) + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(filename);
            return uniqueFileName;
        }

        private string GetPathAndFilename(string filename)
        {
            return _filePaths.Value.UploadFilePath + "\\" + filename;
        }
    }
}
