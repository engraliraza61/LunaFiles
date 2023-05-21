using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Luna.Recruitment.VisaProcessing.Web.Models
{
    public partial class State
    {
        public State()
        {
            Agent = new HashSet<Agent>();
            City = new HashSet<City>();
            Client = new HashSet<Client>();
            Counslate = new HashSet<Counslate>();
            Oep = new HashSet<Oep>();
            OepvisaDemandDetail = new HashSet<OepvisaDemandDetail>();
            OepvisaDemandPodetail = new HashSet<OepvisaDemandPodetail>();
            Passport = new HashSet<Passport>();
            Sponser = new HashSet<Sponser>();
            TestCenter = new HashSet<TestCenter>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ArabicName { get; set; }
        public long CountryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Agent> Agent { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Client> Client { get; set; }
        public virtual ICollection<Counslate> Counslate { get; set; }
        public virtual ICollection<Oep> Oep { get; set; }
        public virtual ICollection<OepvisaDemandDetail> OepvisaDemandDetail { get; set; }
        public virtual ICollection<OepvisaDemandPodetail> OepvisaDemandPodetail { get; set; }
        public virtual ICollection<Passport> Passport { get; set; }
        public virtual ICollection<Sponser> Sponser { get; set; }
        public virtual ICollection<TestCenter> TestCenter { get; set; }
    }
}
