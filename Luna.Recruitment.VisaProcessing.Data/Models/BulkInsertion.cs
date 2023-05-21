using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class BulkInsertion
    {
        public long? Id { get; set; }
        public long? BulkSelectionMasterId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Cnic { get; set; }
        public DateTime? CnicissueDate { get; set; }
        public DateTime? CnicexpiryDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BirthCountry { get; set; }
        public string PermanentAddress { get; set; }
    }
}
