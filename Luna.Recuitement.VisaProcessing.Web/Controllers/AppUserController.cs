using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Identity;
using Luna.Recruitment.VisaProcessing.DTO;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web;
using System.Text;
using System.Net.Http;
using System.Security.Cryptography;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    [Authorize(Roles ="Administrator")]
    public class AppUserController : Controller
    {
        private readonly lunaContext _context;
        //private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppUserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            lunaContext context)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: AspNetUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.AspNetUsers.Include(u=>u.AspNetUserRoles).ThenInclude(u=>u.Role).ToListAsync());
        }
        public JsonResult getUserName()
        {
            return Json(_context.AspNetUsers.Where(c=>c.Status!=false).ToList());
        }
        public async Task<IActionResult> AllUser()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id !=null).ToListAsync();
            return View(allUsersExceptCurrentUser);
        }
        // GET: AspNetUser/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }
        public ActionResult SendEmail(string EmailToAddress,string emailSubject,string emailBody, [FromForm] CandidateDocumentDto emailAttachment)
        {
            bool status = false;
            try
            {
                string smtpAddress = "smtp.gmail.com";
                int portNumber = 587;
                bool enableSSL = true;
                string emailFromAddress = "arkcb62@gmail.com"; //Sender Email Address  
                string password = "03075070470"; //Sender Password  
                string emailToAddress = EmailToAddress; //Receiver Email Address  
                string subject = emailSubject;
                string body = emailBody;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(emailFromAddress);
                mail.To.Add(emailToAddress);
                mail.Subject = subject;
                mail.Body = body;
                if (emailAttachment.File != null)
                {
                    var uniqueFileName = System.IO.Path.GetFileNameWithoutExtension(emailAttachment.File.FileName) + "_" + Guid.NewGuid() + System.IO.Path.GetExtension(emailAttachment.File.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents\\Excel", uniqueFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        emailAttachment.File.CopyTo(stream);
                    }
                    List<CandidateBulkSelection> candidates = new List<CandidateBulkSelection>();
                    var file = path;
                    mail.Attachments.Add(new System.Net.Mail.Attachment(file));
                }
                
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
                smtp.Credentials = new NetworkCredential(emailFromAddress, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                status = false;
            }
            //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment
            return RedirectToAction("Create", "FollowUp");
        }
        public JsonResult GetEmail (long candidateSelectionId)
        {
            var CandidateSelection = _context.CandidateSelectionDetail.Where(c => c.Id == candidateSelectionId).FirstOrDefault();
            var candidate = _context.CandidateProfile.Where(c => c.Id == CandidateSelection.CandidateProfileId).FirstOrDefault();
            return Json(new { email = candidate.Email,phone=candidate.PhoneNumber }) ;

        }
        public async Task<ActionResult> SendSMSInCsharp()
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage massage = new MailMessage();
            smtp.Credentials = new NetworkCredential("UserName", "password");
            smtp.Host = "ipipi.com";
            massage.From = new MailAddress(string.Format("{0}@ipipi.com", "username"));
            massage.To.Add(string.Format("{0}@sms.ipipi.com", "toPhone"));
            massage.Subject = "Ali Raza";
            massage.Body = "textMassage";
            smtp.Send(massage);
            return View();
        }

        // GET: AspNetUser/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name");
            return View();
        }
        public IActionResult UserActivity(string email)
        {
            List<UserActivity> activity = _context.UserActivities.Where(c=>c.UserName==email).ToList();
            return View(activity);
        }
        // POST: AspNetUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterUserDto input)
        {
            if (ModelState.IsValid)
            {                
                var user = new IdentityUser { UserName = input.Email, NormalizedUserName=input.Email.ToUpper(), Email = input.Email, NormalizedEmail=input.Email.ToUpper(),PhoneNumber=input.PhoneNumber};
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                {
                    var userRole = await _userManager.AddToRoleAsync(user, _context.AspNetRoles.Find(input.RoleId).Name);
                    if (userRole.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles, "Id", "Name", input.RoleId);

            return View(input);
        }

        // GET: AspNetUser/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var user =  await _context.AspNetUsers.AsNoTracking().FirstAsync(t=>t.Id==id);
            
            if (user == null)
            {
                return NotFound();
            }
            var userRole =  await _context.AspNetUserRoles.AsNoTracking().FirstAsync(t => t.UserId == id);
            var input = new EditUserDto() { Id = user.Id, Email = user.Email, PhoneNumber = user.PhoneNumber, Password = string.Empty, ConfirmPassword = string.Empty };
            if (userRole!=null)
            {
                input.RoleId = userRole.RoleId;
            }
            ViewData["RoleId"] = new SelectList(_context.AspNetRoles.AsNoTracking(), "Id", "Name",input.RoleId);
            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserDto input)
        {
            if (id != input.Id)
            {
                return NotFound();
            }
            IdentityResult result = null;
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(input.Id);
                    if (!String.IsNullOrWhiteSpace(input.Password) && input.Password == input.ConfirmPassword)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        result = await _userManager.ResetPasswordAsync(user, token, input.Password);
                        //var addPasswordResult = await _userManager.AddPasswordAsync(user, input.Password);
                    }
                    user.PhoneNumber = input.PhoneNumber;
                    user.Email = input.Email;
                    user.UserName = input.Email;
                    user.NormalizedEmail = input.Email.ToUpper();
                    user.NormalizedUserName = input.Email.ToUpper();
                    result = await _userManager.UpdateAsync(user);
                    var role=_context.AspNetUserRoles.Where(c => c.UserId == user.Id).FirstOrDefault();
                    _context.AspNetUserRoles.Remove(role);
                    _context.SaveChanges();
                    AspNetUserRoles aspNetUserRoles = new AspNetUserRoles();
                    aspNetUserRoles.RoleId = input.RoleId;
                    aspNetUserRoles.UserId = user.Id;
                    _context.AspNetUserRoles.Add(aspNetUserRoles);
                    _context.SaveChanges();


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AspNetUsersExists(input.Id))
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
            return View(input);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, EditUserDto input)
        //{
        //    if (id != input.Id)
        //    {
        //        return NotFound();
        //    }
        //    IdentityResult result = null;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var user = new IdentityUser { Id = id, UserName = input.Email, NormalizedUserName = input.Email.ToUpper(), Email = input.Email, NormalizedEmail = input.Email.ToUpper(), PhoneNumber = input.PhoneNumber };
        //            if (!String.IsNullOrWhiteSpace(input.Password) && input.Password == input.ConfirmPassword)
        //            {
        //                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //                result = await _userManager.ResetPasswordAsync(user, token, input.Password);
        //            }
        //            result = await _userManager.UpdateAsync(user);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AspNetUsersExists(input.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(input);
        //}
        // GET: AspNetUser/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aspNetUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetUsers == null)
            {
                return NotFound();
            }

            return View(aspNetUsers);
        }
        public async Task<JsonResult> activateUser(string active,string email)
        {
            bool status = false;
            var user = _context.AspNetUsers.Where(c => c.Email == email).FirstOrDefault();
            if(active== "Activate")
            {
                user.Status = true;
            }
            if(active== "Deactivate")
            {
                user.Status = false;
            }
            _context.AspNetUsers.Update(user);
            _context.SaveChanges();
            return Json(status);
        }

        // POST: AspNetUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUsers = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(aspNetUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AspNetUsersExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
