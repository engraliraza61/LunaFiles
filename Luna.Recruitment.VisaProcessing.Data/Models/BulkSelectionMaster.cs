using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class BulkSelectionMaster
    {
        public long Id { get; set; }
        public DateTime? UploadDate { get; set; }
        public string FileName { get; set; }
        public int? TotalRecords { get; set; }
    }
}
