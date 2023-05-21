using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Passport
    {
        public long Id { get; set; }
        public long CandidateProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassportNo { get; set; }
        public long? PlaceOfIssueCountryId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Religion { get; set; }
        public long? PlaceOfBirthCountryId { get; set; }
        public long? PlaceOfBirthStateId { get; set; }
        public long? PlaceOfBirthCityId { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual City PlaceOfBirthCity { get; set; }
        public virtual Country PlaceOfBirthCountry { get; set; }
        public virtual State PlaceOfBirthState { get; set; }
        public virtual Country PlaceOfIssueCountry { get; set; }
    }
}
