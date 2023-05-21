using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class CandidateSelection
    {
        public CandidateSelection()
        {
            CandidateSelectionDetail = new HashSet<CandidateSelectionDetail>();
        }

        public long Id { get; set; }
        public DateTime SelectionDate { get; set; }
        public long SponserId { get; set; }
        public string SelectionGroup { get; set; }

        public virtual Sponser Sponser { get; set; }
        public virtual ICollection<CandidateSelectionDetail> CandidateSelectionDetail { get; set; }
    }
}
