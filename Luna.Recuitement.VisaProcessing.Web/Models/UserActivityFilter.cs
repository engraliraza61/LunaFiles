using Luna.Recruitment.Models;
using Luna.Recruitment.VisaProcessing.Data.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
   public class UserActivityFilter : IActionFilter
    {
        private readonly Luna.Recruitment.VisaProcessing.Data.Models.lunaContext _context;
        public UserActivityFilter(Luna.Recruitment.VisaProcessing.Data.Models.lunaContext context)
        {
            _context = context;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var data = "";
            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var url = $"{controllerName}/{actionName}";
            if (!string.IsNullOrEmpty(context.HttpContext.Request.QueryString.Value))
            {
                data = context.HttpContext.Request.QueryString.Value;
            }
            else
            {
                var userData = new List<object>();
                foreach (var item in context.ActionArguments)
                {
                    if (item.Value != null && item.Key!="id"&&
                        !context.ActionDescriptor.DisplayName.Contains("UserActivity"))
                    {
                        var stringUserData = JsonConvert.SerializeObject(item.Value);
                        userData.Add(item.Value.ToString());                        data = stringUserData;
                        var userName = context.HttpContext.User.Identity.Name;
                        var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
                        StoreUserActivity(data, url, userName, ipAddress);
                    }
                    
                }
               
            }
           
        }
        public void StoreUserActivity(string data,string url,string userName,string ipAddress)
        {
            var userActivity = new UserActivity
            {
                Data = data,
                Url = url,
                UserName = userName,
                IpAddress = ipAddress
            };
            _context.UserActivities.Add(userActivity);
            _context.SaveChanges();
        }
    }
}
