using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class Vaccine
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? VaccineTypeEntitySetupId { get; set; }
        public long? VaccineVariantEntitySetupId { get; set; }
        public DateTime? VaccineDate { get; set; }
        public string VaccineDose { get; set; }
        public DateTime? VaccineValidity { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual EntitySetup VaccineTypeEntitySetup { get; set; }
        public virtual EntitySetup VaccineVariantEntitySetup { get; set; }
    }
}
