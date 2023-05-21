using Luna.Recruitment.VisaProcessing.Data.DTO;
using Luna.Recruitment.VisaProcessing.DTO;
using Luna.Recruitment.VisaProcessing.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Luna.Recruitment.VisaProcessing.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<SMSSettings> SMSSettings;
        private readonly IOptions<ERPSettings> _ERPSettings;

        private readonly string SMSID = "92347217545";
        private readonly string SMSPASS = "123456";
        private readonly string SMSMASK = "LUNA";
        private readonly string SMSURL = "http://www.outreach.pk/api/sendsms.php/sendsms/url";
        public HomeController(ILogger<HomeController> logger, IOptions<SMSSettings> _SMSSettings, IOptions<ERPSettings> ERPSettings)
        {
            _logger = logger;
            SMSSettings = _SMSSettings;
            _ERPSettings = ERPSettings; 
        }

        public IActionResult Index()
        {
            ViewBag.dataALI = _ERPSettings.Value.URL;
            return View();
        }
        [HttpGet]
        public IActionResult SMS()
        {
            var sms = new SMSDto();
            return View(sms);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SMS(SMSDto sms)
        {
            if (sms != null)
            {
                
                string result = "";
                string message = HttpUtility.UrlEncode(sms.Message);
                string strPost = $"id={SMSSettings.Value.ID}&pass={SMSSettings.Value.Password}&msg={message}&to={sms.PhoneNumber}&mask={SMSSettings.Value.Mask}&type={SMSSettings.Value.Type}&lang={SMSSettings.Value.Language}";
                StreamWriter sw = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SMSURL);
                request.Method = "POST";
                request.ContentLength = Encoding.UTF8.GetByteCount(strPost);
                request.ContentType = "application/x-www-form-urlencoded";
                sw = new StreamWriter(request.GetRequestStream());
                sw.Write(strPost);
                sw.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }
                ViewBag.SMSResutl = result;
                ViewBag.SMSStatus = "SMS sent successfully.";
            }
            return View(sms);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
