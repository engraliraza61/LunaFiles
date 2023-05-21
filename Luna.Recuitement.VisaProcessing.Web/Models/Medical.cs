using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class Medical
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? SponserId { get; set; }
        public string MedicalCenterName { get; set; }
        public string GhccodeNo { get; set; }
        public string GccslipNo { get; set; }
        public DateTime? DateExamined { get; set; }
        public DateTime? ReportExpiryDate { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual Sponser Sponser { get; set; }
    }
}
