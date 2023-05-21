using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Luna.Recruitment.VisaProcessing.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{
    public class BulkProcessController : Controller
    {
        [HttpGet]
        public IActionResult Selection()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Selection(BulkSelection bulkSelection,IFormFile file)
        {
            
            return View(new { bulkSelection, file });
        }
    }
}
