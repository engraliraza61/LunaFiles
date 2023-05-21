using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Luna.Recruitment.VisaProcessing.Data.DTO;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Luna.Recruitment.VisaProcessing.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class RolePermissionController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly lunaContext _context;

        public RolePermissionController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, lunaContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            context = _context;
        }

        public async Task<ActionResult> Index(string userId)
        {
            var model = new PermissionViewModel();
            var allPermissions = new List<RoleClaimsViewModel>();
            allPermissions.GetPermissions(typeof(Permissions.FileCreation), userId);
            allPermissions.GetPermissions(typeof(Permissions.Selection), userId);
            allPermissions.GetPermissions(typeof(Permissions.Agent), userId);
            allPermissions.GetPermissions(typeof(Permissions.UnderProcessedCandidate), userId);
            var role = await _userManager.FindByIdAsync(userId);
            var claims = await _userManager.GetClaimsAsync(role);
            var allClaimValues = allPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in allPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = allPermissions;
            return View(model);
        }
        public async Task<IActionResult> Update(PermissionViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var claim in claims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await _userManager.AddClaimAsync(user, new Claim(claim.Type, claim.Value.ToString()));
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", new { userId = model.UserId });
            
        }
    }
}
