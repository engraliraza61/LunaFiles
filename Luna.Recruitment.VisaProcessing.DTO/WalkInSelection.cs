using System;
using System.Collections.Generic;
using System.Text;

namespace Luna.Recruitment.VisaProcessing.DTO
{
    public class WalkInSelection
    {
        public long OEPId { get; set; }
        public string FileType { get; set; } // 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CNICNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Days { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DomicileCity { get; set; }
        public string Religion { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public long BirthCountryId { get; set; }
        public long BirthCityId { get; set; }
        public string CellNumber { get; set; }
        public string CellNumber2 { get; set; }
        public string CellNumber3 { get; set; }
        public long NationalityCountryId { get; set; }
        public long AlternateNationalityCountryId { get; set; }
        public string PermanentAddress { get; set; }
        public string PermanentAddressCity { get; set; }
        public string CurrentAddress { get; set; }
        public string CurrentAddressCity { get; set; }
        public string Reference { get; set; }
        public bool JobOffer{ get; set; } // Currently not in use
        public long JobType_EntitySetupId { get; set; }
        public string SelectionGroup { get; set; }
        public long SponserId { get; set; }
    }
}
