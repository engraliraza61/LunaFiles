using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Web
{
    public static class AppLists
    {
        
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Religion { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Islam",Value="Islam",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Agnostic",Value="Agnostic",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Atheist",Value="Atheist",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Bahai",Value="Bahai",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Buddhism",Value="Buddhism",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Christianity",Value="Christianity",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Hinduism",Value="Hinduism",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Sikhism",Value="Sikhism",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Judaism",Value="Judaism",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Jainism",Value="Jainism",Selected=false }
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> FileTypes { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="N/A",Value="N/A",Selected=false },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Demand",Value="Demand",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Processing",Value="Processing",Selected=false },
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> VisaTypes { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="N/A",Value="N/A",Selected=false },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Work Visa",Value="Work Visa",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Shortterm Work Visit",Value="Shortterm Work Visit",Selected=false },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Longterm Work Visit",Value="Longterm Work Visit",Selected=false },
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Government Visit",Value="Government Visit",Selected=false }
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Gender { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Male",Value="Male",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Female",Value="Female",Selected=false },
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Domicile { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Lahore",Value="Lahore",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Rawalpindi",Value="Rawalpindi",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Islamabad",Value="Islamabad",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Peshawar",Value="Peshawar",Selected=false },
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> MaritalStatus { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Married",Value="Married",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Unmarried",Value="Unmarried",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Widowed",Value="Widowed",Selected=false },
            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Other",Value="Other",Selected=false },
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> DocumentTypes { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="N/A",Value="0",Selected=true },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Demand",Value="1",Selected=false },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Processing",Value="2",Selected=false },
        };
        public static List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Modes { get; set; } = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>() {
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="N/A",Value="0",Selected=true },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Email",Value="Email",Selected=false },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Phone",Value="Phone",Selected=false },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="WhatsApp",Value="WhatsApp",Selected=false },
                            new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem(){Text="Verbal",Value="Verbal",Selected=false }
        };
    }
}
