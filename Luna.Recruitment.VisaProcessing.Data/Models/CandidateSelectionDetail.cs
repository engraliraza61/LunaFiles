using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class CandidateSelectionDetail
    {
        public CandidateSelectionDetail()
        {
            CandidatejobDetail = new HashSet<CandidatejobDetail>();
            FollowUp = new HashSet<FollowUp>();
            VisaProcess = new HashSet<VisaProcess>();
        }

        public long Id { get; set; }
        public long CandidateSelectionId { get; set; }
        public long CandidateProfileId { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual CandidateSelection CandidateSelection { get; set; }
        public virtual ICollection<CandidatejobDetail> CandidatejobDetail { get; set; }
        public virtual ICollection<FollowUp> FollowUp { get; set; }
        public virtual ICollection<VisaProcess> VisaProcess { get; set; }
    }
}
