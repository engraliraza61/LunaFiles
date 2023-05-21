using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class CandidateProfile
    {
        public CandidateProfile()
        {
            CandidateDocument = new HashSet<CandidateDocument>();
            CandidateSelectionDetail = new HashSet<CandidateSelectionDetail>();
            Dependent = new HashSet<Dependent>();
            Medical = new HashSet<Medical>();
            Mehrum = new HashSet<Mehrum>();
            Nominee = new HashSet<Nominee>();
            Passport = new HashSet<Passport>();
            Vaccine = new HashSet<Vaccine>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ArabicName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? PlaceOfBirthCountryId { get; set; }
        public long? PlaceOfBirthCityId { get; set; }
        public long? PreviousNationalityCountryId { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Sect { get; set; }
        public string HomeAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string Domicile { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string Cnic { get; set; }
        public string FatherName { get; set; }
        public string FatherArabicName { get; set; }
        public string Qualification { get; set; }
        public string HusbandName { get; set; }
        public string HusbandArabicName { get; set; }
        public string Reference { get; set; }
        public long? AgentId { get; set; }
        public string ReferenceName { get; set; }
        public string ReferencePhone { get; set; }
        public string CompanyName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? IssueDate { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual City PlaceOfBirthCity { get; set; }
        public virtual Country PlaceOfBirthCountry { get; set; }
        public virtual Country PreviousNationalityCountry { get; set; }
        public virtual ICollection<CandidateDocument> CandidateDocument { get; set; }
        public virtual ICollection<CandidateSelectionDetail> CandidateSelectionDetail { get; set; }
        public virtual ICollection<Dependent> Dependent { get; set; }
        public virtual ICollection<Medical> Medical { get; set; }
        public virtual ICollection<Mehrum> Mehrum { get; set; }
        public virtual ICollection<Nominee> Nominee { get; set; }
        public virtual ICollection<Passport> Passport { get; set; }
        public virtual ICollection<Vaccine> Vaccine { get; set; }
    }
}
