using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class PermissionDocumentMap
    {
        public long Id { get; set; }
        public long? PermissionTypeEntitySetupId { get; set; }
        public long? DocumentTypeEntitySetupId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual EntitySetup DocumentTypeEntitySetup { get; set; }
        public virtual EntitySetup PermissionTypeEntitySetup { get; set; }
    }
}
