using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class FollowUp
    {
        public long Id { get; set; }
        public long CandidateSelectionDetailId { get; set; }
        public string Type { get; set; }
        public DateTime FollowUpDate { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual CandidateSelectionDetail CandidateSelectionDetail { get; set; }
    }
}
