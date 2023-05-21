using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
    public class ERPSettings
    {
        public const string SectionName = "ERPSettings";
        public string usr { get; set; }
        public string pwd { get; set; }
        public string Mask { get; set; }
        public string URL { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
    }
}
