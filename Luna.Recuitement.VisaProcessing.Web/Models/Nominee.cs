using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class Nominee
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArabicName { get; set; }
        public string Relationship { get; set; }
        public DateTime? Dob { get; set; }
        public string Cnic { get; set; }
        public string Gender { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
    }
}
