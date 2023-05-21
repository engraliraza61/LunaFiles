using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class PermissionRequest
    {
        public long Id { get; set; }
        public long? OepvisaDemandId { get; set; }
        public string PermissionNumber { get; set; }
        public DateTime? ApplyDate { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DateTime? ValidityDate { get; set; }
        public long? PermissionTypeEntitySetupId { get; set; }

        public virtual OepvisaDemand OepvisaDemand { get; set; }
    }
}
