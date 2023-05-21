using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class CandidateDocument
    {
        public long Id { get; set; }
        public long? CandidateProfileId { get; set; }
        public long? FileTypeEntitySetupId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual CandidateProfile CandidateProfile { get; set; }
        public virtual EntitySetup FileTypeEntitySetup { get; set; }
    }
}
