using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class MedicalCenter
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? CityId { get; set; }
        public string Ghccode { get; set; }
        public string Gccslip { get; set; }

        public virtual City City { get; set; }
    }
}
