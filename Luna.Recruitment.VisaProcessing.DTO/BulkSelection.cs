using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.DTO
{
    public class BulkSelection
    {
        public DateTime DateOfInterview { get; set; }
        public string PlaceOfInterview { get; set; }
        public long SponserId { get; set; }
        public string SponserGroup { get; set; }
        //public IFormFile file { get; set; }
    }
}
