using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public class SelectionAddCandidateMaster
    {
        [Key]
        public long BatchNumber { get; set; }
        public string SelectionGroup { get; set; }
        public DateTime? DateOfInterview { get; set; }
        public string PlaceOfInterview { get; set; }
        public string ListFile { get; set; }
        public DateTime? DateOfUploading { get; set; }
        public long SponserId { get; set; }
    }
}
