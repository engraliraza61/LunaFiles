using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class CandidateBulkSelection
    {
        [Key]
        public int Id { get; set; }
        public string Reference { get; set; }
        public string BatchNumber { get; set; }
        public string CandidateName { get; set; }
        public string FatherName { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string CnicNumber { get; set; }
        public string CnicIssueDate { get; set; }
        public string ExpiryDate { get; set; }
        public string DateOfBirth { get; set; }
        public string BirthCountry { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public string Religion { get; set; }
        public string CellNo { get; set; }
        public string PassportNo { get; set; }
        public string PassportIssueDate { get; set; }
        public string passportExpiryDate { get; set; }
        public string SelectionTrade { get; set; }  
        public string Salary { get; set; }
        public string ContrctDurationType { get; set; }
        public string ContrctDuration { get; set; }
        public string AgentName { get; set; }
        public string Food { get; set; }
        public string Status { get; set; }
    }
}
