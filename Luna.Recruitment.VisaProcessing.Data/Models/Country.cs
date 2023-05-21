using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            Agent = new HashSet<Agent>();
            CandidateProfilePlaceOfBirthCountry = new HashSet<CandidateProfile>();
            CandidateProfilePreviousNationalityCountry = new HashSet<CandidateProfile>();
            Client = new HashSet<Client>();
            Counslate = new HashSet<Counslate>();
            Oep = new HashSet<Oep>();
            OepvisaDemand = new HashSet<OepvisaDemand>();
            OepvisaDemandDetail = new HashSet<OepvisaDemandDetail>();
            OepvisaDemandPo = new HashSet<OepvisaDemandPo>();
            OepvisaDemandPodetail = new HashSet<OepvisaDemandPodetail>();
            PassportPlaceOfBirthCountry = new HashSet<Passport>();
            PassportPlaceOfIssueCountry = new HashSet<Passport>();
            Sponser = new HashSet<Sponser>();
            State = new HashSet<State>();
            TestCenter = new HashSet<TestCenter>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public string Language { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Agent> Agent { get; set; }
        public virtual ICollection<CandidateProfile> CandidateProfilePlaceOfBirthCountry { get; set; }
        public virtual ICollection<CandidateProfile> CandidateProfilePreviousNationalityCountry { get; set; }
        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<Counslate> Counslate { get; set; }
        public virtual ICollection<Oep> Oep { get; set; }
        public virtual ICollection<OepvisaDemand> OepvisaDemand { get; set; }
        public virtual ICollection<OepvisaDemandDetail> OepvisaDemandDetail { get; set; }
        public virtual ICollection<OepvisaDemandPo> OepvisaDemandPo { get; set; }
        public virtual ICollection<OepvisaDemandPodetail> OepvisaDemandPodetail { get; set; }
        public virtual ICollection<Passport> PassportPlaceOfBirthCountry { get; set; }
        public virtual ICollection<Passport> PassportPlaceOfIssueCountry { get; set; }
        public virtual ICollection<Sponser> Sponser { get; set; }
        public virtual ICollection<State> State { get; set; }
        public virtual ICollection<TestCenter> TestCenter { get; set; }
    }
}
