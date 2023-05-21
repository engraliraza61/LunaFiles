using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Oeplicense
    {
        public long Id { get; set; }
        public long Oepid { get; set; }
        public string LicenseNumber { get; set; }
        public string ProprieterName { get; set; }
        public long? OepstatusEntitySetupId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Hoaddress { get; set; }
        public string Boaddress { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Oep Oep { get; set; }
        public virtual EntitySetup OepstatusEntitySetup { get; set; }
    }
}
